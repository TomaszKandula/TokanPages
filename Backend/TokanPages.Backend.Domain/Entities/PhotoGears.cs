using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TokanPages.Backend.Core.Entities;

namespace TokanPages.Backend.Domain.Entities
{
    public class PhotoGears : Entity<Guid>
    {
        public PhotoGears()
        {
            Photos = new HashSet<Photos>();
        }

        [MaxLength(100)]
        public string BodyVendor { get; set; }

        [MaxLength(100)]
        public string BodyModel { get; set; }
        
        [MaxLength(100)]
        public string LensVendor { get; set; }
        
        [MaxLength(60)]
        public string LensName { get; set; }
        
        public int FocalLength { get; set; }
        
        [MaxLength(10)]
        public string ShutterSpeed { get; set; }

        public decimal Aperture { get; set; }
        
        public int FilmIso { get; set; }
        
        public ICollection<Photos> Photos { get; set; }
    }
}
