using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using ProgramManager;

namespace ProgramManager;

class Global
{
    public static string path = string.Empty;
    public static string[] dirs = Array.Empty<string>();
    public static string[] files = Array.Empty<string>();
    public static string[] items = Array.Empty<string>();
    public static int num;
    public static int value;
    public static string command = string.Empty;
}

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();

        Global.path = @"C:\Users\fUJITSU\OneDrive\Desktop\programs";
        Paths.Show();
        
        Make.New();

        Console.Write(">>> ");
        Global.command = Console.ReadLine()!;

        while (Global.command != "close")
        {
            Make.New();
            Base.Commands();

            Console.Write(">>> ");
            Global.command = Console.ReadLine()!;
        }
    }
}