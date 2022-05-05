using System;
using System.Diagnostics.CodeAnalysis;

namespace JBoyer
{

    [ExcludeFromCodeCoverage]
    public class Team : IEquatable<Team>
    {

        public int Id { get; set; }

        public string? Name { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is not Team team)
            {
                return false;
            }

            return Equals(team);
        }

        public bool Equals(Team? other)
        {
            if (other == null || Id != other.Id)
            {
                return false;
            }

            return string.Equals(Name, other.Name);
        }

        public override int GetHashCode() => Id.GetHashCode() | (Name ?? "").GetHashCode();

    }

}
