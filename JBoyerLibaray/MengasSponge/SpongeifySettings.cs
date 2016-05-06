using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.MengasSponge
{
    public class SpongeifySettings
    {
        private Color _color = Color.Transparent;
        public Color Color { get { return _color; } }
        
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
