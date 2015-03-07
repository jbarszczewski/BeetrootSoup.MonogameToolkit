namespace BeetrootSoup.MonogameToolkit.Layout
{
    using BeetrootSoup.MGToolkit;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class SpriteObject : Node
    {
        private readonly SpriteBatch spriteBatch;

        private Texture2D texture;

        private Vector2 scale;

        public SpriteObject(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            this.Scale = Vector2.One;
        }

        public Texture2D Texture
        {
            get
            {
                return this.texture;
            }
            set
            {
                this.texture = value;
                var absolutePosition = this.GetAbsolutePosition();
                this.BoundingRectangle = new RotatedRectangle(new Rectangle( (int)absolutePosition.X, (int)absolutePosition.Y, this.Texture.Width, this.Texture.Height), this.GetAbsoluteRotation());
            }
        }

        public RotatedRectangle BoundingRectangle { get; set; }

        public Vector2 Scale
        {
            get
            {
                return this.scale;
            }
            set
            {
                this.scale = value;
                if (this.BoundingRectangle != null)
                {
                    this.BoundingRectangle.CollisionRectangle.Width = (int)(this.Texture.Width * this.Scale.X);
                    this.BoundingRectangle.CollisionRectangle.Width = (int)(this.Texture.Height * this.Scale.Y);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (this.BoundingRectangle != null)
            {
                var absolutePosition = this.GetAbsolutePosition();
                this.BoundingRectangle.CollisionRectangle.X = (int)absolutePosition.X;
                this.BoundingRectangle.CollisionRectangle.Y = (int)absolutePosition.Y;
                this.BoundingRectangle.CollisionRectangle.Width = (int)(this.Texture.Width * this.Scale.X);
                this.BoundingRectangle.CollisionRectangle.Width = (int)(this.Texture.Height * this.Scale.Y);
                this.BoundingRectangle.Rotation = this.GetAbsoluteRotation();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(
                texture: this.Texture,
                position: base.GetAbsolutePosition(),
                rotation: base.GetAbsoluteRotation(),
                scale: this.Scale,
                origin: new Vector2(this.Texture.Width / 2f, this.Texture.Height / 2f));
        }
    }
}
