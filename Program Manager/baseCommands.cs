using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using ProgramManager;

namespace ProgramManager;

public class Base
{
    public static void Commands()
    {
        //--------------------------------- open ---------------------------------//

        if (Global.command.StartsWith("open "))
        {
            string input = Global.command.Substring(5);

            if (int.TryParse(input, out int value) &&
                value >=  1 &&
                value <= Global.items.Length)
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = Global.items[value - 1],
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($">> ERROR: {ex.Message} <<");
                }
            }
            else
            {
                Console.WriteLine(">> ERROR: INVALID NUMBER <<");
            }
        }

        //--------------------------------- code ---------------------------------//

        else if (Global.command.StartsWith("code "))
        {
            string input = Global.command.Substring(5);

            if (int.TryParse(input, out Global.value) &&
                Global.value >= 1 &&
                Global.value <= Global.items.Length)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "code",
                    Arguments = $"\"{Global.items[Global.value - 1]}\"",
                    UseShellExecute = true
                });
            }
        }

        //--------------------------------- open VS Code ---------------------------------//

        else if (Global.command == "code" || Global.command == "VS")
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "code",
                UseShellExecute = true
            });
        }

        //--------------------------------- /open ---------------------------------//

        else if (Global.command == "/open")
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = Global.path,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($">> ERROR: {ex.Message} <<");
            }
        }

        //--------------------------------- /code ---------------------------------//

        else if (Global.command == "/code")
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "code",
                    Arguments = $"\"{Global.path}\"",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($">> ERROR: {ex.Message} <<");
            }
        }


        //--------------------------------- cd ---------------------------------//

        else if (Global.command.StartsWith("cd "))
        {
            string input = Global.command.Substring(3);

            if (int.TryParse(input, out Global.value) &&
                Global.value >= 1 &&
                Global.value <= Global.dirs.Length)
            {
                Console.Clear();

                Global.path = Global.dirs[Global.value - 1];
                Paths.Show();
            }
            else
            {
                Console.WriteLine(">> ERROR: INVALID DIRECTORY NUMBER <<");
            }
        }

        //--------------------------------- return ---------------------------------//

        else if (Global.command == "return")
        {
            DirectoryInfo? parent = Directory.GetParent(Global.path);

            if (Global.path == @"C:\Users\fUJITSU\OneDrive\Desktop\programs")
            {
                Console.WriteLine(">> ERROR: already at root directory <<");
            }

            else if (parent != null)
            {
                Global.path = parent.FullName;

                Console.Clear();
                Paths.Show();
            }
        }

        //--------------------------------- return root ---------------------------------//

        else if (Global.command == "/return" || Global.command == "// root")
        {
            Global.path = @"C:\Users\fUJITSU\OneDrive\Desktop\programs";

            Console.Clear();
            Paths.Show();
        }

        //--------------------------------- clear ---------------------------------//

        else if (Global.command == "/clear")
        {
            Console.Clear();
            Paths.Show();
        }

        //--------------------------------- commands ---------------------------------//

        else if (Global.command == "// commands")
        {
            COMMANDS.Show();
        }

        //--------------------------------- Edit.Commands() ---------------------------------//

        else
        {
            Edit.Commands();
        }
    }
}