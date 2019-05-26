using System;
using System.Collections.Generic;

public static class GlobalMembers
{
	public static int n;
	public static int m;
	public static int[,] Map = new int[15, 15];
	public static node[,] pre = new node[15, 15];
	public static int[,] visit = new int[15, 15];
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

    public static int inborad(int x,int y)
	{
		if (x >= 0 && x<n && y >= 0 && y < m)
		{
		return 1;
		}
		return 0;
	}
	static int Main()
	{
		Console.Write("请输入地图大小: \n");
		string tempVar = ConsoleInput.ScanfRead();
		if (tempVar != null)
		{
			n = int.Parse(tempVar);
		}
		string tempVar2 = ConsoleInput.ScanfRead();
		if (tempVar2 != null)
		{
			m = int.Parse(tempVar2);
		}
		Console.Write("请输入地图:\n");
		for (int i = 0;i < n;i++)
		{
			for (int j = 0;j < m;j++)
			{
				string tempVar3 = ConsoleInput.ScanfRead();
				if (tempVar3 != null)
				{
					Map[i, j] = int.Parse(tempVar3);
				}
			}
		}
		Console.Write("请输入起点位置：\n");
		string tempVar4 = ConsoleInput.ScanfRead();
		if (tempVar4 != null)
		{
			spos.x = int.Parse(tempVar4);
		}
		string tempVar5 = ConsoleInput.ScanfRead();
		if (tempVar5 != null)
		{
			spos.y = int.Parse(tempVar5);
		}
		Console.Write("请输入终点位置：\n");
		string tempVar6 = ConsoleInput.ScanfRead();
		if (tempVar6 != null)
		{
			epos.x = int.Parse(tempVar6);
		}
		string tempVar7 = ConsoleInput.ScanfRead();
		if (tempVar7 != null)
		{
			epos.y = int.Parse(tempVar7);
		}

		spos.steps = 0;
		Queue<node> Q = new Queue<node>();
		Q.Enqueue(spos);
		node temp = new node();
		node temp1 = new node();
		int flag = 0;
		visit[spos.x, spos.y] = 1;

		while (Q.Count > 0)
		{
			temp = Q.Peek();
			Q.Dequeue();
			if (temp.x == epos.x && temp.y == epos.y)
			{
				Console.Write("shortest dist:");
				Console.Write(temp.steps);
				Console.Write("\n");
				node[] route = Arrays.InitializeWithDefaultInstances<node>(300);
				int cnt=0;
				int xx = temp.x;
				int yy = temp.y;
				temp1.x = xx;
				temp1.y = yy;
				while (xx != spos.x || yy != spos.y)
				{
					route[cnt++] = temp1;
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
//ORIGINAL LINE: temp1=pre[temp1.x][temp1.y];
					temp1=pre[temp1.x, temp1.y];
					xx = temp1.x;
					yy = temp1.y;
				}
				Console.Write("(");
				Console.Write(spos.x);
				Console.Write(",");
				Console.Write(spos.y);
				Console.Write(")");

				for (int i = cnt;i >= 0;i--)
				{
					Console.Write("(");
					Console.Write(route[i].x);
                    Y = route[i].y;
                    Console.Write(",");
					Console.Write(route[i].y);
					Console.Write(")");
				}
				flag = 1;
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
		if (flag == 0)
		{
		Console.Write("No solution!");
		}

		return Y;
	}

}