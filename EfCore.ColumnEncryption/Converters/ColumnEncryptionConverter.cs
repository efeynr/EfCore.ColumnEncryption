using EfCore.ColumnEncryption.Interfaces;

namespace EfCore.ColumnEncryption.Converters;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class ColumnEncryptionConverter(IColumnEncryptionProvider encryptionProvider) : ValueConverter<string?, string?>(
    v => encryptionProvider.Encrypt(v),
    v => encryptionProvider.Decrypt(v));