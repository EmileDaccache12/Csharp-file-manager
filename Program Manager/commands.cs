using System;
using System.Diagnostics;
using System.IO;
using ProgramManager;

namespace ProgramManager;

public class COMMANDS()
{
    public static void Show()
    {
        Console.Clear();

        Console.WriteLine("Commands: ");
        Make.Separator();

        string[] commands = [
            "  • open : open file / directory",
            "  • /open : open current file / directory",
            "  • code : open in VS Code",
            "  • /code : open current in VS Code",
            "  • VS : open VS code",
            "  • cd : change directory",
            "  • return : return to previous directory",
            "  • /return : return to root directory",
            "  • /clear : clear console",
            "  • dir.new : create new directory",
            "  • file.new : create new file",
            "  • delete / del : delete directory or file",
            "  • /delete : delete current directory",
            "  • delete . : delete current directory content",
            "  • rename : rename directory or file",
            "  • // commands : show commands",
            "  • // root : return to root directory",
            "  • close : close Program Manager"
        ];

        foreach (string command in commands)
            Console.WriteLine(command);

        Make.Separator();
    }
}