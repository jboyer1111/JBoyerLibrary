using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.DeckOfCards
{
    [ExcludeFromCodeCoverage]
    public class TestCard : Card
    {
        public TestCard(string suit, string rank) : base(suit, rank)
        {

        }
    }
}
