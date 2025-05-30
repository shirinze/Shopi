using Shopi.DomainModel.BaseModels;

namespace Shopi.DomainModel.Models;

public class UserEntity:TrackableEntity
{
    

    public string Name { get;private set; } = string.Empty;
    public string LastName { get;private set; } = string.Empty;
    public string Phone { get;private set; } = string.Empty;
    public bool IsActive { get; set; }

    public static UserEntity CreateUser(string name,string lastName,string phone)
    {
        return new UserEntity(name, lastName, phone);
    }
    public UserEntity(string name, string lastName, string phone)
    {
        SetName(name);
        SetLastName(lastName);
        SetPhone(phone);
        
    }

    public void Update(string name,string lastName,string phone)
    {
        SetName(name);
        SetLastName(lastName);
        SetPhone(phone);
    }
    public void ToggleActivation()
    {
        IsActive = !IsActive;
    }

    private void SetPhone(string phone)
    {
        Phone = phone;
    }

    private void SetLastName(string lastName)
    {
        LastName = lastName;
    }

    private void SetName(string name)
    {
        Name = name;
    }

}
