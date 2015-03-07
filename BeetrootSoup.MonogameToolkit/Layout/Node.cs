namespace BeetrootSoup.MonogameToolkit.Layout
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Collections.Generic;

    public abstract class Node
    {
        public Vector2 RelativePosition { get; set; }
        public float Rotation { get; set; }
        public Node Owner { get; set; }
        public IList<Node> Nodes { get; set; }

        protected Node()
        {
            this.Nodes = new List<Node>();
        }

        public void AddNode(Node node)
        {
            this.Nodes.Add(node);
            node.Owner = this;
        }

        public void RemoveNode(Node node)
        {
            node.Owner = null;
            this.Nodes.Remove(node);
        }

        public Vector2 GetAbsolutePosition()
        {
            if (this.Owner == null)
            {
                return this.RelativePosition;
            }

            ////Remark: manual calculations were faster than:
            ////var result2 = Vector2.Transform(this.Position, Matrix.CreateRotationZ(this.Owner.Rotation));
            var rotationCos = Math.Cos(this.Owner.Rotation);
            var rotationSin = Math.Sin(this.Owner.Rotation);
            var result = new Vector2
                             {
                                 X = (float)(this.RelativePosition.X * rotationCos - this.RelativePosition.Y * rotationSin),
                                 Y = (float)(this.RelativePosition.Y * rotationCos + this.RelativePosition.X * rotationSin)
                             };

            return result + this.Owner.GetAbsolutePosition();
        }

        public float GetAbsoluteRotation()
        {
            if (this.Owner == null)
            {
                return this.Rotation;
            }

            return this.Rotation + this.Owner.GetAbsoluteRotation();
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var node in this.Nodes)
            {
                node.Update(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime)
        {
            foreach (var node in this.Nodes)
            {
                node.Draw(gameTime);
            }
        }
    }
}