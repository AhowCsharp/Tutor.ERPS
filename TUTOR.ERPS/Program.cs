using TUTOR.ERPS.API.Infa.Swagger;
using Microsoft.OpenApi.Models;
using NLog.Web;
using TUTOR.Repository.EF;
using Microsoft.EntityFrameworkCore;
using TUTOR.Repository;
using TUTOR.Biz.Domain.API;
using TUTOR.Biz.Repository_Interfaces;
using TUTOR.Biz.Extensions;
using TUTOR.Biz.Models;
using System.Data;
using System.Data.SqlClient;
using TUTOR.Biz.Services;
using Microsoft.AspNetCore.Hosting;
using TUTOR.Repository.Mapper;
using TUTOR.Repository.Repositories;
using AutoMapper;
using TUTOR.Biz.SeedWork;
using TUTOR.Biz.Domain.API.Interface;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();

// DB
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TUTORERPSContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IDbConnection, SqlConnection>(serviceProvider =>
{
    SqlConnection conn = new SqlConnection();
    conn.ConnectionString = connectionString;
    return conn;
});
// Config
builder.Services.ConfigureConfig<TUTORConfig>(builder.Configuration.GetSection("TUTORConfig"));

#region Service

builder.Services.AddScoped<UserService>();

#endregion Service

#region Repository

builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<ISentenceManageRepository, SentenceManageRepository>();
builder.Services.AddScoped<IStudentAnswerLogRepository, StudentAnswerLogRepository>();
builder.Services.AddScoped<IAdminManageRepository, AdminManageRepository>();

#endregion Repository

#region Domain

builder.Services.AddScoped<AttributeDomain>();
builder.Services.AddScoped<MemberDomain>();
builder.Services.AddScoped<SentenceManageDomain>();
builder.Services.AddScoped<AdminManageDomain>();
builder.Services.AddScoped<StudentAnswerLogDomain>();

#endregion Domain

// 保持屬性名稱不變
builder.Services.AddMvc().AddJsonOptions(option => option.JsonSerializerOptions.PropertyNamingPolicy = null);
// Swagger
var environmentName = builder.Configuration.GetSection("TUTORConfig")["EnvironmentName"];
// Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo { Title = $"{environmentName} TUTOR API", Version = "v1" });
    swagger.OperationFilter<SwaggerFileOperationFilter>();
    swagger.OperationFilter<AddRequireHeaderParameter>();
    swagger.EnableAnnotations();
});

builder.Services.AddAutoMapper(typeof(AutoMapperTool));

builder.Services.AddScoped<IMapper, Mapper>();

builder.Host.UseNLog();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder
            .WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});
var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(option =>
{
    option.SwaggerEndpoint($"{builder.Configuration["PATHBASE"] ?? string.Empty}/swagger/v1/swagger.json", "IALW API v1");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1"));
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();
app.UseStaticFiles(); 

// 需要加上這段使用 swagger 時才不會404
app.MapControllers();

app.Run();