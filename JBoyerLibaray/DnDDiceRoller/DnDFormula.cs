using JBoyerLibaray.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JBoyerLibaray.DnDDiceRoller
{
    public class DnDFormula
    {
        #region Private Variables

        private string _formula;
        private Random _rand;
        private char[] _specialChars = { 'm', 'M', 'l', 'h' };
        
        private List<DnDFormulaCalc> _items = new List<DnDFormulaCalc>();
        
        private int? _lastRoll = null;

        #endregion

        #region Public Properties

        #endregion

        #region Constructor / Init

        public DnDFormula(string formula) : this(formula, new Random()) { }

        public DnDFormula(string formula, Random rand)
        {
            if (String.IsNullOrWhiteSpace(formula))
            {
                throw ExceptionHelper.CreateArgumentInvalidException(() => formula, "Cannot be null, empty, or whitespace", formula);
            }

            if (rand == null)
            {
                throw ExceptionHelper.CreateArgumentNullException(() => rand);
            }

            string specialChars = String.Join("", _specialChars);
            var matches = Regex.Matches(formula, String.Format(@"[^ \dd+\-{0}]", specialChars));
            if (matches.Count > 0)
            {
                string message = PluralizeString.Pluralize("Formula contains {AN}invalid character{S}: ", matches.Count);

                for (int i = 0; i < matches.Count; i++)
                {
                    var test = matches[i].ToString();

                    message += test;
                }

                throw ExceptionHelper.CreateArgumentInvalidException(() => formula, message, formula);
            }

            _formula = formula;
            _rand = rand;
            
            string[] formulaParts = Regex.Split(formula, @" [+-] ");
            var formulaText = new Regex(String.Format(@"^((\d+[{0}])*\d+d\d+([\+\-]\d|)*|\d)$", specialChars));

            // Date Changed: 7-17-2017
            // Used Sense: 0
            //_forumlaTest = new Regex(String.Format(@"^(((\d+[{0}])*\d+d\d+([\+\-]\d|)*|\d)( [+-] |))*$", specialChars));

            //if (!_forumlaTest.IsMatch(formula))
            //    throw new ArgumentException("Not a DnD formula");

            foreach (var item in formulaParts)
            {
                if (!formulaText.IsMatch(item))
                {
                    throw ExceptionHelper.CreateArgumentInvalidException(() => formula, "Formula is invalid. Invalid part is '" + item + "'", formula);
                }
                
                int? minNumber = null;
                int? maxNumber = null;
                int? topBottomNumber = null;
                var topBottom = TopBottom.Not;

                // Check if part is just a number
                int intParse;
                if (Int32.TryParse(item, out intParse))
                {
                    continue;
                }

                // Validate h and l are not in same formula
                if (item.Contains('h') && item.Contains('l'))
                {
                    throw ExceptionHelper.CreateArgumentInvalidException(() => formula, "Cannot have l and h in the same calculation", item);
                }

                // Process each special char
                foreach (char character in _specialChars)
                {
                    if (item.Count(c => c == character) > 1)
                    {
                        throw ExceptionHelper.CreateArgumentInvalidException(() => formula, "Formula is invalid. Has too many '" + character + "' characters.", formula);
                    }

                    var characterMatch = Regex.Match(item, @"(\d+)" + character);
                    if (characterMatch.Success)
                    {
                        string number = characterMatch.Groups[1].ToString();
                        switch (character)
                        {
                            case 'm':
                                minNumber = Int32.Parse(number);
                                break;
                            case 'M':
                                maxNumber = Int32.Parse(number);
                                break;
                            case 'l':
                                topBottomNumber = Int32.Parse(number);
                                topBottom = TopBottom.Bottom;
                                break;
                            default: // Only h atm
                                topBottomNumber = Int32.Parse(number);
                                topBottom = TopBottom.Top;
                                break;
                        }
                    }
                }

                var dicePiece = Regex.Match(item, @"\d+d\d+((\+|\-)\d+|)");
                var dicePieceParts = Regex.Matches(dicePiece.ToString(), @"([+-]\d+|\d+)");

                int numDice = Int32.Parse(dicePieceParts[0].ToString());
                int sideDice = Int32.Parse(dicePieceParts[1].ToString());
                int addSubtractPart = dicePieceParts.Count > 2 ? Int32.Parse(dicePieceParts[2].ToString()) : 0;

                _items.Add(new DnDFormulaCalc(_rand, numDice, sideDice, addSubtractPart, topBottom, topBottomNumber, minNumber, maxNumber));

                int start = _formula.IndexOf(item);
                int end = start + item.Length;

                _formula = _formula.Substring(0, start) + String.Format("{{{0}}}", _items.Count - 1) + _formula.Substring(end); 
            }
        }

        #endregion

        #region Public Methods

        public int Roll()
        {
            var values = (from i in _items
                          select (Object)i.Calc()).ToArray();

            string getMathForumla = String.Format(_formula, values);

            _lastRoll = (int)Evaluate(getMathForumla);

            return _lastRoll.Value;
        }

        public string Stats
        {
            get
            {
                if (!_lastRoll.HasValue)
                {
                    return null;
                }

                int plusMinusCount = _formula.Count(c => c == '-' || c == '+');
                var stats = (
                    from s in _items
                    select (Object)s.Stats(plusMinusCount > 0)
                ).ToArray();
                
                string result;
                if (plusMinusCount > 0)
                {
                    string statsFormat = _formula.Replace(" + ", ", ").Replace(" - ", ", -");
                    statsFormat = Regex.Replace(statsFormat, @"(?<!\{[^{\}]*)\b\d+?\b(?![^{\}]*\})", s => String.Format("({0})", s));
                    result = String.Format("{0}: {1}", _lastRoll, String.Format(statsFormat, stats));
                }
                else
                {
                    result = stats[0].ToString();
                }

                return result;
            }
        }

        #endregion

        #region Private Methods

        private double Evaluate(string expression)
        {
            DataTable table = new DataTable();
            table.Columns.Add("expression", typeof(string), expression);
            DataRow row = table.NewRow();
            table.Rows.Add(row);
            return double.Parse((string)row["expression"]);
        }

        #endregion
    }
}
