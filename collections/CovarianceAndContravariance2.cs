using System;
using System.Collections;
using System.Collections.Generic;
					
public class Program
{
	static object GetObject() { return null; }  
    static void SetObject(object obj) { }  
    
    static string GetString() { return ""; }  
    static void SetString(string str) { }

	public static void Main()
	{
		Func<object> del = GetString;
        Action<string> del2 = SetObject; 
	}
}