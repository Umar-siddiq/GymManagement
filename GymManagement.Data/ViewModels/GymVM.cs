using System;
using GymManagement.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagement.Data.ViewModels
{
    public class GymVM
    {
        public Gym? Gym {  get; set; }
        public IEnumerable<SelectListItem>? GymList {  get; set; } 
    }
}
