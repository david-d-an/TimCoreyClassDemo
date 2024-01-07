using System;
using System.Linq;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace GenericsDemo;

public class EncTestBouncyCastle
{
    public static byte[] EncryptAes(string input, string key)
    {
        byte[] plaintext = StringToByteArray(input);  // Encoding.UTF8.GetBytes(input);
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);

        // Create AES cipher with ECB mode and PKCS7 padding
        // NOPADDING
        IBufferedCipher cipher = CipherUtilities.GetCipher("AES/ECB/NOPADDING");
        cipher.Init(true, new KeyParameter(keyBytes));

        // Encrypt the plaintext
        byte[] ciphertext = cipher.DoFinal(plaintext);
        return ciphertext;
    }

    public static string DecryptAes(byte[] ciphertext, string key)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);

        // Create AES cipher with ECB mode and PKCS7 padding
        IBufferedCipher cipher = CipherUtilities.GetCipher("AES/ECB/NOPADDING");
        cipher.Init(false, new KeyParameter(keyBytes));

        // Decrypt the ciphertext
        byte[] decryptedBytes = cipher.DoFinal(ciphertext);

        // Convert the decrypted bytes to a string
        // string decryptedText = Encoding.UTF8.GetString(decryptedBytes);
        // return decryptedText;
        return GetHexString(decryptedBytes);
    }
    
    public static byte[] StringToByteArray(string hex) =>
        Enumerable.Range(0, hex.Length)
            .Where(x => x % 2 == 0)
            .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
            .ToArray();
    
    public static string GetHexString(byte[] byteArray) =>
        BitConverter.ToString(byteArray).Replace("-", "");
}