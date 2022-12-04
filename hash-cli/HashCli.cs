using System.Security.Cryptography;
using System.Text;
using static hash_cli.Algorithm;
using static hash_cli.Generator;

namespace hash_cli
{
    class HashCli
    {
        static void Main(string[] args)
        {
            try
            {
                if (args[0][0] == '-')
                {
                    string parameter = args[0].ToLower();
                    if (parameter == "--help" || parameter == "-h")
                    {
                        Console.WriteLine("\nUsage: hash-cli [ hash-algorithm ] [ raw-data ]\n" +
                                          "\n  or\n" +
                                          "\nUsage for hashing files: hash-cli [ hash-algorithm ] --file [ file-name ]\n" +
                                          "\nFile-name parameter may be clear => will opens a window for selecting a file\n");
                    }
                    else if (parameter == "--test" || parameter == "-t")
                    {
                        Tests.Run();
                    }
                }
                else
                {
                    string hashType = args[0].ToLower();
                    string rawData = args[1];
                    

                    switch (hashType)
                    {
                        case "sha256":

                            if (rawData == "--file" || rawData == "-f")
                            {

                                try
                                {
                                    string path = args[2];

                                    string hash = HashCompute(Sha256, path, true);

                                    LogHash($"file:{path}", hashType, hash);
                                }
                                catch (IndexOutOfRangeException e)
                                {
                                    WriteColor("Type path to file after ");
                                    WriteColor("--flag", ConsoleColor.DarkGray);
                                    WriteColor(" flag");
                                }
                            }
                            else
                            {
                                string hash = HashCompute(Sha256, rawData, false);
                            
                                LogHash(rawData, hashType, hash);
                            }
                            
                            break;
                        
                        case "md5":

                            if (rawData == "--file" || rawData == "-f")
                            {
                                try
                                {
                                    string path = args[2];

                                    string hash = HashCompute(Md5, path, true);
                                    
                                    LogHash($"file:{path}", hashType, hash);
                                }
                                catch (IndexOutOfRangeException e)
                                {
                                    WriteColor("Type path to file after ");
                                    WriteColor("--flag", ConsoleColor.DarkGray);
                                    WriteColor(" flag");
                                }
                            }
                            else
                            {
                                {
                                    string hash = HashCompute(Md5, rawData, false);
                                    
                                    LogHash(rawData, hashType, hash);
                                }
                            }
                            break;
                        
                        case "sha1":
                            
                            if (rawData == "--file" || rawData == "-f")
                            {
                                try
                                {
                                    string path = args[2];

                                    string hash = HashCompute(Sha1, path, true);
                                    
                                    LogHash($"file:{path}", hashType, hash);
                                }
                                catch (IndexOutOfRangeException e)
                                {
                                    WriteColor("Type path to file after ");
                                    WriteColor("--flag", ConsoleColor.DarkGray);
                                    WriteColor(" flag");
                                }
                            }
                            else
                            {
                                {
                                    string hash = HashCompute(Sha1, rawData, false);
                                    
                                    LogHash(rawData, hashType, hash);
                                }
                            }
                            break;
                        
                        case "sha384":
                            
                            if (rawData == "--file" || rawData == "-f")
                            {
                                try
                                {
                                    string path = args[2];

                                    string hash = HashCompute(Sha384, path, true);
                                    
                                    LogHash($"file:{path}", hashType, hash);
                                }
                                catch (IndexOutOfRangeException e)
                                {
                                    WriteColor("Type path to file after ");
                                    WriteColor("--flag", ConsoleColor.DarkGray);
                                    WriteColor(" flag");
                                }
                            }
                            else
                            {
                                {
                                    string hash = HashCompute(Sha384, rawData, false);
                                    
                                    LogHash(rawData, hashType, hash);
                                }
                            }
                            break;
                        
                        case "sha512":
                            
                            if (rawData == "--file" || rawData == "-f")
                            {
                                try
                                {
                                    string path = args[2];

                                    string hash = HashCompute(Sha512, path, true);
                                    
                                    LogHash($"file:{path}", hashType, hash);
                                }
                                catch (IndexOutOfRangeException e)
                                {
                                    WriteColor("Type path to file after ");
                                    WriteColor("--flag", ConsoleColor.DarkGray);
                                    WriteColor(" flag");
                                }
                            }
                            else
                            {
                                {
                                    string hash = HashCompute(Sha512, rawData, false);
                                    
                                    LogHash(rawData, hashType, hash);
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                WriteColor("Use ");
                WriteColor("--help");
                WriteColor(" or ");
                WriteColor("-h");
                WriteLineColor(" flags, to see usage list");
            }
        }

        static void LogHash(string rawData, string hashType, string hash)
        {
            WriteColor($"{rawData}:{hashType} -> ", ConsoleColor.DarkGray);
            WriteLineColor(hash);
        }

        public static void WriteColor(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }
        
        public static void WriteLineColor(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}