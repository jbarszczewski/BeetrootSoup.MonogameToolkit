using BeetrootSoup.MonogameToolkit.Helpers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeetrootSoup.MonogameToolkit.Layout
{
    public class CollisionNode : Node
    {
        public RotatedRectangle ColisionRectangle { get; set; }

        public override void Update(GameTime gameTime)
        {
            if (this.ColisionRectangle != null)
            {
                var absolutePosition = this.GetAbsolutePosition();
                this.ColisionRectangle.CollisionRectangle.X = (int)(absolutePosition.X - this.ColisionRectangle.Origin.X);
                this.ColisionRectangle.CollisionRectangle.Y = (int)(absolutePosition.Y - this.ColisionRectangle.Origin.Y);
                this.ColisionRectangle.Rotation = this.GetAbsoluteRotation();
            }

            base.Update(gameTime);
        }
    }
}
