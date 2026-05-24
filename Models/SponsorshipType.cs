using System.ComponentModel.DataAnnotations;

namespace Sponsorship.Models;

public class SponsorshipTypeRead
{

    public string TypeCode { get; set; } = null!;

    public string TypeName { get; set; } = null!;

    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
}

public class SponsorshipTypeCreate
{

    [Required]
    [MaxLength(3)]
    public string TypeCode { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string TypeName { get; set; } = null!;

    [MaxLength(250)]
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
}

public class SponsorshipTypeUpdate
{
    [Required]
    [MaxLength(3)]
    public string TypeCode { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string TypeName { get; set; } = null!;

    [MaxLength(250)]
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
}
