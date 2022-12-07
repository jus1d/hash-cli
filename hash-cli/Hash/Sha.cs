using System.Security.Cryptography;
using System.Text;

namespace hash_cli.Hash;

public class Sha
{
    public static string Compute(Algorithm algorithm, string rawData, bool isFile = false)
    {
        byte[] byteData;
        
        if (isFile)
            byteData = File.ReadAllBytes(rawData);
        else
            byteData = Encoding.ASCII.GetBytes(rawData);

        return algorithm switch
        {
            Algorithm.Sha1 => Sha1(byteData),
            Algorithm.Sha256 => Sha256(byteData),
            Algorithm.Sha384 => Sha384(byteData),
            Algorithm.Sha512 => Sha512(byteData),
            _ => "Unsupported hash algorithm"
        };
    }

    private static string Sha1(byte[] byteData)
    {
        byte[] hashBytes;
        
        SHA1 sha1 = SHA1.Create();
        hashBytes = sha1.ComputeHash(byteData);
        return Convert.ToHexString(hashBytes).ToLower();
    }
    
    private static string Sha256(byte[] byteData)
    {
        byte[] hashBytes;
        
        SHA256 sha256 = SHA256.Create();
        hashBytes = sha256.ComputeHash(byteData);
        return Convert.ToHexString(hashBytes).ToLower();
    }
    
    private static string Sha384(byte[] byteData)
    {
        byte[] hashBytes;
        
        SHA384 sha384 = SHA384.Create();
        hashBytes = sha384.ComputeHash(byteData);
        return Convert.ToHexString(hashBytes).ToLower();
    }
    
    private static string Sha512(byte[] byteData)
    {
        byte[] hashBytes;
        
        SHA512 sha512 = SHA512.Create();
        hashBytes = sha512.ComputeHash(byteData);
        return Convert.ToHexString(hashBytes).ToLower();
    }
}