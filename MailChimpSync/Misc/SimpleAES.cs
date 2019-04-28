// <copyright file="SimpleAES.cs" company="Mark van de Veerdonk">
//     MailChimpSync - Synchronize a local data source with a MailChimp Audience
//     Copyright (C) 2019  Mark van de Veerdonk
//
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
//
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
//
//     You should have received a copy of the GNU General Public License
//     along with this program. If not, see &lt;https://www.gnu.org/licenses/&gt;
// </copyright>

using System;
using System.IO;
using System.Security.Cryptography;

/// <summary>
/// An easy to use encryption/decryption class using the AES algorithm.
/// </summary>
public class SimpleAES
{
    private readonly byte[] key = { 216, 4, 71, 120, 233, 136, 177, 109, 152, 39, 156, 80, 115, 74, 170, 14, 154, 139, 55, 255, 241, 230, 106, 195, 37, 199, 232, 81, 101, 85, 224, 235 };

    // a hardcoded IV should not be used for production AES-CBC code
    // IVs should be unpredictable per ciphertext
    private readonly byte[] vector = { 56, 251, 211, 144, 42, 166, 81, 193, 213, 235, 202, 195, 197, 63, 250, 176 };
    private readonly ICryptoTransform encryptorTransform;
    private readonly ICryptoTransform decryptorTransform;
    private System.Text.UTF8Encoding utfEncoder;

    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleAES"/> class.
    /// </summary>
    public SimpleAES()
    {
        // This is our encryption method
        RijndaelManaged rm = new RijndaelManaged();

        // Create an encryptor and a decryptor using our encryption method, key, and vector.
        encryptorTransform = rm.CreateEncryptor(key, vector);
        decryptorTransform = rm.CreateDecryptor(key, vector);

        // Used to translate bytes to text and vice versa
        utfEncoder = new System.Text.UTF8Encoding();
    }

    // -------------- Two Utility Methods (not used but may be useful) -----------

    /// <summary>
    /// Generates an encryption key.
    /// </summary>
    /// <returns>encryption key</returns>
    public static byte[] GenerateEncryptionKey()
    {
        // Generate a Key.
        RijndaelManaged rm = new RijndaelManaged();
        rm.GenerateKey();
        return rm.Key;
    }

    /// <summary>
    /// Generates a unique encryption vector.
    /// </summary>
    /// <returns>unique encryption vector</returns>
    public static byte[] GenerateEncryptionVector()
    {
        // Generate a Vector
        RijndaelManaged rm = new RijndaelManaged();
        rm.GenerateIV();
        return rm.IV;
    }

    // ----------- The commonly used methods ------------------------------

    /// <summary>
    /// Encrypt some text and return a string suitable for passing in a URL.
    /// </summary>
    /// <param name="textValue">The text value.</param>
    /// <returns>the encrypted string</returns>
    public string EncryptToString(string textValue)
    {
        return ByteArrToString(Encrypt(textValue));
    }

    /// <summary>
    /// Encrypt some text and return an encrypted byte array.
    /// </summary>
    /// <param name="textValue">The text value.</param>
    /// <returns>The encrypted byte array</returns>
    public byte[] Encrypt(string textValue)
    {
        // Translates our text value into a byte array.
        byte[] bytes = utfEncoder.GetBytes(textValue);

        // Used to stream the data in and out of the CryptoStream.
        MemoryStream memoryStream = new MemoryStream();

        /*
         * We will have to write the unencrypted bytes to the stream,
         * then read the encrypted result back from the stream.
         */
        CryptoStream cs = new CryptoStream(memoryStream, encryptorTransform, CryptoStreamMode.Write);
        cs.Write(bytes, 0, bytes.Length);
        cs.FlushFinalBlock();

        memoryStream.Position = 0;
        byte[] encrypted = new byte[memoryStream.Length];
        memoryStream.Read(encrypted, 0, encrypted.Length);

        // Clean up.
        cs.Close();
        memoryStream.Close();

        return encrypted;
    }

    /// <summary>
    /// Decrypts the string.
    /// </summary>
    /// <param name="encryptedString">The encrypted string.</param>
    /// <returns>the unencrypted string</returns>
    public string DecryptString(string encryptedString)
    {
        return Decrypt(StrToByteArray(encryptedString));
    }

    /// <summary>
    /// Decrypts the specified encrypted value.
    /// </summary>
    /// <param name="encryptedValue">The encrypted value.</param>
    /// <returns>The decrypted string</returns>
    public string Decrypt(byte[] encryptedValue)
    {
        MemoryStream encryptedStream = new MemoryStream();
        CryptoStream decryptStream = new CryptoStream(encryptedStream, decryptorTransform, CryptoStreamMode.Write);
        decryptStream.Write(encryptedValue, 0, encryptedValue.Length);
        decryptStream.FlushFinalBlock();

        encryptedStream.Position = 0;
        byte[] decryptedBytes = new byte[encryptedStream.Length];
        encryptedStream.Read(decryptedBytes, 0, decryptedBytes.Length);
        encryptedStream.Close();

        return utfEncoder.GetString(decryptedBytes);
    }

    /// <summary>
    /// Strings to byte array.
    ///     Convert a string to a byte array.  NOTE: Normally we'd create a Byte Array from a string using an ASCII encoding (like so).
    ///     System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
    ///     return encoding.GetBytes(str);
    ///
    /// However, this results in character values that cannot be passed in a URL.So, instead, I just
    /// lay out all of the byte values in a long string of numbers (three per - must pad numbers less than 100).
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns>the byte array</returns>
    /// <exception cref="Exception">Invalid string value in StrToByteArray</exception>
    public byte[] StrToByteArray(string str)
    {
        if (str.Length == 0)
        {
            throw new Exception("Invalid string value in StrToByteArray");
        }

        byte val;
        byte[] byteArr = new byte[str.Length / 3];
        int i = 0;
        int j = 0;
        do
        {
            val = byte.Parse(str.Substring(i, 3));
            byteArr[j++] = val;
            i += 3;
        }
        while (i < str.Length);
        return byteArr;
    }

    /// <summary>
    /// Converts a byte array to a string.
    /// Same comment as above.  Normally the conversion would use an ASCII encoding in the other direction:
    ///     System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
    ///     return enc.GetString(byteArr);
    /// </summary>
    /// <param name="byteArr">The byte arr.</param>
    /// <returns>the string</returns>
    public string ByteArrToString(byte[] byteArr)
    {
        byte val;
        string tempStr = string.Empty;
        for (int i = 0; i <= byteArr.GetUpperBound(0); i++)
        {
            val = byteArr[i];
            if (val < 10)
            {
                tempStr += "00" + val.ToString();
            }
            else if (val < 100)
            {
                tempStr += "0" + val.ToString();
            }
            else
            {
                tempStr += val.ToString();
            }
        }
        return tempStr;
    }
}