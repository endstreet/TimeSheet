using Microsoft.EntityFrameworkCore;
using razor.Components;
using Trm.MaLogger.Api.Services;
using Trm.MaLogger.App.Services.DataAccess;
using Trm.MaLogger.App.Services;
using Trm.MaLogger.MsData.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUserSession, UserSession>();
builder.Services.AddScoped<IAlertService, AlertService>();

//builder.Services.Configure<MaLoggerDbConfig>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddDbContextFactory<MaLoggerDbContext>(options => options.UseSqlServer("name=ConnectionStrings:Malogger"));
builder.Services.AddDbContext<MaLoggerDbContext>();//options => options.UseSqlServer("name=ConnectionStrings:Malogger"));
//builder.Services.AddSingleton<MaLoggerDbContext>();
builder.Services.AddScoped<UserService>();
builder.Services.AddSingleton<StaticDataService>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<TimeEntryService>();
builder.Services.AddScoped<ReportDataService>();

builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("EmailConfig"));
builder.Services.AddSingleton<EmailService>();

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
