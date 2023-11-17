using Domain.Errors;
using Domain.Primitives;
using Domain.Shared;
using Domain.ValueObjects;

namespace Domain.Entities;

public class User : AggregateRoot, IAuditableEntity, ISoftDeletableEntity
{
    private string _passwordHash;

    private User(FirstName firstName, LastName lastName, Email email, string passwordHash)
        : base(Guid.NewGuid())
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        _passwordHash = passwordHash;
    }

    private User()
    {
    }
    
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public string FullName => $"{FirstName} {LastName}";
    public Email Email { get; private set; }
    
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public DateTime? DeletedOnUtc { get; set; }
    public bool Deleted { get; set; }
    
    public static User Create(FirstName firstName, LastName lastName, Email email, string passwordHash)
    {
        var user = new User(firstName, lastName, email, passwordHash);
        
        return user;
    }
    
    public Result ChangePassword(string passwordHash)
    {
        if (passwordHash == _passwordHash)
            return Result.Failure(DomainErrors.User.CannotChangePassword);

        _passwordHash = passwordHash;
        
        return Result.Success();
    }
}