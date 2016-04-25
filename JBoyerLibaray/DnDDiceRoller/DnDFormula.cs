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
        private Regex _forumlaTest;
        private Random _rand;
        private List<DnDFormulaCalc> _items = new List<DnDFormulaCalc>();
        private char[] _specialChars = { 'm', 'M', 'l', 'h' };
        private string _formula;
        private int _minNumber;
        private int _maxNumber;
        private int _topBottomNumber;
        private TopBottom _topBottom;
        private int _lastRoll;

        public DnDFormula(string formula) : this(formula, new Random()) { }

        public DnDFormula(string formula, Random rand)
        {
            _rand = rand;
            _formula = String.Copy(formula);

            Regex spliter = new Regex(@" [+-] ");
            string letters = string.Join("", _specialChars);
            _forumlaTest = new Regex(String.Format(@"^(((\d+[{0}])*\d+d\d+([\+\-]\d|)*|\d)( [+-] |))*$", letters));

            if (!_forumlaTest.IsMatch(formula))
                throw new ArgumentException("Not a DnD formula");

            string[] items = spliter.Split(formula);
            foreach (var item in items)
            {
                _minNumber = 0;
                _maxNumber = 0;
                _topBottomNumber = 0;
                _topBottom = TopBottom.Not;

                int intParse;
                if (Int32.TryParse(item, out intParse))
                {
                    continue;
                }
                bool containsL = item.Contains('l');
                bool containsH = item.Contains('h');
                if (containsH && containsL)
                {
                    throw new ArgumentException(String.Format("'{0}' is in a invalid state. Cannot have l and h in the same claculation", item));
                }
                foreach (char charictar in _specialChars)
                {
                    if (item.Count(c => c == charictar) > 1)
                    {
                        throw new ArgumentException(String.Format("'{0}' is in a invalid state. Has more thane one '{1}'", item, charictar));
                    }

                    ProcessSpecialChar(item, charictar);
                }

                Match match = Regex.Match(item, @"\d+d\d+((\+|\-)\d+|)");
                MatchCollection collection = Regex.Matches(match.ToString(), @"([+-]\d+|\d+)");

                string[] numbers = new string[] {
                    collection[0].ToString(),
                    collection[1].ToString(),
                    collection.Count > 2 ? collection[2].ToString() : "0"
                };

                _items.Add(new DnDFormulaCalc(_rand, int.Parse(numbers[0]), int.Parse(numbers[1]), int.Parse(numbers[2]), _topBottomNumber, _topBottom, _minNumber, _maxNumber));

                int start = _formula.IndexOf(item);
                int end = start + item.Length;

                _formula = _formula.Substring(0, start) + String.Format("{{{0}}}", _items.Count - 1) + _formula.Substring(end); 
            }
        }

        public int Roll()
        {
            var values = (from i in _items
                          select (Object)i.Calc()).ToArray();

            string getMathForumla = String.Format(_formula, values);

            _lastRoll = (int)Evaluate(getMathForumla);

            return _lastRoll;
        }

        public string Stats
        {
            get
            {
                if (_lastRoll == 0)
                {
                    return null;
                }
                int plusMinusCount = _formula.Count(c => c == '-' || c == '+');
                var stats = (from s in _items
                             select (Object)s.Stats(plusMinusCount > 0)).ToArray();
                
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

        private double Evaluate(string expression)
        {
            DataTable table = new DataTable();
            table.Columns.Add("expression", typeof(string), expression);
            DataRow row = table.NewRow();
            table.Rows.Add(row);
            return double.Parse((string)row["expression"]);
        }

        private void ProcessSpecialChar(string item, char specialChar)
        {
            Match match = Regex.Match(item, @"\d+" + specialChar);
            string number = match.ToString();
            if (number != String.Empty)
            {
                number = number.Substring(0, number.Length - 1);
                switch(specialChar)
                {
                    case 'm':
                        _minNumber = int.Parse(number);
                        break;
                    case 'M':
                        _maxNumber = int.Parse(number);
                        break;
                    case 'l':
                        _topBottomNumber = int.Parse(number);
                        _topBottom = TopBottom.Bottom;
                        break;
                    case 'h':
                        _topBottomNumber = int.Parse(number);
                        _topBottom = TopBottom.Top;
                        break;
                    default:
                        throw new NotImplementedException(String.Format("'{0}' is not implementd", specialChar));
                }
            }
        }
    }
}
