using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
namespace FitAdvisor.Pages
{



    public class CaloriePlanModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CaloriePlanModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public double Weight { get; set; }
        public double Height { get; set; }
        public double TargetWeight { get; set; }

        public int EasyCalories { get; set; }
        public int MediumCalories { get; set; }
        public int HardCalories { get; set; }
        public double CurrentBodyFat { get; set; }
        public double TargetBodyFat { get; set; }


        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                Weight = user.Weight;
                Height = user.Height;
                TargetWeight = user.TargetWeight;

                // Recalculate all three from BMR:
                double bmr = 10 * Weight + 6.25 * Height - 5 * 30 + (user.Gender == "Male" ? 5 : -161);
                int maintenance = (int)(bmr * 1.5*0.8);

                // Apply 20% reduction for fitness goal
                int adjusted = (int)(maintenance * 0.8);

                // Now base all three off that
                EasyCalories = (int)(adjusted * 1.1);   // Easiest (more calories)
                MediumCalories = adjusted;             // Base level
                HardCalories = (int)(adjusted * 0.9);  // Toughest
                int age = 30; // hardcoded for now

                double heightM = Height / 100.0;
                double bmi = Weight / (heightM * heightM);
                double targetBmi = TargetWeight / (heightM * heightM);
                int genderFlag = user.Gender == "Male" ? 1 : 0;

                CurrentBodyFat = Math.Round((1.2 * bmi) + (0.23 * age) - (10.8 * genderFlag) - 5.4, 1);
                TargetBodyFat = Math.Round((1.2 * targetBmi) + (0.23 * age) - (10.8 * genderFlag) - 5.4, 1);


            }
        }
    }

}