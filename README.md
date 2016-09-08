# Quarks.DomainModel

Domain-Driven Design is an approach to software development that suggests that (1) For most software projects, the primary focus should be on the domain and domain logic; and (2) Complex domain designs should be based on a model.

[![Version](https://img.shields.io/nuget/v/Quarks.DomainModel.svg)](https://www.nuget.org/packages/Quarks.DomainModel)

## Glossary

Excerpted from Domain-Driven Design Book by Eric Evans

### Aggregate

A cluster of associated objects that are treated as a unit for the purpose of data changes. External references are restricted to one member of the AGGREGATE, designated as the root. A set of consistency rules applies within the AGGREGATE’S boundaries.

```csharp
public class Account : IAggregate
{
}
```
or
```csharp
[Aggregate]
public class Account
{
}
```

### Bounded Context

The delimited applicability of a particular model. BOUNDING CONTEXTS gives team members a clear and shared understanding of what has to be consistent and what can develop independently.

```csharp
public class AccountManagement : IBoundedContext
{
    public string Name => "AccountManagement";
}
```
or
```csharp
[BoundedContext("AccountManagement")]
public class AccountManagement
{
}
```

### Entity

An object fundamentally defined not by its attributes, but by a thread of continuity and identity.

```csharp
public class Account : IEntity
{
}
```
or
```csharp
[Entity]
public class Account
{
}
```

### Factory

A mechanism for encapsulating complex creation logic and abstracting the type of a created object for the sake of a client.

```csharp
public class AccountFactory : IFactory
{
    public Account CreateAccount(string name) => new Account(name);
}
```
or
```csharp
[Factory]
public class AccountFactory
{
    public Account CreateAccount(string name) => new Account(name);
}
```

### Repository 

A mechanism for encapsulating storage, retrieval, and search behavior which emulates a collection of objects.

```csharp
public interface IAccountRepository : IRepository<Account>
{
    public Account FindById(int id);
}
```
or
```csharp
[Repository(typeof(Account))]
public class IAccountRepository
{
    public Account FindById(int id);
}
```

### Service

An operation offered as an interface that stands alone in the model, with no encapsulated state.

```csharp
public class AccountService : IService
{
    public void Transfer(Account source, Account dest, Money value)
    {
        source.Withdraw(value);
        dest.Enroll(value);
    }
}
```
or
```csharp
[Service]
public class AccountService
{
    public void Transfer(Account source, Account dest, Money value)
    {
        source.Withdraw(value);
        dest.Enroll(value);
    }
}
```

###Value object

An object that describes some characteristic or attribute but carries no concept of identity.

```csharp
public class Money : IValueObject
{
    public decimal Value { get; }
    public Currency Currency { get; }
}
```
or
```csharp
[ValueObject]
public class Money : IValueObject
{
    public decimal Value { get; }
    public Currency Currency { get; }
}
```

## Domain events

Domain Events work in exactly the same way that an event based architecture works in other contexts.

You will typically create a new event such as *UserWasRegistered*. This will be a class that holds the required details of the event that just took place, in this case an instance a *User* object.
```csharp
public class UserWasRegistered : IDomainEvent
{
	public UserWasRegistered(User user) 
	{
		User = user;
	}

	public User User { get; }
}

public class UserManager : IService
{
	private readonly IUserRepository _userRepository;
	private readonly IUserFactory _userFactory;

	public async Task RegisterUserAsync(string name, CancellationToken cancellationToken)
	{
		User user _userFactory.CreateUser(name);
		await _userRepository.AddAsync(user, cancellationToken);

		UserWasRegistered event = new UserWasRegistered(user);
		await DomainEvents.RiseAsync(event, cancellationToken);
	}
}
```

Next you will write handler to handle the event. For example, you might have a handler called *SendNewUserWelcomeEmail*. This would be a class that accepts the UserWasRegistered event and uses the *User* object to send the email.
```csharp
public class SendNewUserWelcomeEmail : IDomainEventHandler<UserWasRegistered>
{
	private IMailService _mailService;

	public async Task HandleAsync(UserWasRegistered event, CancellationToken cancellationToken)
	{
		MailMessage message = CreateMail(event.User);
		await _mailService.SendMessageAsync(member, cancellationToken);
	}
}
```
The *SendNewUserWelcomeEmail* is responsible for having the ability to send the email and so the process for registering a new user is completely decoupled from the process of sending the email.

You can also register multiple listeners for events so you can very easily add or remove actions that should be fired whenever an event takes place.
```csharp
public void ConfigureDomainEvents
{
	var handlers = new IDomainEventHandler[]
	{
		new SendNewUserWelcomeEmail(),
		new CreateUserAccount()
	}

	DomainEvents.Dispatcher = new DomainEventsDispatcher(handlers);
}
```