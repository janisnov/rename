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
            start();
        }
        private static void start()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\ntype \"exit\" to exit or \"start\" to start or restart");
            Console.ResetColor();
            switch (Console.ReadLine())
            {
                case "exit":
                    Environment.Exit(0);
                    break;

                case "start":
                    int action;
                    string path_to_files;// C:\\path\
                    Console.WriteLine("type path to file like \"C:\\\\path\\to\\file\\\"");
                    path_to_files = Console.ReadLine();
                    Console.WriteLine("type number of action \n1) rename by hash(md5) \n2) rename by hash and replace(md5)");
                    action = Convert.ToInt32(Console.ReadLine());
                    by_hash(path_to_files, action);
                    break;
            }
        }

        private static void by_hash(string path_to_files, int action)
        {
            IEnumerable<string> allfiles = Directory.EnumerateFiles(path_to_files);
            foreach (string filename in allfiles)//C:\\path\img.jpg
            {
                string out_path = Path.Combine(path_to_files, "out");//C:\\path\out
                string extention = Path.GetExtension(filename);//.jpg
                switch (action)
                {
                    case 1:
                        Directory.CreateDirectory(out_path);
                        string newfilename;//hash
                        extention = Path.GetExtension(filename);
                        newfilename = ComputeMD5Checksum(filename);
                        Console.WriteLine("some shit "+path_to_files + "\\" + newfilename + extention);

                        if (!File.Exists(out_path + "\\" + newfilename + extention))
                        {
                            File.Copy(filename, out_path + "\\" + newfilename + extention);
                            Console.Write(filename);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("  -> file successfully renamed in " + newfilename + extention + "!\n");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write(filename);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("  -> file already exist, with name "+newfilename+extention+"!\n");
                            Console.ResetColor();
                        }
                        break;
                    case 2:
                        string newfilename_2;
                        extention = Path.GetExtension(filename);
                        newfilename_2 = ComputeMD5Checksum(filename);

                        if (!File.Exists(path_to_files + "\\" + newfilename_2 + extention))
                        {
                            File.Copy(filename, path_to_files + "\\" + newfilename_2 + extention);
                            File.Delete(filename);
                            Console.Write(filename);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("  -> file successfully renamed in " + newfilename_2 + extention + "!\n");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write(filename);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("  -> file already exist, with name " + newfilename_2 + extention + "!\n");
                            Console.ResetColor();
                        }
                        break;
                }
            }
            start();
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
    }
}
