using System.Drawing;
using System.Collections.Generic;

namespace TravelEurope
{
    interface IDrawable
    {
        /// <summary>
        /// Draw a component using graphics g with specified color c
        /// </summary>
        /// <param name="g"></param>
        void Draw(Graphics g, Color c);
    }
}
