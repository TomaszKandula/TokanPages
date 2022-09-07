using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Persistence.Database.Initializer.Seeders;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Domain.Entities;

[ExcludeFromCodeCoverage]
public static class PhotoCategoriesSeeder
{
    public static IEnumerable<PhotoCategories> SeedPhotoCategories()
    {
        var categories = Data.PhotoCategories.PhotoCategories.Categories;
        var output = new List<PhotoCategories>(categories.Count);

        foreach (var item in categories)
        {
            output.Add(new PhotoCategories
            {
                Id = Guid.Parse(item.Key),
                CategoryName = item.Value,
                CreatedAt = Data.PhotoCategories.PhotoCategories.CreatedAt,
                CreatedBy = Data.PhotoCategories.PhotoCategories.CreatedBy,
                ModifiedAt = Data.PhotoCategories.PhotoCategories.ModifiedAt,
                ModifiedBy = Data.PhotoCategories.PhotoCategories.ModifiedBy
            });
        }

        return output;
    }
}