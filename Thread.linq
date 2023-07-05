<Query Kind="Program" />

/// <summary>
/// https://threads.whuanle.cn/
/// 练习线程的使用
/// 首先C# 的线程是由CLR注入的，不是直接由操作系统注入的
/// </summary>

unsafe void Main()
{
	var currentThread = Thread.CurrentThread;
	Console.WriteLine("线程标识：" + currentThread.Name);
	Console.WriteLine("当前地域：" + currentThread.CurrentCulture.Name);
	Console.WriteLine("线程执行状态：" + currentThread.IsAlive);
	Console.WriteLine("是否为后台线程：" + currentThread.IsBackground);
	Console.WriteLine("是否为线程池线程" + currentThread.IsThreadPoolThread);
	
	string myParam = "abcdef";
	ParameterizedThreadStart parameterized = new ParameterizedThreadStart(OneTest);
	Thread thread = new Thread(parameterized);
	thread.Start(myParam);

	Thread thread1 = new Thread(() =>
	{
		Console.WriteLine("启用线程1");
	});
	thread1.Start();

	var thread3 = new Thread(Print);
	Console.WriteLine("开始执行线程 B");

	thread3.Start();

	// 开始等待另一个完成完成
	thread3.Join();
	Console.WriteLine("线程 B 已经完成");

	Console.WriteLine("结构体大小" + sizeof(Test));
	new Thread(() =>
	   {
	   		Console.WriteLine("执行StackOver");
			StackOver();
			Console.WriteLine("StackOver执行完毕");
	   }, 64 * 1024).Start();
}

// You can define other methods, fields, classes and namespaces here

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
	Test* test = stackalloc Test[750];         // 729 不会导致堆栈溢出
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