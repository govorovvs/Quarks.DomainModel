# Quarks.DomainModel

Domain-Driven Design An approach to software development that suggests that (1) For most software projects, the primary focus should be on the domain and domain logic; and (2) Complex domain designs should be based on a model.

[![Version](https://img.shields.io/nuget/v/Quarks.DomainModel.svg)](https://www.nuget.org/packages/Quarks.DomainModel)

## Glossary

Excerpted from Domain-Driven Design Book by Eric Evans

### Aggregate

A cluster of associated objects that are treated as a unit for the purpose of data changes. External references are restricted to one member of the AGGREGATE, designated as the root. A set of consistency rules applies within the AGGREGATE’S boundaries.

<pre><code>
public class Account : IAggregate
{
}
</code></pre>
or
<pre><code>
[Aggregate]
public class Account
{
}
</code></pre>

### Bounded Context

The delimited applicability of a particular model. BOUNDING CONTEXTS gives team members a clear and shared understanding of what has to be consistent and what can develop independently.

<pre><code>
public class AccountManagement : IBoundedContext
{
    public string Name => "AccountManagement";
}
</pre></code>
or
<pre><code>
[BoundedContext("AccountManagement")]
public class AccountManagement
{
}
</pre></code>

### Entity

An object fundamentally defined not by its attributes, but by a thread of continuity and identity.

<pre><code>
public class Account : IEntity
{
}
</pre></code>
or
<pre><code>
[Entity]
public class Account
{
}
</pre></code>

### Factory

A mechanism for encapsulating complex creation logic and abstracting the type of a created object for the sake of a client.

<pre><code>
public class AccountFactory : IFactory
{
    public Account CreateAccount(string name) => new Account(name);
}
</pre></code>
or
<pre><code>
[Factory]
public class AccountFactory
{
    public Account CreateAccount(string name) => new Account(name);
}
</pre></code>

### Repository 

A mechanism for encapsulating storage, retrieval, and search behavior which emulates a collection of objects.

<pre><code>
public interface IAccountRepository : IRepository<Account>
{
    public Account FindById(int id);
}
</pre></code>
or
<pre><code>
[Repository(typeof(Account))]
public class IAccountRepository
{
    public Account FindById(int id);
}
</pre></code>

### Service

An operation offered as an interface that stands alone in the model, with no encapsulated state.

<pre><code>
public class AccountService : IService
{
    public void Transfer(Account source, Account dest, Money value)
    {
        source.Withdraw(value);
        dest.Enroll(value);
    }
}
</pre></code>
or
<pre><code>
[Service]
public class AccountService
{
    public void Transfer(Account source, Account dest, Money value)
    {
        source.Withdraw(value);
        dest.Enroll(value);
    }
}
</pre></code>

###Value object

An object that describes some characteristic or attribute but carries no concept of identity.

<pre><code>
public class Money : IValueObject
{
    public decimal Value { get; }
    public Currency Currency { get; }
}
</pre></code>
or
<pre><code>
[ValueObject]
public class Money : IValueObject
{
    public decimal Value { get; }
    public Currency Currency { get; }
}
</pre></code>