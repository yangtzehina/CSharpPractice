<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

/// <summary>
/// https://threads.whuanle.cn/
/// 练习线程的使用
/// 首先C# 的线程是由CLR注入的，不是直接由操作系统注入的
/// </summary>

unsafe void Main()
{
	// 定义两个任务
	Task task1 = new Task(() =>
	{
		Console.WriteLine("① 开始执行");
		Thread.Sleep(TimeSpan.FromSeconds(1));

		Console.WriteLine("① 执行中");
		Thread.Sleep(TimeSpan.FromSeconds(1));

		Console.WriteLine("① 执行即将结束");
	});

	Task task2 = new Task(MyTask);
	// 开始任务
	task1.Start();
	task2.Start();
}

// You can define other methods, fields, classes and namespaces here

private static void MyTask()
{
	Console.WriteLine("② 开始执行");
	Thread.Sleep(TimeSpan.FromSeconds(1));

	Console.WriteLine("② 执行中");
	Thread.Sleep(TimeSpan.FromSeconds(1));

	Console.WriteLine("② 执行即将结束");
}


private static int sum = 0;
public static void AddOne()
{
	for (int i = 0; i < 100_0000; i++)
	{
		Interlocked.Add(ref sum,1);
	}
}

public static void Print()
{
	for (int i = 0; i < 10; i++)
	{
		Console.WriteLine(i);
		Thread.Sleep(1000);
	}
}

public static void OneTest(object obj)
{
	string str = obj as string;
	if (string.IsNullOrEmpty(str))
		return;

	Console.WriteLine("新的线程已经启动");
	Console.WriteLine(str);
}

public static System.Threading.ThreadState GetThreadState(System.Threading.ThreadState ts)
{
	return ts & (System.Threading.ThreadState.Unstarted |
	System.Threading.ThreadState.WaitSleepJoin |
	System.Threading.ThreadState.Stopped);
}

static unsafe void StackOver()
{
	Test* test = stackalloc Test[730];         // 729 不会导致堆栈溢出
}

public struct Test
{
	public long A;
	public long B;
	public long C;
	public long D;
	public long E;
	public long F;
	public long G;
	public long H;
}