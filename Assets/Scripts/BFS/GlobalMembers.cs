using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GlobalMembers : Events
{
	//public static int n;
	//public static int m;
	public static int[,] Map = new int[50, 50];
	//public static node[,] pre = new node[50, 50];
	//public static int[,] visit = new int[50, 50];
	//public static int[,] dir =
	//{
	//	{0, 1},
	//	{0, -1},
	//	{-1, 0},
	//	{1, 0}
	//};

	//public static node spos = new node();
	//public static node epos = new node();
 //   public static int Y;

    public Tilemap obstacle;
    public Vector2 topLeftPos;
    public Vector2 bottomRightPos;
    Vector2 firstPoint;

    Vector2 direction;
    float speed = 1;

    void Start()
    {
        Map = new int[(int)(topLeftPos.y - bottomRightPos.y + 1), (int)(bottomRightPos.x - topLeftPos.x + 1)];
        //visit = new int[(int)(topLeftPos.y - bottomRightPos.y + 1), (int)(bottomRightPos.x - topLeftPos.x + 1)];
        //pre = new node[(int)(topLeftPos.y - bottomRightPos.y + 1), (int)(bottomRightPos.x - topLeftPos.x + 1)];
        for (float y = topLeftPos.y; y <= bottomRightPos.y; y++)
        {
            int i = (int)(y - topLeftPos.y);
            for (float x = topLeftPos.x; x <= bottomRightPos.x; x++)
            {
                int j = (int)(x - topLeftPos.x);
                Vector3Int cellPosition = obstacle.WorldToCell(new Vector2(x, y));
                if (obstacle.HasTile(cellPosition)) Map[j,i] = 0;
                else Map[j,i] = 1;
            }
        }
        moving = false;
    }

    private void Update()
    {
        if (moving) EnemyMove();
        else EnemyControl(FindRoute(transform.position, player.GetComponent<Events>().target, Map));
    }
    public class MyPoint
    {
        public MyPoint parent { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public MyPoint(int a, int b)
        {
            this.X = a;
            this.Y = b;
        }
    }
    void WritePath(MyPoint p)
    {
        if (p.parent != null)
        {

            WritePath(p.parent);
            if (p.parent.parent == null)
            {
                firstPoint = new Vector2(p.X, p.Y);
                Debug.Log(p.X);
            }
                
            //Console.Write("(" + p.X + "," + p.Y + ")-->");
        }
        


    }
    //static void Main(string[] args)
    //{
    //    int[,] data = new int[,]{  //初始化数据 1为路 0为墙
    //            {1,1,0,1,1},
    //            {1,0,1,1,1},
    //            {1,0,1,0,0},
    //            {1,0,1,1,1},
    //            {1,1,1,0,1},
    //            {1,1,1,1,1}
    //        };


    //    MyPoint p_bfs = new MyPoint(0, 0);
    //    p_bfs.parent = null;
    //    //BFS(p_bfs, data);
    //    Console.Read();

    //}

    Vector2 FindRoute(Vector2 start, Vector2 end, int[,] data)
    {
        start = new Vector2(start.x - topLeftPos.x, topLeftPos.y - start.y);
        end = new Vector2(end.x - topLeftPos.x, topLeftPos.y - end.y);
        MyPoint p = new MyPoint((int)start.x, (int)start.y);
        p.parent = null;
        Vector2 direction = Vector2.right;
        Queue q = new Queue();
        data[(int)start.x, (int)start.y] = -1;
        q.Enqueue(p);
        while (q.Count > 0)
        {
            MyPoint qp = (MyPoint)q.Dequeue();
            for (int i = -1; i < 2; i++) //遍历可以到达的节点
            {
                for (int j = -1; j < 2; j++)
                {
                    if ((qp.X + i >= 0) && (qp.X + i <= (int)bottomRightPos.x) && (qp.Y + j >= 0) && (qp.Y + j <= (int)bottomRightPos.y) && (qp.X + i == qp.X || qp.Y == qp.Y + j)) //是否越界 只遍历上下左右
                    {
                        if (data[qp.X + i, qp.Y + j] == 1)
                        {
                            if (qp.X + i == (int)end.x && qp.Y + j == (int)end.y)  //是否为终点
                            {
                                //Console.Write("BFS:(0,0)-->");
                                WritePath(qp); //递归输出路径
                                //Console.Write("(5,4)");
                                //Console.WriteLine("");
                                direction = firstPoint - start;
                                Debug.Log(direction);
                                return direction;
                            }
                            else
                            {
                                MyPoint temp = new MyPoint(qp.X + i, qp.Y + j);   //加入队列
                                data[qp.X + i, qp.Y + j] = -1;
                                temp.parent = qp;
                                q.Enqueue(temp);
                            }
                        }
                    }
                }
            }
        }
        return Vector2.zero;
    }
    //   public static int inborad(int x,int y)
    //{
    //       if (x >= 0 && x < n && y >= 0 && y < m) 
    //	{
    //	return 1;
    //	}
    //	return 0;
    //}
    //   Vector2 FindRoute(Vector2 start, Vector2 end)
    //{
    //       start = new Vector2(start.x - topLeftPos.x, topLeftPos.y - start.y);
    //       end = new Vector2(end.x - topLeftPos.x, topLeftPos.y - end.y);
    //       spos.x = (int)start.x; spos.y = (int)start.y;
    //       epos.x = (int)end.x; epos.y = (int)end.y;
    //       //Debug.Log(epos.x + "," + epos.y);
    //       spos.steps = 0;
    //	Queue<node> Q = new Queue<node>();
    //	Q.Enqueue(spos);
    //	node temp = new node();
    //	node temp1 = new node();
    //	visit[spos.x, spos.y] = 1;

    //	while (Q.Count > 0)
    //	{
    //		temp = Q.Peek();
    //		Q.Dequeue();
    //		if (temp.x == epos.x && temp.y == epos.y)
    //		{
    //               Debug.Log("b");
    //               //Console.Write("shortest dist:");
    //               //Console.Write(temp.steps);
    //               //Console.Write("\n");
    //               node[] route = Arrays.InitializeWithDefaultInstances<node>(300);
    //			int cnt=0;
    //			int xx = temp.x;
    //			int yy = temp.y;
    //			temp1.x = xx;
    //			temp1.y = yy;
    //			while (xx != spos.x || yy != spos.y)
    //			{
    //				route[cnt++] = temp1;
    //				temp1=pre[temp1.x, temp1.y];
    //				xx = temp1.x;
    //				yy = temp1.y; 
    //			}
    //               Debug.Log(spos.x + "," + spos.y);
    //               direction = new Vector2(route[1].x - spos.x, route[1].y - spos.y);

    //               //Console.Write("(");
    //               //Console.Write(spos.x);
    //               //Console.Write(",");
    //               //Console.Write(spos.y);
    //               //Console.Write(")");

    //               //for (int i = cnt;i >= 0;i--)
    //               //{
    //               //	Console.Write("(");
    //               //	Console.Write(route[i].x);
    //               //                Y = route[i].y;
    //               //                Console.Write(",");
    //               //	Console.Write(route[i].y);
    //               //	Console.Write(")");
    //               //}
    //               //flag = 1;
    //               break;
    //		}
    //		temp1.steps = temp.steps + 1;
    //		for (int i = 0;i < 4;i++)
    //		{
    //			temp1.x = temp.x + dir[i, 0];
    //			temp1.y = temp.y + dir[i, 1];
    //			if (inborad(temp1.x, temp1.y) != 0 && visit[temp1.x, temp1.y] == 0 && Map[temp1.x, temp1.y] != 0)
    //			{
    //				pre[temp1.x, temp1.y] = temp;
    //				visit[temp1.x, temp1.y] = 1;
    //				Q.Enqueue(temp1);
    //			}

    //		}
    //	}

    //       return direction;
    //   }

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