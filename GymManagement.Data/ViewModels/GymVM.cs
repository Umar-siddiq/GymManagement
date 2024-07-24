using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
