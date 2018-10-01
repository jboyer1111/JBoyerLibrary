using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.DnDDiceRoller
{
    public class DnDRoller
    {

        #region Private Variables

        private string _rollText;

        #endregion

        public DnDRoller(string rollText)
        {
            _rollText = rollText;
        }

        public DnDRollResult Roll()
        {
            int total;
            if (Int32.TryParse(_rollText, out total))
            {
                return new DnDRollResult() { Total = total, DiceResult = _rollText };
            }


            return new DnDRollResult();
        }
    }
}
