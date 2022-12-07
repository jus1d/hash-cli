using System.Security.Cryptography;
using System.Text;
using hash_cli.Hash;

namespace hash_cli;

public class Generator
{
    public static string ComputeHash(Algorithm algorithm, string rawData, bool isFile)
    {
        if (isFile)
        {
            switch (algorithm)
            {
                case Algorithm.Sha1:
                    return FileSha1(rawData);
                
                case Algorithm.Sha256:
                    return FileSha256(rawData);
                
                case Algorithm.Sha384:
                    return FileSha384(rawData);
                
                case Algorithm.Sha512:
                    return FileSha512(rawData);
                
                case Algorithm.Md5:
                    return FileMd5(rawData);
            }
        }
        else
        {
            switch (algorithm)
            {
                case Algorithm.Sha1:
                    return Sha1(rawData);
                
                case Algorithm.Sha256:
                    return Sha256(rawData);
                
                case Algorithm.Sha384:
                    return Sha384(rawData);
                
                case Algorithm.Sha512:
                    return Sha512(rawData);
                
                case Algorithm.Md5:
                    return Md5(rawData);
                
                case Algorithm.Keccak224:
                    return Keccak.ComputeHash(Algorithm.Keccak224, rawData);
                
                case Algorithm.Keccak256:
                    return Keccak.ComputeHash(Algorithm.Keccak256, rawData);
                
                case Algorithm.Keccak384:
                    return Keccak.ComputeHash(Algorithm.Keccak384, rawData);
                
                case Algorithm.Keccak512:
                    return Keccak.ComputeHash(Algorithm.Keccak512, rawData);
            }
        }

        return "Hash algorithm that you entered is unsupported";
    }
    
    static string Sha1(string rawData)
    { 
        using (SHA1 sha1 = SHA1.Create())
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(rawData);
            byte[] hashBytes = sha1.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes).ToLower();
        }
    }
        
    static string Sha256(string rawData)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(rawData);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes).ToLower();
        }
    }
        
    static string Sha384(string rawData)
    {
        using (SHA384 sha384 = SHA384.Create()) 
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(rawData);
            byte[] hashBytes = sha384.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes).ToLower();
        }
    }
        
    static string Sha512(string rawData)
    {
        using (SHA512 sha512 = SHA512.Create())
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(rawData);
            byte[] hashBytes = sha512.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes).ToLower();
        }
    }
        
    static string Md5(string rawData)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(rawData);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes).ToLower();
        }
    }
        
    static string FileSha1(string path)
    {
        using (FileStream stream = File.OpenRead(path))
        {
            var sha = SHA1.Create();
            byte[] checksum = sha.ComputeHash(stream);
            return BitConverter.ToString(checksum).Replace("-", String.Empty).ToLower();
        }
    }
        
    static string FileSha256(string path)
    {
        using (FileStream stream = File.OpenRead(path))
        {
            var sha = SHA256.Create();
            byte[] checksum = sha.ComputeHash(stream);
            return BitConverter.ToString(checksum).Replace("-", String.Empty).ToLower();
        }
    }
        
    static string FileSha384(string path)
    {
        using (FileStream stream = File.OpenRead(path))
        {
            var sha = SHA384.Create();
            byte[] checksum = sha.ComputeHash(stream);
            return BitConverter.ToString(checksum).Replace("-", String.Empty).ToLower();
        }
    }
        
    static string FileSha512(string path)
    {
        using (FileStream stream = File.OpenRead(path))
        {
            var sha = SHA512.Create();
            byte[] checksum = sha.ComputeHash(stream); 
            return BitConverter.ToString(checksum).Replace("-", String.Empty).ToLower();
        }
    }

    static string FileMd5(string path)
    {
        using (FileStream stream = File.OpenRead(path))
        {
            var md5 = MD5.Create();
            byte[] checksum = md5.ComputeHash(stream);
            return BitConverter.ToString(checksum).Replace("-", String.Empty).ToLower(); 
        }
    }
    
    
}