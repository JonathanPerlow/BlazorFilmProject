namespace NFLXV.Common.DTOs;

public class DirectorGetDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
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