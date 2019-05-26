using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GlobalMembers : Events
{
	public static int n;
	public static int m;
	public static int[,] Map = new int[50, 50];
	public static node[,] pre = new node[50, 50];
	public static int[,] visit = new int[50, 50];
	public static int[,] dir =
	{
		{0, 1},
		{0, -1},
		{-1, 0},
		{1, 0}
	};

	public static node spos = new node();
	public static node epos = new node();
    public static int Y;

    public Tilemap obstacle;
    public Vector2 topLeftPos;
    public Vector2 bottomRightPos;

    Vector2 direction;
    float speed = 1;

    void Start()
    {
        //Map = new int[(int)(topLeftPos.y - bottomRightPos.y + 1), (int)(bottomRightPos.x - topLeftPos.x + 1)];
        //visit = new int[(int)(topLeftPos.y - bottomRightPos.y + 1), (int)(bottomRightPos.x - topLeftPos.x + 1)];
        //pre = new node[(int)(topLeftPos.y - bottomRightPos.y + 1), (int)(bottomRightPos.x - topLeftPos.x + 1)];
        for (float y = topLeftPos.y; y <= bottomRightPos.y; y++)
        {
            int i = (int)(y - topLeftPos.y);
            for (float x = topLeftPos.x; x <= bottomRightPos.x; x++)
            {
                int j = (int)(x - topLeftPos.x);
                Vector3Int cellPosition = obstacle.WorldToCell(new Vector2(x, y));
                if (obstacle.HasTile(cellPosition)) Map[i,j] = 0;
                else Map[i,j] = 1;
            }
        }
        moving = false;
    }

    private void Update()
    {
        if (moving) EnemyMove();
        else EnemyControl(FindRoute(transform.position, player.GetComponent<Events>().target));
    }

    public static int inborad(int x,int y)
	{
        if (x >= 0 && x < n && y >= 0 && y < m) 
		{
		return 1;
		}
		return 0;
	}
    Vector2 FindRoute(Vector2 start, Vector2 end)
	{
        start = new Vector2(start.x - topLeftPos.x, topLeftPos.y - start.y);
        end = new Vector2(end.x - topLeftPos.x, topLeftPos.y - end.y);
        spos.x = (int)start.x; spos.y = (int)start.y;
        epos.x = (int)end.x; epos.y = (int)end.y;
        //Debug.Log(epos.x + "," + epos.y);
        spos.steps = 0;
		Queue<node> Q = new Queue<node>();
		Q.Enqueue(spos);
		node temp = new node();
		node temp1 = new node();
		visit[spos.x, spos.y] = 1;

		while (Q.Count > 0)
		{
			temp = Q.Peek();
			Q.Dequeue();
			if (temp.x == epos.x && temp.y == epos.y)
			{
                Debug.Log("b");
                //Console.Write("shortest dist:");
                //Console.Write(temp.steps);
                //Console.Write("\n");
                node[] route = Arrays.InitializeWithDefaultInstances<node>(300);
				int cnt=0;
				int xx = temp.x;
				int yy = temp.y;
				temp1.x = xx;
				temp1.y = yy;
				while (xx != spos.x || yy != spos.y)
				{
					route[cnt++] = temp1;
					temp1=pre[temp1.x, temp1.y];
					xx = temp1.x;
					yy = temp1.y; 
				}
                Debug.Log(spos.x + "," + spos.y);
                direction = new Vector2(route[1].x - spos.x, route[1].y - spos.y);

                //Console.Write("(");
                //Console.Write(spos.x);
                //Console.Write(",");
                //Console.Write(spos.y);
                //Console.Write(")");

                //for (int i = cnt;i >= 0;i--)
                //{
                //	Console.Write("(");
                //	Console.Write(route[i].x);
                //                Y = route[i].y;
                //                Console.Write(",");
                //	Console.Write(route[i].y);
                //	Console.Write(")");
                //}
                //flag = 1;
                break;
			}
			temp1.steps = temp.steps + 1;
			for (int i = 0;i < 4;i++)
			{
				temp1.x = temp.x + dir[i, 0];
				temp1.y = temp.y + dir[i, 1];
				if (inborad(temp1.x, temp1.y) != 0 && visit[temp1.x, temp1.y] == 0 && Map[temp1.x, temp1.y] != 0)
				{
					pre[temp1.x, temp1.y] = temp;
					visit[temp1.x, temp1.y] = 1;
					Q.Enqueue(temp1);
				}

			}
		}

        return direction;
    }

    public void EnemyMove()
    {
        if (moving)
        {
            rb.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * 5 * an.speed * speed);
            if (Vector2.Distance(rb.position, target) <= 0.001f)
            {
                moving = false;
                rb.position = target;
                ////if ((!Input.GetButton("Horizontal") && !Input.GetButton("Vertical")) || FaceObstacle())
                //{
                //    an.enabled = false;
                //    SetSprite();
                //}
            }
        }
    }

    public void EnemyControl(Vector2 direction)
    {
        //Debug.Log(direction);
        if (!moving)
        {
            faceOrientation = direction;
            if (!FaceObstacle())
            {
                an.enabled = true;
                moving = true;
                target = rb.position + faceOrientation;
                SetSprite();
                //SetWalkAnimation();
            }
        }
    }
}