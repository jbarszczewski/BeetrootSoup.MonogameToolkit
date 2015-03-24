using Microsoft.Xna.Framework;
using System;

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

        public static Vector2 GetRandomVector2(float maxVariation)
        {
            return GetRandomVector2(-maxVariation, maxVariation, -maxVariation, maxVariation);
        }

        public static int GetRandomInt(int iMin, int iMax)
        {
            return random.Next(iMin, iMax);
        }

        public static int GetRandomSignFloat()
        {
            return random.NextDouble() > 0.5 ? 1 : -1;
        }
    }
}
