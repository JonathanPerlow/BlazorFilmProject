var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureServices();
ConfigureAutoMapper();

// Configure the HTTP request pipeline.
ConfigureMiddleware();


void ConfigureAutoMapper()
{
    var config = new AutoMapper.MapperConfiguration(cfg =>
    {
        cfg.CreateMap<Director, DirectorGetDTO>();

        cfg.CreateMap<DirectorCreateDTO, Director>()
        .ForMember(dest => dest.Films, src => src.Ignore());

        cfg.CreateMap<DirectorEditDTO, Director>()
        .ForMember(dest => dest.Films, src => src.Ignore());

        cfg.CreateMap<DirectorDeleteDTO, Director>()
        .ForMember(dest => dest.Films, src => src.Ignore());

        cfg.CreateMap<Film, FilmGetDTO>();


        cfg.CreateMap<FilmCreateDTO, Film>();
        cfg.CreateMap<FilmEditDTO, Film>();

        cfg.CreateMap<FilmDeleteDTO, Film>();
        cfg.CreateMap<Film, FilmInfoDTO>().ReverseMap();


        cfg.CreateMap<Genre, GenreGetDTO>();
        cfg.CreateMap<GenreCreateDTO, Genre>()
            .ForMember(dest => dest.Films, src => src.Ignore());
        cfg.CreateMap<GenreEditDTO, Genre>()
            .ForMember(dest => dest.Films, src => src.Ignore());
        cfg.CreateMap<GenreDeleteDTO, Genre>()
            .ForMember(dest => dest.Films, src => src.Ignore());



        cfg.CreateMap<FilmGenreDTO, FilmGenres>();
        cfg.CreateMap<SimilarFilmsDTO, SimilarFilm>().ReverseMap();
       


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
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDbContext<NFLXVContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("NFLXVConnection"))
        );
    builder.Services.AddScoped<IDbService, DbService>();
}

