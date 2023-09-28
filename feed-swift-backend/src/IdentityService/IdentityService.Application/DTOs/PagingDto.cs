using System.ComponentModel;

namespace IdentityService.Application.DTOs;

public sealed class PagingDto
{
    [DefaultValue(1)]
    public int Page { get; set; } = 1;
    [DefaultValue(20)]
    public int Limit { get; set; } = 20;
    public string? SearchText { get; set; }
}