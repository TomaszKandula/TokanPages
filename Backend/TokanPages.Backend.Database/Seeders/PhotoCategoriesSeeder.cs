using System.Collections.Generic;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Seeders
{
    public static class PhotoCategoriesSeeder
    {
        public static IEnumerable<PhotoCategories> SeedPhotoCategories()
        {
            return new List<PhotoCategories>
            {
                new PhotoCategories
                {
                    CategoryName = "Uncategorised"
                },
                new PhotoCategories
                {
                    CategoryName = "Abstract"
                },
                new PhotoCategories
                {
                    CategoryName = "Aerial"
                },
                new PhotoCategories
                {
                    CategoryName = "Animals"
                },
                new PhotoCategories
                {
                    CategoryName = "Black and White"
                },
                new PhotoCategories
                {
                    CategoryName = "Boudoir"
                },
                new PhotoCategories
                {
                    CategoryName = "Celebrities"
                },
                new PhotoCategories
                {
                    CategoryName = "City & Architecture"
                },
                new PhotoCategories
                {
                    CategoryName = "Commercial"
                },
                new PhotoCategories
                {
                    CategoryName = "Concert"
                },
                new PhotoCategories
                {
                    CategoryName = "Family"
                },
                new PhotoCategories
                {
                    CategoryName = "Fashion"
                },
                new PhotoCategories
                {
                    CategoryName = "Film"
                },
                new PhotoCategories
                {
                    CategoryName = "Fine Art"
                },
                new PhotoCategories
                {
                    CategoryName = "Food"
                },
                new PhotoCategories
                {
                    CategoryName = "Journalism"
                },
                new PhotoCategories
                {
                    CategoryName = "Landscapes"
                },
                new PhotoCategories
                {
                    CategoryName = "Macro"
                },
                new PhotoCategories
                {
                    CategoryName = "Nature"
                },
                new PhotoCategories
                {
                    CategoryName = "Night"
                },
                new PhotoCategories
                {
                    CategoryName = "Nude"
                },
                new PhotoCategories
                {
                    CategoryName = "People"
                },
                new PhotoCategories
                {
                    CategoryName = "Performing Arts"
                },
                new PhotoCategories
                {
                    CategoryName = "Sport"
                },
                new PhotoCategories
                {
                    CategoryName = "Still Life"
                },
                new PhotoCategories
                {
                    CategoryName = "Street"
                },
                new PhotoCategories
                {
                    CategoryName = "Transportation"
                },
                new PhotoCategories
                {
                    CategoryName = "Travel"
                },
                new PhotoCategories
                {
                    CategoryName = "Underwater"
                },
                new PhotoCategories
                {
                    CategoryName = "Urban Exploration"
                },
                new PhotoCategories
                {
                    CategoryName = "Wedding"
                }
            };
        }
    }
}