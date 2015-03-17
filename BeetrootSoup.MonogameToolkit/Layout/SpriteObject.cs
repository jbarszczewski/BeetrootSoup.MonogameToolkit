namespace BeetrootSoup.MonogameToolkit.Layout
{
    using BeetrootSoup.MonogameToolkit.Helpers;
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
                this.ColisionRectangle = new RotatedRectangle(new Rectangle( (int)absolutePosition.X, (int)absolutePosition.Y, this.Texture.Width, this.Texture.Height), this.GetAbsoluteRotation());
                this.ColisionRectangle.Origin = new Vector2(this.Texture.Width / 2f, this.Texture.Height / 2f);
            }
        }

        public RotatedRectangle ColisionRectangle { get; set; }

        public Vector2 Scale
        {
            get
            {
                return this.scale;
            }
            set
            {
                this.scale = value;
                if (this.ColisionRectangle != null)
                {
                    this.ColisionRectangle.CollisionRectangle.Width = (int)(this.Texture.Width * this.Scale.X);
                    this.ColisionRectangle.CollisionRectangle.Height = (int)(this.Texture.Height * this.Scale.Y);
                    this.ColisionRectangle.Origin = new Vector2(this.Texture.Width * this.Scale.X / 2f, this.Texture.Height * this.Scale.Y / 2f);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (this.ColisionRectangle != null)
            {
                var absolutePosition = this.GetAbsolutePosition();
                this.ColisionRectangle.CollisionRectangle.X = (int)absolutePosition.X;
                this.ColisionRectangle.CollisionRectangle.Y = (int)absolutePosition.Y;
                this.ColisionRectangle.CollisionRectangle.Width = (int)(this.Texture.Width * this.Scale.X);
                this.ColisionRectangle.CollisionRectangle.Height = (int)(this.Texture.Height * this.Scale.Y);
                this.ColisionRectangle.Rotation = this.GetAbsoluteRotation();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(
                texture: this.Texture,
                position: base.GetAbsolutePosition(),
                rotation: base.GetAbsoluteRotation(),
                scale: this.Scale,
                origin: new Vector2(this.Texture.Width / 2f, this.Texture.Height / 2f));

            base.Draw(gameTime);
        }
    }
}
