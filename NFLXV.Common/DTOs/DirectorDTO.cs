using System.ComponentModel.DataAnnotations;

namespace NFLXV.Common.DTOs;

public class DirectorGetDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<FilmGetDTO> Films { get;set; } = new();
}

public class DirectorCreateDTO
{
    public string Name { get; set; } = null!;
}

public class DirectorEditDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}


public class DirectorDeleteDTO
{
    public int Id { get; set; }
}