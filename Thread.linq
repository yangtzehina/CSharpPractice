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
	// 实例化任务类
	MyTaskClass<string> myTask1 = new MyTaskClass<string>(() =>
	{
		Thread.Sleep(TimeSpan.FromSeconds(1));
		return "www.whuanle.cn";
	});

	// 直接同步获取结果
	Console.WriteLine(myTask1.GetResult());


	// 实例化任务类
	MyTaskClass<string> myTask2 = new MyTaskClass<string>(() =>
	{
		Thread.Sleep(TimeSpan.FromSeconds(1));
		return "www.whuanle.cn";
	});

	// 异步获取结果
	myTask2.RunAsync();

	Console.WriteLine(myTask2.Result);

}

/// <summary>
/// 实现同步任务和异步任务的类型
/// </summary>
/// <typeparam name="TResult"></typeparam>
public class MyTaskClass<TResult>
{
	private readonly TaskCompletionSource<TResult> source = new TaskCompletionSource<TResult>();
	private Task<TResult> task;
	// 保存用户需要执行的任务
	private Func<TResult> _func;

	// 是否已经执行完成，同步或异步执行都行
	private bool isCompleted = false;
	// 任务执行结果
	private TResult _result;

	/// <summary>
	/// 获取执行结果
	/// </summary>
	public TResult Result
	{
		get
		{
			if (isCompleted)
				return _result;
			else return task.Result;
		}
	}
	public MyTaskClass(Func<TResult> func)
	{
		_func = func;
		task = source.Task;
	}

	/// <summary>
	/// 同步方法获取结果
	/// </summary>
	/// <returns></returns>
	public TResult GetResult()
	{
		_result = _func.Invoke();
		isCompleted = true;
		return _result;
	}

	/// <summary>
	/// 异步执行任务
	/// </summary>
	public void RunAsync()
	{
		Task.Factory.StartNew(() =>
		{
			source.SetResult(_func.Invoke());
			isCompleted = true;
		});
	}
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