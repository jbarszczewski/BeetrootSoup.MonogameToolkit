
namespace BeetrootSoup.MonogameToolkit.Layout
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class AnimatedSpriteNode : Node
    {
        private readonly SpriteBatch spriteBatch;

        private double animationTimer;

        public AnimatedSpriteNode(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            this.Scale = Vector2.One;
        }

        //[JsonConverter(typeof(TextureSerializationConverter))]
        public Texture2D Texture { get; set; }

        public Vector2 Scale { get; set; }

        public int XFramesCount { get; set; }

        public int CurrentFrameX { get; set; }

        public int YFramesCount { get; set; }

        public int CurrentFrameY { get; set; }

        public int FrameWidth { get; set; }

        public int FrameHeight { get; set; }

        public double AnimationFrameTime { get; set; }

        public override void Update(GameTime gameTime)
        {
            this.animationTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (this.animationTimer > this.AnimationFrameTime)
            {
                this.animationTimer = 0;
                this.CurrentFrameX++;
                if (this.CurrentFrameX == this.XFramesCount)
                {
                    this.CurrentFrameX = 0;
                    this.CurrentFrameY++;
                    if (this.CurrentFrameY == this.YFramesCount)
                        this.CurrentFrameY = 0;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(
                texture: this.Texture,
                position: base.GetAbsolutePosition(),
                sourceRectangle: new Rectangle(this.FrameWidth * this.CurrentFrameX, this.FrameHeight * this.CurrentFrameY, this.FrameWidth, this.FrameHeight),
                origin: new Vector2(this.FrameWidth / 2f, this.FrameHeight / 2f),
                rotation: base.GetAbsoluteRotation(),
                scale: this.Scale);

            base.Draw(gameTime);
        }
    }
}
