using System.ComponentModel.DataAnnotations;

namespace EasyAPI.Models.DTOs;

public class UserDto
{
    [Required]
    public string Username { get; set; } = string.Empty;
        
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
        
    [Required]
    public string Password { get; set; } = string.Empty;
        
    [Required]
    public string FullName { get; set; } = string.Empty;
}