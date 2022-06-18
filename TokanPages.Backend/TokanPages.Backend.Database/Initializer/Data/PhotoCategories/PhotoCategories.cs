namespace TokanPages.Backend.Database.Initializer.Data.PhotoCategories;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public static class PhotoCategories
{
    public static readonly Dictionary<string, string> Categories = new()
    {
        { "7e96ecf7-79ed-4316-bba4-767139ab49f3", "Uncategorised" },
        { "f09f61d3-486a-4702-ba51-d82b5a546725", "Abstract" },
        { "ae8a4167-a5ee-492b-a00c-db5d8ac3f628", "Aerial" },
        { "0c7292a1-f198-4b0b-81f1-bf89b29ac141", "Animals" },
        { "a5217144-5e84-4618-9dc9-a60427e83e7e", "Black and White" },
        { "de5abfea-2980-4ac9-bdea-4c760fde30e7", "Boudoir" },
        { "cb889f63-3e4b-4cc8-9627-169b5705a58d", "Celebrities" },
        { "833bc143-63e0-4f4f-83c4-a54ca6a2fce8", "City & Architecture" },
        { "51e8046c-f81d-442b-8fb9-84bdc8ceefa3", "Commercial" },
        { "bd65f8c6-ac3a-4b1e-be6c-16f672b95e4c", "Concert" },
        { "86257200-c5c2-4869-968f-92a4c6ec4114", "Family" },
        { "c049711b-a8fa-4941-9315-aa4d939260c5", "Fashion" },
        { "4d3c62d7-d309-44d1-9ec6-49bf0e9096eb", "Film" },
        { "2339611b-83f6-4be8-923f-d9b6a934a941", "Fine Art" },
        { "48be9bbf-a683-4b75-b914-a5ffbb6f5abd", "Food" },
        { "32d03c60-1ec9-4097-871e-4322ffa31ed7", "Journalism" },
        { "91a9978c-53f9-45bd-980e-edf80c5b16de", "Landscapes" },
        { "b7f6ddd1-a8a1-48fe-8529-6df67b76d8d1", "Macro" },
        { "aa3a2b27-7926-472d-af18-b48df160964b", "Nature" },
        { "8ee1d8f8-e3d7-4362-81d3-884e9e841cce", "Night" },
        { "f03a8dd5-bd2b-472b-825d-e0b5d75eb27f", "Nude" },
        { "e0fb2d49-0f4b-487a-aaf6-7fc2d162835b", "People" },
        { "a71b5d01-6c23-4749-870e-a13f3d6a0252", "Performing Arts" },
        { "f4d9f044-01e2-46df-ab7c-06f54473d449", "Sport" },
        { "ebcdaedf-e9c0-479d-b88d-d987212b0ba2", "Still Life" },
        { "ab5135ae-a003-42f2-87e2-69c342b39ea3", "Street" },
        { "9092803d-3b0b-488c-98de-3dac1cc739ba", "Transportation" },
        { "39af22f2-791e-4af7-a599-ee0e3bf542d1", "Travel" },
        { "7cbcd170-db19-437b-a29c-969814543774", "Underwater" },
        { "6826e4cb-acb1-4dde-b5b8-82f155f81e9b", "Urban Exploration" },
        { "f32ae05c-2e1f-4d54-bb03-5c73de775c57", "Wedding" }
    };

    public static readonly DateTime CreatedAt = DateTime.Parse("2021-05-04 10:12:44");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}