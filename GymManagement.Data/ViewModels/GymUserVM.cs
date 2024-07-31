using GymManagement.Data.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace GymManagement.Data.ViewModels
{
	public class GymUserVM
	{
		public GymUser? GymUser { get; set; }

		public string? Roles { get; set; }
		[ValidateNever]
		public IEnumerable<SelectListItem> GymUserList { get; set; }
	}
}
