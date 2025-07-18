namespace CoreFlow.Application.Interfaces;

public interface IEncryptionService
{
    byte[] Encrypt(string plainText);
    string Decrypt(byte[] cipherBytes);
}