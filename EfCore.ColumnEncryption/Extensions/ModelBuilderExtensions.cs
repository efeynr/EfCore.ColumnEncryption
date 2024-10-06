using EfCore.ColumnEncryption.Attributes;
using EfCore.ColumnEncryption.Converters;
using EfCore.ColumnEncryption.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EfCore.ColumnEncryption.Extensions;

public static class ModelBuilderExtensions
{
    public static void UseColumnEncryption(this ModelBuilder modelBuilder, IColumnEncryptionProvider encryptionProvider)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);
        ArgumentNullException.ThrowIfNull(encryptionProvider);

        var stringProperties = modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetProperties())
            .Where(p =>
                p.ClrType == typeof(string) &&
                !p.IsPrimaryKey() &&
                !p.IsForeignKey());
        foreach (var property in stringProperties)
        {
            var propertyInfo = property.PropertyInfo;

            if (propertyInfo != null && Attribute.IsDefined(propertyInfo, typeof(EncryptedAttribute)))
            {
                var converter = new ColumnEncryptionConverter(encryptionProvider);
                property.SetValueConverter(converter);
            }
        }
    }
    //TODO -> we may get key instead of encryptionProvider.
}