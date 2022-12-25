using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.MigrationRunner.Databases.DatabaseContext.Data.PhotoCategories;

[ExcludeFromCodeCoverage]
public static class PhotoCategories
{
    public static readonly ImmutableDictionary<string, string> Categories = ImmutableDictionary
        .Create<string, string>().AddRange(   
            new []
            {
                new KeyValuePair<string, string>("7e96ecf7-79ed-4316-bba4-767139ab49f3", "Uncategorised"),
                new KeyValuePair<string, string>("f09f61d3-486a-4702-ba51-d82b5a546725", "Abstract"),
                new KeyValuePair<string, string>("ae8a4167-a5ee-492b-a00c-db5d8ac3f628", "Aerial"),
                new KeyValuePair<string, string>("0c7292a1-f198-4b0b-81f1-bf89b29ac141", "Animals"),
                new KeyValuePair<string, string>("a5217144-5e84-4618-9dc9-a60427e83e7e", "Black and White"),
                new KeyValuePair<string, string>("de5abfea-2980-4ac9-bdea-4c760fde30e7", "Boudoir"),
                new KeyValuePair<string, string>("cb889f63-3e4b-4cc8-9627-169b5705a58d", "Celebrities"),
                new KeyValuePair<string, string>("833bc143-63e0-4f4f-83c4-a54ca6a2fce8", "City & Architecture"),
                new KeyValuePair<string, string>("51e8046c-f81d-442b-8fb9-84bdc8ceefa3", "Commercial"),
                new KeyValuePair<string, string>("bd65f8c6-ac3a-4b1e-be6c-16f672b95e4c", "Concert"),
                new KeyValuePair<string, string>("86257200-c5c2-4869-968f-92a4c6ec4114", "Family"),
                new KeyValuePair<string, string>("c049711b-a8fa-4941-9315-aa4d939260c5", "Fashion"),
                new KeyValuePair<string, string>("4d3c62d7-d309-44d1-9ec6-49bf0e9096eb", "Film"),
                new KeyValuePair<string, string>("2339611b-83f6-4be8-923f-d9b6a934a941", "Fine Art"),
                new KeyValuePair<string, string>("48be9bbf-a683-4b75-b914-a5ffbb6f5abd", "Food"),
                new KeyValuePair<string, string>("32d03c60-1ec9-4097-871e-4322ffa31ed7", "Journalism"),
                new KeyValuePair<string, string>("91a9978c-53f9-45bd-980e-edf80c5b16de", "Landscapes"),
                new KeyValuePair<string, string>("b7f6ddd1-a8a1-48fe-8529-6df67b76d8d1", "Macro"),
                new KeyValuePair<string, string>("aa3a2b27-7926-472d-af18-b48df160964b", "Nature"),
                new KeyValuePair<string, string>("8ee1d8f8-e3d7-4362-81d3-884e9e841cce", "Night"),
                new KeyValuePair<string, string>("f03a8dd5-bd2b-472b-825d-e0b5d75eb27f", "Nude"),
                new KeyValuePair<string, string>("e0fb2d49-0f4b-487a-aaf6-7fc2d162835b", "People"),
                new KeyValuePair<string, string>("a71b5d01-6c23-4749-870e-a13f3d6a0252", "Performing Arts"),
                new KeyValuePair<string, string>("f4d9f044-01e2-46df-ab7c-06f54473d449", "Sport"),
                new KeyValuePair<string, string>("ebcdaedf-e9c0-479d-b88d-d987212b0ba2", "Still Life"),
                new KeyValuePair<string, string>("ab5135ae-a003-42f2-87e2-69c342b39ea3", "Street"),
                new KeyValuePair<string, string>("9092803d-3b0b-488c-98de-3dac1cc739ba", "Transportation"),
                new KeyValuePair<string, string>("39af22f2-791e-4af7-a599-ee0e3bf542d1", "Travel"),
                new KeyValuePair<string, string>("7cbcd170-db19-437b-a29c-969814543774", "Underwater"),
                new KeyValuePair<string, string>("6826e4cb-acb1-4dde-b5b8-82f155f81e9b", "Urban Exploration"),
                new KeyValuePair<string, string>("f32ae05c-2e1f-4d54-bb03-5c73de775c57", "Wedding"),
            }
        );

    public static readonly DateTime CreatedAt = DateTime.Parse("2021-05-04 10:12:44");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}