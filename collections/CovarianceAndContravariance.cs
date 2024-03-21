using System;
using System.Collections;
using System.Collections.Generic;
					
public class Program
{
	static void SetObject(object o) { }
	Action<object> actObject = SetObject;
	
	public static void Main()
	{
		string str = "test";
		object obj = str;
		
		IEnumerable<string> strings = new List<string>() { str };
		IEnumerable<object> objects = strings;
		
		foreach(var item in objects)
		Console.Write(item);
	}
}