using System;
using System.Diagnostics;
using System.IO;
using ProgramManager;

namespace ProgramManager;

public class Paths()
{
    public static void Show()
    {
        Global.dirs = Directory.GetDirectories(Global.path);
        Global.files = Directory.GetFiles(Global.path);

        Global.items = Global.dirs.Concat(Global.files).ToArray();

        Console.WriteLine(Global.path);
        Make.New();

        Global.num = 1;

        foreach (string dir in Global.dirs)
        {
            Console.WriteLine(Global.num + ". " + Path.GetFileName(dir));
            Global.num++;
        }

        foreach (string file in Global.files)
        {
            Console.WriteLine(Global.num + ". " + Path.GetFileName(file));
            Global.num++;
        }

        Make.Separator();
    }
}