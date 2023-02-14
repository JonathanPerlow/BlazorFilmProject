namespace NFLXV.Common.DTOs; 
public class GenreGetDTO 
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<FilmGetDTO> Genres { get; set; } = new();
}

public class GenreCreateDTO
{
    public string Name { get; set; } = null!;
}

public class GenreEditDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}

public class GenreDeleteDTO
{
    public int Id { get; set; }
}

