using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.DnDDiceRoller
{
    internal class DnDFormulaCalc
    {
        private Random _rand;
        private int _numberOfDice;
        private int _numberOfSides;
        private int _topBottomNumber;
        private TopBottom _topBottom;
        private int _minNumber;
        private int _maxNumber;
        private int _statsCalcedValue;
        private int _perRollAddSub;
        private List<int> _rolls;

        public DnDFormulaCalc(Random rand, int numberOfDice, int numberOfSides, int perRollAddSub, int topBottomNumber, TopBottom topBottom, int minNumber, int maxNumber)
        {
            _rand = rand;
            _rolls = new List<int>(numberOfDice);
            _numberOfDice = numberOfDice;
            _numberOfSides = numberOfSides;
            _topBottomNumber = topBottomNumber;
            _topBottom = topBottom;
            _minNumber = minNumber;
            _maxNumber = maxNumber;
            _perRollAddSub = perRollAddSub;

            if (minNumber > numberOfSides)
            {
                throw new ArgumentException("Min number is greater than number of sides");
            }
            else if (maxNumber > numberOfSides)
            {
                throw new ArgumentException("Max number is greater than number of sides");
            }
            else if (minNumber > maxNumber)
            {
                throw new ArgumentException("Min number is greater than max numbe");
            }
            else if (topBottomNumber > 0 && _topBottom == TopBottom.Not)
            {
                throw new ArgumentException(String.Format("Have a top bottom number but does not specify if is the top {0} rolls or bottom {0} rolls", topBottomNumber));
            }
            else if (topBottomNumber > numberOfDice)
            {
                throw new ArgumentException(String.Format("Cannot take {0} {1} rolls only have {2} dice", topBottom == TopBottom.Top ? "Top" : "Bottom", topBottomNumber, numberOfDice));
            }
        }

        public int Calc()
        {
            _rolls.Clear();
            int currentMinNumber = 1;
            int currentMaxNumber = _numberOfSides;
            if (_minNumber > currentMinNumber)
                currentMinNumber = _minNumber;
            if (_maxNumber > 0 && _maxNumber < currentMaxNumber)
                currentMaxNumber = _maxNumber;

            for (int i = 0; i < _numberOfDice; i++)
            {
                _rolls.Add(_rand.Next(currentMinNumber, currentMaxNumber + 1) + _perRollAddSub);
            }

            int result;
            if (_topBottom != TopBottom.Not)
            {
                _rolls = _topBottom == TopBottom.Top ? _rolls.OrderByDescending(i => i).ToList() : _rolls.OrderBy(i => i).ToList();
                result = _rolls.Take(_topBottomNumber).Sum();
            }
            else
            {
                result = _rolls.Sum();
            }

            _statsCalcedValue = result;

            return result;
        }


        public string Stats(bool partOfList)
        {
            if (_rolls.Count < 1)
            {
                return null;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}:{1}", _statsCalcedValue, partOfList ? "(" : " ");

            int top = _topBottom != TopBottom.Not ? _topBottomNumber : _rolls.Count;

            for (int i = 0; i < top; i++)
            {
                if (i > 0)
                {
                    sb.Append(", ");
                }
                sb.Append(_rolls[i]);
            }

            if (top < _rolls.Count)
            {
                sb.Append(", [");
                while (top < _rolls.Count)
                {
                    sb.Append(_rolls[top++]);
                    if (top < _rolls.Count)
                    {
                        sb.Append(", ");
                    }
                }
                sb.Append("]");
            }
            if (partOfList)
            {
                sb.Append(")");
            }

            return sb.ToString();
        }
    }
}
