﻿using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
    public string Gender { get; set; }
    public double Height { get; set; } // in cm or meters

    public double Weight { get; set; }
    public double TargetWeight { get; set; }
}
