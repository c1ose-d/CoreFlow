namespace CoreFlow.Infrastructure.Services;

public class DpapiEncryptionService : IEncryptionService
{
    public byte[] Encrypt(string plainText)
    {
        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
        return ProtectedData.Protect(plainBytes, null, DataProtectionScope.CurrentUser);
    }

    public string Decrypt(byte[] cipherBytes)
    {
        byte[] plainBytes = ProtectedData.Unprotect(cipherBytes, null, DataProtectionScope.CurrentUser);
        return Encoding.UTF8.GetString(plainBytes);
    }
}