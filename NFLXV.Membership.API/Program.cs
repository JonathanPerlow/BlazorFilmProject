

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NFLXV.Common.DTOs;
using NFLXV.Membership.Database.Entities;
using NFLXV.Membership.Database.Services;
using static System.Collections.Specialized.BitVector32;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureServices();
ConfigureAutoMapper();

// Configure the HTTP request pipeline.
ConfigureMiddleware();

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("CorsAllAccessPolicy", opt =>
    opt.AllowAnyOrigin().AllowAnyHeader()
     .AllowAnyMethod());
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NFLXVContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("NFLXVConnection"))
    );

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsAllAccessPolicy");

app.UseAuthorization();

app.MapControllers();
app.Run();

void ConfigureAutoMapper()
{
    var config = new AutoMapper.MapperConfiguration(cfg =>
    {
        cfg.CreateMap<Director, DirectorGetDTO>().ReverseMap();
        cfg.CreateMap<Director, DirectorCreateDTO>().ReverseMap();
        cfg.CreateMap<Director, DirectorEditDTO>().ReverseMap();
        cfg.CreateMap<Director, DirectorDeleteDTO>().ReverseMap();

        cfg.CreateMap<Film, FilmGetDTO>().ReverseMap();
        cfg.CreateMap<Film, FilmCreateDTO>().ReverseMap();
        cfg.CreateMap<Film, FilmEditDTO>().ReverseMap();
        cfg.CreateMap<Film, FilmDeleteDTO>().ReverseMap();

        cfg.CreateMap<Genre, GenreGetDTO>().ReverseMap();
        cfg.CreateMap<Genre, GenreCreateDTO>().ReverseMap();
        cfg.CreateMap<Genre, GenreEditDTO>().ReverseMap();
        cfg.CreateMap<Genre, GenreDeleteDTO>().ReverseMap();

        cfg.CreateMap<FilmGenres, FilmGenreDTO>().ReverseMap();
        cfg.CreateMap<SimilarFilms, SimilarFilmsDTO>().ReverseMap();
       


    });
    var mapper = config.CreateMapper();
    builder.Services.AddSingleton(mapper);
}

void ConfigureMiddleware()
{
    

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseCors("CorsAllAccessPolicy");

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}


void ConfigureServices()
{
    builder.Services.AddCors(policy =>
    {
        policy.AddPolicy("CorsAllAccessPolicy", opt =>
        opt.AllowAnyOrigin().AllowAnyHeader()
         .AllowAnyMethod());
    });
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDbContext<NFLXVContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("NFLXVConnection"))
        );
    builder.Services.AddScoped<IDbService, DbService>();
}

