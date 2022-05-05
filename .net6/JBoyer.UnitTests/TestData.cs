using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JBoyer
{

    [ExcludeFromCodeCoverage]
    public static class TestData
    {

        public static IEnumerable<Team> GetTeams()
        {
            return new Team[]
            {
                new Team { Id = 1, Name = "Billy" },
                new Team { Id = 2, Name = "Bob" }
            };
        }

        public static IEnumerable<TableRow> GetTable()
        {
            return new TableRow[]
            {

            };
        }

    }

}
