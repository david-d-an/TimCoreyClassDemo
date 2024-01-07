using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace GenericsDemo;

public class EncTest
{
    private const int KeySize = 128;
    private const int BlockSize = 128;
    public static string? Key { get; set; }

    public static byte[]? KeyByte => Key == null ? null : Encoding.UTF8.GetBytes(Key);
    // ConvertHexStringToByteArray(Key);

    public static byte[] EncryptByteToByte(string plainText) =>
        EncryptByteToByte(ConvertHexStringToByteArray(plainText));

    public static byte[] EncryptByteToByte(byte[] plainTextByte)
    {
        if (KeyByte == null || KeyByte.Length == 0)
            throw new InvalidDataException("Key bytes are missing");

        using var aes = BuildAes(KeyByte);
        ICryptoTransform encryptor = aes.CreateEncryptor();

        byte[] encryptedBytes = encryptor.TransformFinalBlock(plainTextByte, 0, plainTextByte.Length);
        return encryptedBytes;
    }

    public static byte[] DecryptByteToByte(string cipher) =>
        DecryptByteToByte(ConvertHexStringToByteArray(cipher));

    public static byte[] DecryptByteToByte(byte[] cipherByte)
    {
        if (KeyByte == null || KeyByte.Length == 0)
            throw new InvalidDataException("Key bytes are missing");

        using var aes = BuildAes(KeyByte);
        ICryptoTransform transform = aes.CreateDecryptor();

        byte[] decryptedBytes = transform.TransformFinalBlock(cipherByte, 0, cipherByte.Length);
        return decryptedBytes;
    }

    private static Aes BuildAes(byte[] key)
    {
        var aes = Aes.Create();
        aes.Mode = CipherMode.ECB;
        aes.Padding = PaddingMode.None;
        aes.KeySize = KeySize;
        aes.BlockSize = BlockSize;
        aes.Key = key;
        return aes;
    }

    private static byte[] ConvertHexStringToByteArray(string hexString)
    {
        if (hexString.Length % 2 != 0)
            throw new ArgumentException(
                "Input string must have an even number of characters.");

        byte[] byteArray = new byte[hexString.Length / 2];

        for (int i = 0; i < hexString.Length; i += 2)
        {
            string hexByte = hexString.Substring(i, 2);
            byteArray[i / 2] = Convert.ToByte(hexByte, 16);
        }

        return byteArray;
    }
 }
