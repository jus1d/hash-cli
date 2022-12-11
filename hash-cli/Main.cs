using System.Net.Mime;
using hash_cli.Hash;

namespace hash_cli;

public class HashProgram
{
    public const ConsoleColor FlagColor = ConsoleColor.DarkGray;
    public const ConsoleColor ErrorColor = ConsoleColor.DarkRed;
    public const ConsoleColor SuccessColor = ConsoleColor.Green;
    
    static void Main(string[] args)
    {
        string? pathEnv = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine);
        bool pathExist = pathEnv!.Contains(Environment.CurrentDirectory);

        if (!pathExist)
        {
            WriteColored("Global variable ", FlagColor, "PATH", " isn't contains hash-cli path");
            
            string newPath = pathEnv + ";" + Environment.CurrentDirectory;
            Environment.SetEnvironmentVariable("PATH",newPath, EnvironmentVariableTarget.Machine);
            
            WriteColored("Global variable ", FlagColor, "PATH", " successfully updated");
        }
        
        Start(args);
    }

    static void Start(string[] args)
    {
        try
        {
            if (args.Length == 0)
            {
                WriteColored("Use ", FlagColor, "--help", " or ", FlagColor, "-h", " flags, to see usage list\n");
                return;
            }
            if (args[0] == "--test" || args[0] == "-t")
            {
                Tests.Run();
                return;
            }
            if (args[0] == "--help" || args[0] == "-h")
            {
                WriteColored("\nUsage: hash-cli [ hash-algorithm ] [ raw-data ]\n", FlagColor,
                    "\n  or\n",
                    "\nUsage for hashing files: hash-cli [ hash-algorithm ] ", FlagColor,  "--file", " [ file-name ]\n" +
                    "\nFile-name parameter may be clear => will opens a window for selecting a file\n");
                return;
            }

            string hashType = args[0];
            string rawData = args[1];
            if (rawData == "--file" || rawData == "-f")
            {
                string path = args[2];

                switch (hashType)
                {
                    case "md5":
                        LogHash($"file:{path}", hashType, Md.Compute(Algorithm.Md5, path, true));
                        break;
                    case "sha1":
                        LogHash($"file:{path}", hashType, Sha.Compute(Algorithm.Sha1, path, true));
                        break;
                    case "sha256":
                        LogHash($"file:{path}", hashType, Sha.Compute(Algorithm.Sha256, path, true));
                        break;
                    case "sha384":
                        LogHash($"file:{path}", hashType, Sha.Compute(Algorithm.Sha384, path, true));
                        break;
                    case "sha512":
                        LogHash($"file:{path}", hashType, Sha.Compute(Algorithm.Sha512, path, true));
                        break;
                    case "keccak224":
                        LogHash($"file:{path}", hashType, Keccak.Compute(Algorithm.Keccak224, path, true));
                        break;
                    case "keccak256":
                        LogHash($"file:{path}", hashType, Keccak.Compute(Algorithm.Keccak256, path, true));
                        break;
                    case "keccak384":
                        LogHash($"file:{path}", hashType, Keccak.Compute(Algorithm.Keccak384, path, true));
                        break;
                    case "keccak512":
                        LogHash($"file:{path}", hashType, Keccak.Compute(Algorithm.Keccak512, path, true));
                        break;
                }
            }
            else
            {
                switch (hashType)
                {
                    case "md5":
                        LogHash(rawData, hashType, Md.Compute(Algorithm.Md5, rawData));
                        break;
                    case "sha1":
                        LogHash(rawData, hashType, Sha.Compute(Algorithm.Sha1, rawData));
                        break;
                    case "sha256":
                        LogHash(rawData, hashType, Sha.Compute(Algorithm.Sha256, rawData));
                        break;
                    case "sha384":
                        LogHash(rawData, hashType, Sha.Compute(Algorithm.Sha384, rawData));
                        break;
                    case "sha512":
                        LogHash(rawData, hashType, Sha.Compute(Algorithm.Sha512, rawData));
                        break;
                    case "keccak224":
                        LogHash(rawData, hashType, Keccak.Compute(Algorithm.Keccak224, rawData));
                        break;
                    case "keccak256":
                        LogHash(rawData, hashType, Keccak.Compute(Algorithm.Keccak256, rawData));
                        break;
                    case "keccak384":
                        LogHash(rawData, hashType, Keccak.Compute(Algorithm.Keccak384, rawData));
                        break;
                    case "keccak512":
                        LogHash(rawData, hashType, Keccak.Compute(Algorithm.Keccak512, rawData));
                        break;
                }
            }
        }
        catch (Exception e)
        {
            ErrorHandler(e);
        }


    }

    public static string Compute(Algorithm algorithm, string rawData, bool isFile)
    {
        return algorithm switch
        {
            Algorithm.Sha1 => Sha.Compute(Algorithm.Sha1, rawData, isFile),
            Algorithm.Sha256 => Sha.Compute(Algorithm.Sha256, rawData, isFile),
            Algorithm.Sha384 => Sha.Compute(Algorithm.Sha384, rawData, isFile),
            Algorithm.Sha512 => Sha.Compute(Algorithm.Sha512, rawData, isFile),
            Algorithm.Keccak224 => Keccak.Compute(Algorithm.Keccak224, rawData, isFile),
            Algorithm.Keccak256 => Keccak.Compute(Algorithm.Keccak256, rawData, isFile),
            Algorithm.Keccak384 => Keccak.Compute(Algorithm.Keccak384, rawData, isFile),
            Algorithm.Keccak512 => Keccak.Compute(Algorithm.Keccak512, rawData, isFile),
            Algorithm.Md5 => Md.Compute(Algorithm.Md5, rawData, isFile),
            _ => "Unsupported hash algorithm"
        };
    }
    
    static void LogHash(string rawData, string hashType, string hash)
    {
        WriteColored(FlagColor, $"{rawData}:{hashType} -> ", hash);
    }

    public static void WriteColored(params object[] list)
    {
        foreach (var obj in list)
        {
            if (obj is ConsoleColor)
            {
                Console.ForegroundColor = (ConsoleColor)obj;
            }
            else if (obj is string)
            {
                Console.Write(obj);
                Console.ResetColor();
            }
        }
            
        Console.WriteLine();
    }

    public static void ErrorHandler(Exception e)
    {
        WriteColored("\nUse ", FlagColor, "--help", " or ", FlagColor, "-h", " flags, to see usage list\n", ErrorColor, "Error: ", e.Message);
    }
}