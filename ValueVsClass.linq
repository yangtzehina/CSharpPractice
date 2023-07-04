<Query Kind="Statements">
  <Namespace>System.Runtime.CompilerServices</Namespace>
  <Namespace>System.Runtime.InteropServices</Namespace>
</Query>

/// <summary>
/// https://www.cnblogs.com/artech/p/17509624.html
/// 值类型vs引用类型的练习
/// 
/// </summary>

var s1 = new FoobarStruct(255, 1);
var c1 = new FoobarClass(255, 1);
var s2 = s1;
var c2 = c1;

Console.WriteLine($"s1: {Utility.AsPointer(ref s1)}");
Console.WriteLine($"c1: {Utility.AsPointer(ref c1)}");
Console.WriteLine($"s2: {Utility.AsPointer(ref s2)}");
Console.WriteLine($"c2: {Utility.AsPointer(ref c2)}");

Console.WriteLine($"s1: {BitConverter.ToString(Utility.Read(ref s1))}");
Console.WriteLine($"c1: {BitConverter.ToString(Utility.Read(ref c1))}");
Console.WriteLine($"s2: {BitConverter.ToString(Utility.Read(ref s2))}");
Console.WriteLine($"c2: {BitConverter.ToString(Utility.Read(ref c2))}");

var s = new FoobarStruct(255, 1);
var c = new FoobarClass(255, 1);
Utility.Invoke(s, c);

internal static class Utility
{
	//或者指定变量的指针，并转为IntPtr(nint)类型
	public static unsafe nint AsPointer<T>(ref T value) => new(Unsafe.AsPointer(ref value));
	
	//拷贝变量到数组中
	public static unsafe byte[] Read<T>(ref T value)
	{
		byte[] bytes = new byte[Unsafe.SizeOf<T>()];
		Marshal.Copy(AsPointer(ref value), bytes, 0, bytes.Length);
		return bytes;
	}

	static public void Invoke(FoobarStruct args, FoobarClass argc)
	{
		args.Foo = 0;
		args.Bar = 0;
		argc.Foo = 0;
		argc.Bar = 0;
	}
}

public class FoobarClass
{
	public byte Foo { get; set; }
	public long Bar { get; set; }
	public FoobarClass(byte foo, long bar)
	{
		Foo = foo;
		Bar = bar;
	}
}

public struct FoobarStruct
{
	public byte Foo { get; set; }
	public long Bar { get; set; }
	public FoobarStruct(byte foo, long bar)
	{
		Foo = foo;
		Bar = bar;
	}
}