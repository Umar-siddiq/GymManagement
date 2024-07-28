using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using GymManagement.DataAccess;
using GymManagement.Utility.Services;
using GymManagement.Data.IRepository;
using GymManagement.DataAccess.Repository;
using Microsoft.AspNetCore.Identity.UI.Services;
using GymManagement.Utility;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<GymApiService>();

builder.Services.AddHttpClient<GymUserApiService>();

builder.Services.AddDbContext<GymDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<GymDbContext>().AddDefaultTokenProviders();

builder.Services.AddRazorPages();

builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LoginPath = $"/Identity/Account/Logout";
    options.LoginPath = $"/Identity/Account/AccessDenied";
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();


using (var scope = app.Services.CreateScope())
{
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
	var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

	// Ensure roles exist
	string[] roleNames = { Roles.Role_Admin, Roles.Role_Trainer, Roles.Role_Customer, Roles.Role_Member, Roles.Role_Company, Roles.Role_Employee };
	foreach (var roleName in roleNames)
	{
		if (!await roleManager.RoleExistsAsync(roleName))
		{
			await roleManager.CreateAsync(new IdentityRole(roleName));
		}
	}

	// Seed admin user
	string adminEmail = "admin@gmail.com";
	string adminPassword = "Test99_";

	if (await userManager.FindByEmailAsync(adminEmail) == null)
	{
		var adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail };
		var result = await userManager.CreateAsync(adminUser, adminPassword);
		if (result.Succeeded)
		{
			await userManager.AddToRoleAsync(adminUser, Roles.Role_Admin);
		}
	}
}


app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}"
    
    );

app.MapControllerRoute(
	name: "admin",
	pattern: "{area=Admin}/{controller=Gym}/{action=Index}/{id?}");

app.Run();
