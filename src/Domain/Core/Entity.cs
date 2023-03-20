namespace Domain.Core;

public abstract class Entity
{
    public virtual long Id { get; protected set; }

    public override bool Equals(object? obj)
    {
        if (!(obj is Entity other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (Id == 0 || other.Id == 0)
        {
            return false;
        }

        return Id == other.Id;
    }

    public static bool operator ==(Entity? left, Entity? right) 
    {
        if (left is null && right is null)
        {
            return true;
        }

        if (left is null || right is null)
        {
            return false;
        }

        return left.Equals(right);
    }

    public static bool operator !=(Entity? left, Entity? right) => !(left == right);

    public override int GetHashCode() => (GetType().ToString() + Id).GetHashCode();
}
