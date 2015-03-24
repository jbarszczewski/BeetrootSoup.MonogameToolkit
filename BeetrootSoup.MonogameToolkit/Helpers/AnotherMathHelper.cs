using Microsoft.Xna.Framework;
using System;

namespace BeetrootSoup.MonogameToolkit.Helpers
{
    public static class AnotherMathHelper
    {
       public static int GetSign(int number)
       {
           return number < 0 ? -1 : 1;
       }

        public static float GetSign(float number)
        {
            return number < 0 ? -1f : 1f;
        }
    }
}
