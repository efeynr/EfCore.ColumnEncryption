namespace EfCore.ColumnEncryption.Interfaces;

public interface IColumnEncryptionProvider
{
    string? Encrypt(string? plaintext);
    string? Decrypt(string? ciphertext);
}