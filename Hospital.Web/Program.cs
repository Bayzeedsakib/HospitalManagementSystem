using Microsoft.EntityFrameworkCore;
using DAL.EF;
using DAL.Repositories;
using BLL.Services;

var builder = WebApplication.CreateBuilder(args);

//Add Services to the container
builder.Services.AddScoped<PatientRepo>();
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<DepartmentRepo>();
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<DoctorRepo>();
builder.Services.AddScoped<DoctorService>();
builder.Services.AddScoped<UserRepo>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AppointmentRepo>();
builder.Services.AddScoped<AppointmentService>();




// Add MVC
builder.Services.AddControllersWithViews();


// Database Connection
builder.Services.AddDbContext<HospitalDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("dbconn")
    )
);


// Session Support
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();


// Error Handling
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();


// Session Middleware
app.UseSession();

app.UseAuthorization();


// Default Route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();