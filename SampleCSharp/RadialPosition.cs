using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCSharp
{
    class RadialPosition
    {
        public Tuple<int, int> GetPosition(byte ItemNumber, byte ItemCount, int CanvasHeight)
        {
            // Determine position of the button on the dial radius
            double ang = (ItemNumber * ((2 * Math.PI) / ItemCount));
            int rad = CanvasHeight / 2;
            int x = Convert.ToInt16(((Math.Cos(ang) * rad) + rad));
            int y = Convert.ToInt16((rad - (Math.Sin(ang) * rad)));
            return Tuple.Create (x,y);
        }
    }
}
