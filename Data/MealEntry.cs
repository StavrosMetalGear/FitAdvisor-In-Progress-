using System;
using System.ComponentModel.DataAnnotations;

namespace FitAdvisor.Data
{
    public class MealEntry
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public string MealName { get; set; }

        [Required]
        [Range(0, 5000)]
        public int Calories { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}

