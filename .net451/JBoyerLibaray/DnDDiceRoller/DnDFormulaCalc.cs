using JBoyerLibaray.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace JBoyerLibaray.DnDDiceRoller
{

    public class DnDFormulaCalc
    {

        #region Private Variables

        private Random _rand;
        private int _numberOfDice;
        private int _numberOfSides;
        
        private TopBottom _topBottom;
        private int? _topBottomNumber;
        private int? _minNumber;
        private int? _maxNumber;

        private int _statsCalcedValue;
        private int _rollModifier;
        private List<int> _rolls;

        #endregion

        #region Public Properties

        [ExcludeFromCodeCoverage]
        public int NumberOfDice => _numberOfDice;

        [ExcludeFromCodeCoverage]
        public int NumberOfSides => _numberOfSides;

        [ExcludeFromCodeCoverage]
        public int RollModifier => _rollModifier;

        [ExcludeFromCodeCoverage]
        public int? MinNumber => _minNumber;

        [ExcludeFromCodeCoverage]
        public int? MaxNumber => _maxNumber;

        [ExcludeFromCodeCoverage]
        public string SubSet => _topBottom != TopBottom.None ? $"{_topBottom} {_topBottomNumber.Value}" : null;
        
        #endregion

        #region Constructor

        internal DnDFormulaCalc(Random rand, int numberOfDice, int numberOfSides, int perRollAddSub, TopBottom topBottom, int? topBottomNumber,  int? minNumber, int? maxNumber)
        {
            // "Null" Checks
            if (rand == null)
            {
                throw ExceptionHelper.CreateArgumentNullException(() => rand);
            }

            if (numberOfDice < 1)
            {
                throw ExceptionHelper.CreateArgumentInvalidException(() => numberOfDice, "You need at least one die!", numberOfDice);
            }

            if (numberOfSides < 2)
            {
                throw ExceptionHelper.CreateArgumentInvalidException(() => numberOfSides, "A die needs at least two sides.", numberOfSides);
            }

            // Valid Enum Check
            if (!Enum.IsDefined(typeof(TopBottom), topBottom))
            {
                throw ExceptionHelper.CreateArgumentInvalidException(() => topBottom, "An invalid Enum value has been passed.", topBottom);
            }

            // Top Bottom Number validation
            if (topBottom != TopBottom.None)
            {
                if (!topBottomNumber.HasValue)
                {
                    throw ExceptionHelper.CreateArgumentNullException(() => topBottomNumber, "Top-Bottom number cannot be null when you specify to take the top or bottom amount of numbers.");
                }

                if (topBottomNumber.Value > numberOfDice)
                {
                    throw ExceptionHelper.CreateArgumentInvalidException(
                        () => topBottomNumber,
                        String.Format("Cannot take {0} {1} rolls only have {2} dice.", topBottom == TopBottom.Top ? "Top" : "Bottom", topBottomNumber, numberOfDice),
                        topBottomNumber
                    );
                }
            }
            else
            {
                if (topBottomNumber.HasValue)
                {
                    throw ExceptionHelper.CreateArgumentInvalidException(
                        () => topBottomNumber,
                        String.Format("Have a top bottom number but does not specify if is the top {0} rolls or bottom {0} rolls.", topBottomNumber),
                        topBottomNumber
                    );
                }
            }

            // Min number validation
            if (minNumber.HasValue)
            {
                if (minNumber.Value > numberOfSides)
                {
                    throw ExceptionHelper.CreateArgumentInvalidException(() => minNumber, "Min number is greater than number of sides.", minNumber);
                }
            }

            // Max number validation
            if (maxNumber.HasValue)
            {
                if (maxNumber.Value > numberOfSides)
                {
                    throw ExceptionHelper.CreateArgumentInvalidException(() => maxNumber, "Max number is greater than number of sides.", maxNumber);
                }

                if (minNumber.HasValue)
                {
                    if (minNumber.Value > maxNumber.Value)
                    {
                        throw ExceptionHelper.CreateArgumentInvalidException(() => minNumber, "Min number is greater than max number.", minNumber);
                    }
                }
            }

            // Store random
            _rand = rand;

            // Number of Dice
            _rolls = new List<int>(numberOfDice);
            _numberOfDice = numberOfDice;

            // Sides
            _numberOfSides = numberOfSides;

            // TopBottom Logic
            _topBottom = topBottom;
            _topBottomNumber = topBottomNumber;
            
            // Min and Max
            _minNumber = minNumber;
            _maxNumber = maxNumber;

            // Per roll add subtract
            _rollModifier = perRollAddSub;

        }

        #endregion

        #region Public Methods

        public int Calc()
        {
            _rolls.Clear();

            int currentMinNumber = 1;
            int currentMaxNumber = _numberOfSides;

            // Setup Min. Number
            if (_minNumber.HasValue && _minNumber.Value > currentMinNumber)
            {
                currentMinNumber = _minNumber.Value;
            }

            // Setup Max. Number
            if (_maxNumber.HasValue && _maxNumber.Value < currentMaxNumber)
            {
                currentMaxNumber = _maxNumber.Value;
            }
                
            // Roll dice
            for (int i = 0; i < _numberOfDice; i++)
            {
                _rolls.Add(_rand.Next(currentMinNumber, currentMaxNumber + 1) + _rollModifier);
            }

            // Calculate Top Bottom Sum
            int result;
            if (_topBottom != TopBottom.None)
            {
                _rolls.Sort();
                
                if (_topBottom == TopBottom.Top)
                {
                    _rolls.Reverse();
                }
                
                result = _rolls.Take(_topBottomNumber.Value).Sum();
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

            // New way
            var top = _topBottom != TopBottom.None ? _topBottomNumber.Value : _rolls.Count;
            var taken = _rolls.Take(top).ToArray();
            var skiped = _rolls.Skip(top).ToArray();

            string list = String.Join(", ", taken);
            if (skiped.Length > 0)
            {
                list += $", [{String.Join(", ", skiped)}]";
            }
            
            return (!partOfList || _rolls.Count > 1 ? $"{_statsCalcedValue}: " : "") + (partOfList ? $"({list})" : list);
        }

        #endregion

    }

}
