using System;
using System.ComponentModel.DataAnnotations;

namespace FitAdvisor.Data
{
    public class WeightEntry
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public double Weight { get; set; }

        public DateTime Date { get; set; }
    }
}

