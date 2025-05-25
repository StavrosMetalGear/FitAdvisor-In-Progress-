using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FitAdvisor.Data;
using System.ComponentModel.DataAnnotations;

namespace FitAdvisor.Pages
{
    public class TrackProgressModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public TrackProgressModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Current Weight (kg)")]
            public double Weight { get; set; }
        }

        public List<WeightEntry> History { get; set; } = new();
        public double? StartingWeight { get; set; }
        public double? TargetWeight { get; set; }
        public double? LatestWeight { get; set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return;

            TargetWeight = user.TargetWeight;

            History = await _context.WeightEntries
                .Where(w => w.UserId == user.Id)
                .OrderBy(w => w.Date)
                .ToListAsync();

            StartingWeight = user.Weight;
            LatestWeight = History.LastOrDefault()?.Weight;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (!ModelState.IsValid || user == null)
                return Page();

            var entry = new WeightEntry
            {
                UserId = user.Id,
                Weight = Input.Weight,
                Date = DateTime.Today
            };

            _context.WeightEntries.Add(entry);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}

