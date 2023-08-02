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
using AutoMapper;

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
builder.Services.AddScoped<ILogSystemRepository, LogSystemRepository>();

#endregion Repository

#region Domain

builder.Services.AddScoped<AttributeDomain>();
builder.Services.AddScoped<MemberDomain>();

#endregion Domain

// �O���ݩʦW�٤���
builder.Services.AddMvc().AddJsonOptions(option => option.JsonSerializerOptions.PropertyNamingPolicy = null);
// Swagger
var environmentName = builder.Configuration.GetSection("TUTORConfig")["EnvironmentName"];
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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

// �ݭn�[�W�o�q�ϥ� swagger �ɤ~���|404
app.MapControllers();

app.Run();