using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession(op =>
{
    op.Cookie.Name = "IsLoggedIn";
    op.IdleTimeout = TimeSpan.FromMinutes(30);
    op.Cookie.IsEssential = true;

});
// Add services to the container.
builder.Services.AddRazorPages();
/*builder.Services.AddAuthentication("MyAuthScheme")
    .AddCookie("MyAuthScheme", options =>
    {
        options.LoginPath = "/Login"; // Set the login page path
    });
*/
/*builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});*/
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
/*app.MapFallbackToPage("/Login");*/
app.Run();
