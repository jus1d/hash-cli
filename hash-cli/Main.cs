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
                WriteColored("Usage: hash-cli [ hash-algorithm ] [ raw-data ]\n" +
                                        "                                   ", FlagColor, "--file", " [ file_path ]\n" +
                                        "                ", FlagColor, "--checksum", " [ hash-algorithm ] [ file-path ] [ file-hash-sum-path ]\n" + 
                                        "                ", FlagColor, "--checksum", " [ hash-algorithm ] [ file-path ] ", FlagColor, "--hash", " [ hash-sum ]\n");
                return;
            }

            if (args[0] == "--checksum" || args[0] == "-cs")
            {
                string algorithmString = args[1];
                Algorithm algorithm = Algorithm.Sha1;
                string path = args[2];
                string hashPath = args[3];

                string computedHash;

                if (hashPath == "--hash" || hashPath == "-h")
                {
                    computedHash = args[4];
                    hashPath = computedHash.Substring(0, 2) + ".." + computedHash.Substring(computedHash.Length - 2, 2);
                }
                else
                {
                    computedHash = File.ReadAllText(hashPath);
                }


                switch (algorithmString)
                {
                    case "sha1":
                        algorithm = Algorithm.Sha1;
                        break;
                    case "sha256":
                        algorithm = Algorithm.Sha256;
                        break;
                    case "sha384":
                        algorithm = Algorithm.Sha384;
                        break;
                    case "sha512":
                        algorithm = Algorithm.Sha512;
                        break;
                    case "keccak224":
                        algorithm = Algorithm.Keccak224;
                        break;
                    case "keccak256":
                        algorithm = Algorithm.Keccak256;
                        break;
                    case "keccak384":
                        algorithm = Algorithm.Keccak384;
                        break;
                    case "keccak512":
                        algorithm = Algorithm.Keccak512;
                        break;
                    case "md5":
                        algorithm = Algorithm.Md5;
                        break;
                }
                
                string hash = Compute(algorithm, path, true);
                bool checksum = hash == computedHash;

                if (checksum)
                {
                    WriteColored(SuccessColor, "\nChecksum matches\n");
                    LogHash(hashPath, algorithmString, hash);
                }
                else
                {
                    WriteColored(ErrorColor, "\nChecksum does not match\n");
                    LogHash(hashPath, algorithmString, hash);
                    WriteColored(FlagColor, "\nchecksum -> ", $"{computedHash}");
                }

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
        WriteColored("\nMissing argument: use ", FlagColor, "--help", " or ", FlagColor, "-h", " flags, to see usage list\n", ErrorColor, "Error: ", e.Message);
    }
}