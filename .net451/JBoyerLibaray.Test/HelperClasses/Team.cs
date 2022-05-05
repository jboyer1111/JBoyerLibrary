using System;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.HelperClasses
{

    [ExcludeFromCodeCoverage]
    public class Team : IEquatable<Team>
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Team)
            {
                return Equals((Team)obj);
            }
            
            return false;
        }

        public bool Equals(Team other)
        {
            if (Id != other.Id)
            {
                return false;
            }

            return String.Equals(Name, other.Name);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() | Name.GetHashCode();
        }

    }

}
