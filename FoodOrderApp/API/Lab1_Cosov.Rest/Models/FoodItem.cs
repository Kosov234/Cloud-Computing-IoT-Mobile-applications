using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class FoodItem
    {
        [Key]
        public int ProductID { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Rating { get; set; }
        public string RatingDetail { get; set; }
        public string HomeSelected { get; set; }
        public double Price { get; set; }
        public int CategoryID { get; set; }
    }
}
