using System.Collections.Generic;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Initializer.Seeders
{
    public static class PhotoCategoriesSeeder
    {
        public static IEnumerable<PhotoCategories> SeedPhotoCategories()
        {
            return new List<PhotoCategories>
            {
                new ()
                {
                    CategoryName = "Uncategorised"
                },
                new ()
                {
                    CategoryName = "Abstract"
                },
                new ()
                {
                    CategoryName = "Aerial"
                },
                new ()
                {
                    CategoryName = "Animals"
                },
                new ()
                {
                    CategoryName = "Black and White"
                },
                new ()
                {
                    CategoryName = "Boudoir"
                },
                new ()
                {
                    CategoryName = "Celebrities"
                },
                new ()
                {
                    CategoryName = "City & Architecture"
                },
                new ()
                {
                    CategoryName = "Commercial"
                },
                new ()
                {
                    CategoryName = "Concert"
                },
                new ()
                {
                    CategoryName = "Family"
                },
                new ()
                {
                    CategoryName = "Fashion"
                },
                new ()
                {
                    CategoryName = "Film"
                },
                new ()
                {
                    CategoryName = "Fine Art"
                },
                new ()
                {
                    CategoryName = "Food"
                },
                new ()
                {
                    CategoryName = "Journalism"
                },
                new ()
                {
                    CategoryName = "Landscapes"
                },
                new ()
                {
                    CategoryName = "Macro"
                },
                new ()
                {
                    CategoryName = "Nature"
                },
                new ()
                {
                    CategoryName = "Night"
                },
                new ()
                {
                    CategoryName = "Nude"
                },
                new ()
                {
                    CategoryName = "People"
                },
                new ()
                {
                    CategoryName = "Performing Arts"
                },
                new ()
                {
                    CategoryName = "Sport"
                },
                new ()
                {
                    CategoryName = "Still Life"
                },
                new ()
                {
                    CategoryName = "Street"
                },
                new ()
                {
                    CategoryName = "Transportation"
                },
                new ()
                {
                    CategoryName = "Travel"
                },
                new ()
                {
                    CategoryName = "Underwater"
                },
                new ()
                {
                    CategoryName = "Urban Exploration"
                },
                new ()
                {
                    CategoryName = "Wedding"
                }
            };
        }
    }
}