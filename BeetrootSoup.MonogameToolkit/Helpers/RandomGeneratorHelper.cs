using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeetrootSoup.MonogameToolkit.Helpers
{
    public static class RandomGeneratorHelper
    {
        public static Random random = new Random();

        public static float GetRandomFloat(float fmin, float fmax)
        {
            return (float)random.NextDouble() * (fmax - fmin) + fmin;
        }

        public static double GetRandomDouble(double dMin, double dMax)
        {
            return random.NextDouble() * (dMax - dMin) + dMin;
        }

        public static Vector2 GetRandomVector2(float xMin, float xMax, float yMin, float yMax)
        {
            return new Vector2(GetRandomFloat(xMin, xMax), GetRandomFloat(yMin, yMax));
        }

        public static int GetRandomInt(int iMin, int iMax)
        {
            return random.Next(iMin, iMax);
        }
    }
}
