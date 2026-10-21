using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using ProgramManager;

namespace ProgramManager;

public class Edit
{
    private static void RemoveAttributes(string path)
    {
        DirectoryInfo dir = new DirectoryInfo(path);

        dir.Attributes = FileAttributes.Normal;

        foreach (FileInfo file in dir.GetFiles("*", SearchOption.AllDirectories))
        {
            file.Attributes = FileAttributes.Normal;
        }

        foreach (DirectoryInfo subDir in dir.GetDirectories("*", SearchOption.AllDirectories))
        {
            subDir.Attributes = FileAttributes.Normal;
        }
    }

    public static void Commands()
    {
        //--------------------------------- create directory ---------------------------------//

        if (Global.command.StartsWith("dir.new "))
        {
            string name = Global.command.Substring(8);

            if (name.StartsWith("\\") && name.EndsWith("\\"))
            {
                name = name.Trim('\\', ' ');
                Directory.CreateDirectory(Global.path + "\\" + name);

                Console.Clear();
                Paths.Show();
            }
            else
                Console.WriteLine("! invalid command structure: missing?  '\\ ' ; ' \\'  !");
        }

        //--------------------------------- create file ---------------------------------//

        else if (Global.command.StartsWith("file.new "))
        {
            string name = Global.command.Substring(9);

            if (name.StartsWith("\\") && name.EndsWith("\\"))
            {
                name = name.Trim('\\', ' ');

                File.Create(Global.path + "\\" + name).Dispose();

                Console.Clear();
                Paths.Show();
            }
            else
                Console.WriteLine("! invalid command structure: missing?  '\\ ' ; ' \\'  !");
        }

        //--------------------------------- delete ---------------------------------//

        else if (Global.command.StartsWith("delete ") ||
                Global.command.StartsWith("del "))
        {
            string input;

            if (Global.command.StartsWith("delete "))
            {
                input = Global.command.Substring(7);
            }
            else
            {
                input = Global.command.Substring(4);
            }

            if (int.TryParse(input, out int value) &&
                value >= 1 &&
                value <= Global.items.Length)
            {
                string item = Global.items[value - 1];

                try
                {
                    if (Directory.Exists(item))
                    {
                        RemoveAttributes(item);
                        Directory.Delete(item, true);
                    }
                    else if (File.Exists(item))
                    {
                        File.SetAttributes(item, FileAttributes.Normal);
                        File.Delete(item);
                    }

                    Console.Clear();
                    Paths.Show();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($">> ERROR: {ex.Message} <<");
                }
            }
            else
            {
                Console.WriteLine(">> ERROR: INVALID ITEM NUMBER <<");
            }
        }

        //--------------------------------- delete current directory ---------------------------------//

        else if (Global.command == "/delete")
        {
            if (Global.path == @"C:\Users\fUJITSU\OneDrive\Desktop\programs")
            {
                Console.WriteLine(">> ERROR: cannot delete root directory <<");
            }
            else
            {
                DirectoryInfo? parent = Directory.GetParent(Global.path);

                if (parent != null)
                {
                    try
                    {
                        RemoveAttributes(Global.path);
                        Directory.Delete(Global.path, true);

                        Global.path = parent.FullName;

                        Console.Clear();
                        Paths.Show();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($">> ERROR: {ex.Message} <<");
                    }
                }
            }
        }

        //--------------------------------- delete current directory content ---------------------------------//

        else if (Global.command == "delete .")
        {
            try
            {
                foreach (string file in Directory.GetFiles(Global.path))
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                }

                foreach (string directory in Directory.GetDirectories(Global.path))
                {
                    RemoveAttributes(directory);
                    Directory.Delete(directory, true);
                }

                Console.Clear();
                Paths.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine($">> ERROR: {ex.Message} <<");
            }
        }

        //--------------------------------- rename ---------------------------------//

        else if (Global.command.StartsWith("rename "))
        {
            string input = Global.command.Substring(7);

            string[] parts = input.Split(' ', 2);

            if 
            (
                parts.Length == 2 &&
                int.TryParse(parts[0], out int value) &&
                value >= 1 &&
                value <= Global.items.Length
            )
            {
                string newName = parts[1];

                if (newName.StartsWith("\\") && newName.EndsWith("\\"))
                {
                    newName = newName.Trim('\\', ' ');

                    string oldPath = Global.items[value - 1];
                    string newPath = Path.Combine(Global.path, newName);

                    try
                    {
                        if (Directory.Exists(oldPath))
                        {
                            Directory.Move(oldPath, newPath);
                        }
                        else if (File.Exists(oldPath))
                        {
                            File.Move(oldPath, newPath);
                        }

                        Console.Clear();
                        Paths.Show();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($">> ERROR: {ex.Message} <<");
                    }
                }
                else
                {
                    Console.WriteLine("! invalid command structure: missing?  '\\ ' ; ' \\'  !");
                }
            }
            else
            {
                Console.WriteLine(">> ERROR: INVALID COMMAND <<");
            }
        }

        //--------------------------------- Error ---------------------------------//

        else
        {
            Console.WriteLine("! invalid command !");
        }
    }
}