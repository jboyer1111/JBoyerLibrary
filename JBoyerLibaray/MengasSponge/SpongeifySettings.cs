using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.MengasSponge
{
    [ExcludeFromCodeCoverage]
    public class SpongeifySettings
    {
        private Color _holeColor = Color.Transparent;
        public Color HoleColor { get { return _holeColor; } }

        private Color _baseColor = Color.White;
        public Color BaseColor { get { return _baseColor; } }

        private SpongeifySettings()
        {

        }
        
        private static SpongeifySettings _default = new SpongeifySettings();

        public static SpongeifySettings DefaultSettings { get { return _default; }}

        public static SpongeifySettings CreateCustomSettings()
        {
            return new SpongeifySettings();
        }
    }
}
