namespace Shopi.WebApi.DTOs.UserDtos;

public class CreateUserDto
{
    public required string Name { get; set; } 
    public required string LastName { get; set; }
    public required string Phone { get; set; }
}
