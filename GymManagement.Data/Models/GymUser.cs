﻿using Microsoft.AspNetCore.Identity;


namespace GymManagement.Data.Models
{
    public class GymUser : IdentityUser 
    {
        public string Full_Name { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Gender { get; set; }
        public string Type { get; set; }
        public int Age  { get; set; }
        public string City { get; set; }
        public bool Membership { get; set; }
        public string Role { get; set; }
    }   
}
