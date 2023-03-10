using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using razor.Components;
using Trm.MaLogger.App.Services;
using Trm.MaLogger.App.Services.DataAccess;
using Trm.MaLogger.MsData.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUserSession, UserSession>();
builder.Services.AddScoped<IAlertService, AlertService>();
builder.Services.AddDbContextFactory<MaLoggerDbContext>(options => options.UseSqlServer("name=ConnectionStrings:MaloggerX"));
builder.Services.AddDbContext<MaLoggerDbContext>();
builder.Services.AddScoped<UserService>();
builder.Services.AddSingleton<StaticDataService>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<TimeEntryService>();
builder.Services.AddScoped<ReportDataService>();

builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("EmailConfig"));
builder.Services.AddSingleton<EmailService>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor().AddCircuitOptions(e =>
{
    e.DetailedErrors = true;
});
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddRouting(ctx => ctx.LowercaseUrls = false);

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

app.UseRouting();
app.MapControllers();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

//using var scope = app.Services.CreateScope();
//await using var dbContext = scope.ServiceProvider.GetRequiredService<MaLoggerDbContext>();
//await dbContext.Database.MigrateAsync();

app.Run();
