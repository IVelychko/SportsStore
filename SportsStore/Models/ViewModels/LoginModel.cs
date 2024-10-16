using System.ComponentModel.DataAnnotations;

namespace SportsStore.Models.ViewModels;

public class LoginModel
{
    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Password { get; set; }

#pragma warning disable CA1056 // URI-like properties should not be strings
    public string ReturnUrl { get; set; } = "/";
#pragma warning restore CA1056 // URI-like properties should not be strings
}
