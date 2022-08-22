using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace renamer_cSharp
{
    class Program
    {

        static void Main(string[] args)
        {
            string arg_0 = args[0].Remove(0, 1);//path to file
            if (!arg_0.EndsWith("\\"))
            {
                arg_0 = arg_0.Insert(arg_0.Length, "\\");
            }
            string arg_1 = args[1].Remove(0, 1);//hash method
            string arg_2 = args[2].Remove(0, 1);//replace files or create subfolder
            string arg_3 = args[3].Remove(0, 1);//start application with control or without
            string arg_4 = args[4].Remove(0, 1);//*only for replace mod. delete file if already exist 
            Console.WriteLine("path to files -> " + arg_0);
            Console.WriteLine("hash method -> " + arg_1);
            Console.WriteLine("new/replace -> " + arg_2);
            Console.WriteLine("start with control -> " + arg_3);
            Console.WriteLine("*only for replace mod. delete file if already exist -> " + arg_4);
            if (arg_3 == "yes")
            {
                Console.WriteLine("press <enter> to continue or <esc> to return");
                while (true)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                    else if (key.Key == ConsoleKey.Escape)
                    {
                        Environment.Exit(100);
                    }
                }
            }

            if (arg_2 == "new")
            {
                rename_new(arg_0, arg_1);
            }
            else if (arg_2 == "replace")
            {
                rename_replace(arg_0, arg_1, arg_4);
            }

        }

        private static void rename_new(string path_to_file, string hash_method)
        {
            IEnumerable<string> allfiles = Directory.EnumerateFiles(path_to_file);
            string out_path = Path.Combine(path_to_file, "out\\");//C:\\path\out
            string extention;
            if (!Directory.Exists(out_path))
            {
                Directory.CreateDirectory(out_path);
            }
            else
            {
                Console.WriteLine("out folder already exist, continue? press <enter> or <esc>");
                while (true)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                    else if (key.Key == ConsoleKey.Escape)
                    {
                        Environment.Exit(101);
                    }
                }
            }
            DateTime one = DateTime.Now;
            switch (hash_method)
            {
                case "MD5":
                    foreach (string filename in allfiles)//C:\\path\img.jpg
                    {
                        string hashfilename;//hash
                        extention = Path.GetExtension(filename);
                        hashfilename = ComputeMD5Checksum(filename);
                        //Console.WriteLine(path_to_file + hashfilename + extention);

                        if (!File.Exists(out_path + hashfilename + extention))
                        {
                            File.Copy(filename, out_path + hashfilename + extention);
                            Console.Write(filename);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("  -> file successfully renamed in " + hashfilename + extention + "!\n\n");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write(filename);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("  -> file already exist, with name " + hashfilename + extention + "!\n\n");
                            Console.ResetColor();
                        }
                    }
                    break;

                case "SHA256":
                    foreach (string filename in allfiles)//C:\\path\img.jpg
                    {
                        string hashfilename;//hash
                        extention = Path.GetExtension(filename);
                        hashfilename = ComputeSHA256Checksum(filename);
                        //Console.WriteLine(path_to_file + hashfilename + extention);

                        if (!File.Exists(out_path + hashfilename + extention))
                        {
                            File.Copy(filename, out_path + hashfilename + extention);
                            Console.Write(filename);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("  -> file successfully renamed in " + hashfilename + extention + "!\n\n");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write(filename);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("  -> file already exist, with name " + hashfilename + extention + "!\n\n");
                            Console.ResetColor();
                        }
                    }
                    break;

            }

            DateTime two = DateTime.Now;
            TimeSpan result = two - one;
            Console.WriteLine("elapsed time -> " + result);
            Console.ReadKey();
        }

        private static void rename_replace(string path_to_file, string hash_method, string arg_4)
        {
            IEnumerable<string> allfiles = Directory.EnumerateFiles(path_to_file);

            string extention;
            DateTime one = DateTime.Now;
            switch (hash_method)
            {
                case "MD5":
                    foreach (string filename in allfiles)//C:\\path\img.jpg
                    {
                        string hashfilename;//hash
                        extention = Path.GetExtension(filename);
                        hashfilename = ComputeMD5Checksum(filename);
                        //Console.WriteLine(path_to_file + hashfilename + extention);

                        if (!File.Exists(path_to_file + hashfilename + extention))
                        {
                            File.Copy(filename, path_to_file + hashfilename + extention);
                            File.Delete(filename);
                            Console.Write(filename);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("  -> file successfully renamed in " + hashfilename + extention + "!\n\n");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write(filename);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("  -> file already exist, with name " + hashfilename + extention + "!\n");
                            Console.ResetColor();
                            if (arg_4 == "yes")
                            {
                                File.Delete(filename);
                                Console.WriteLine("file deleted!\n\n");
                            }
                        }
                    }
                    break;

                case "SHA256":
                    foreach (string filename in allfiles)//C:\\path\img.jpg
                    {
                        string hashfilename;//hash
                        extention = Path.GetExtension(filename);
                        hashfilename = ComputeSHA256Checksum(filename);
                        //Console.WriteLine(path_to_file + hashfilename + extention);

                        if (!File.Exists(path_to_file + hashfilename + extention))
                        {
                            File.Copy(filename, path_to_file + hashfilename + extention);
                            File.Delete(filename);
                            Console.Write(filename);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("  -> file successfully renamed in " + hashfilename + extention + "!\n\n");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write(filename);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("  -> file already exist, with name " + hashfilename + extention + "!\n");
                            Console.ResetColor();
                            if (arg_4 == "yes")
                            {
                                File.Delete(filename);
                                Console.WriteLine("file deleted!\n\n");
                            }
                        }
                    }
                    break;

            }

            DateTime two = DateTime.Now;
            TimeSpan result = two - one;
            Console.WriteLine("elapsed time -> " + result);
            Console.ReadKey();
        }

        private static string ComputeMD5Checksum(string path)
        {
            using (FileStream fs = System.IO.File.OpenRead(path))
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] fileData = new byte[fs.Length];
                fs.Read(fileData, 0, (int)fs.Length);
                byte[] checkSum = md5.ComputeHash(fileData);
                string result = BitConverter.ToString(checkSum).Replace("-", String.Empty);
                return result;
            }
        }

        private static string ComputeSHA256Checksum(string path)
        {
            using (FileStream fs = System.IO.File.OpenRead(path))
            {
                SHA256 SHA256 = new SHA256CryptoServiceProvider();
                byte[] fileData = new byte[fs.Length];
                fs.Read(fileData, 0, (int)fs.Length);
                byte[] checkSum = SHA256.ComputeHash(fileData);
                string result = BitConverter.ToString(checkSum).Replace("-", String.Empty);
                return result;
            }
        }
    }
}
