

namespace NFLXV.Membership.Database.Contexts;

public class NFLXVContext : DbContext
{
    public DbSet<Film> Film => Set<Film>();
    public DbSet<Director> Director => Set<Director>();
    public DbSet<SimilarFilms> SimilarFilms => Set<SimilarFilms>();
    public DbSet<Genre> Genre => Set<Genre>();
    public DbSet<FilmGenres> FilmGenres => Set<FilmGenres>();

    public NFLXVContext(DbContextOptions<NFLXVContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<FilmGenres>().HasKey(f => new { f.FilmId, f.GenreId });
        builder.Entity<SimilarFilms>().HasKey(f => new { f.ParentFilmId, f.SimilarFilmId });

        builder.Entity<Film>(entity =>
        {
            entity
                .HasMany(m => m.SimilarFilms)
                .WithOne(f => f.ParentFilm)
                .HasForeignKey(e => e.ParentFilmId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity
                .HasMany(c => c.Genres)
                .WithMany(d => d.Films)
                .UsingEntity<FilmGenres>()
                .ToTable("FilmGenres");
        });
        
        

        foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            // Ta bort cascading delete. Ändrar Entity framework konfiguration
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}
