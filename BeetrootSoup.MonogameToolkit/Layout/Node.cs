namespace BeetrootSoup.MonogameToolkit.Layout
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public abstract class Node
    {
        public Vector2 RelativePosition;
        public float Rotation;
        public Node Owner { get; set; }
        public IList<Node> Nodes { get; set; }

         public Vector2 LinearVelocity;
        public float AngularVelocity;
        
        protected Node()
        {
            this.Nodes = new List<Node>();
            this.LinearVelocity = Vector2.Zero;
            this.AngularVelocity = 0f;
        }

        public void AddNode(Node node)
        {
            this.Nodes.Add(node);
            node.Owner = this;
        }

        public void RemoveNode(Node node, bool recursively = false)
        {
            if (recursively)
            {
                foreach (var innerNode in this.Nodes)
                {
                    innerNode.RemoveNode(node, recursively);
                }
            }

            this.Nodes.Remove(node);
            node.Owner = null;
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
            this.RelativePosition.X += this.LinearVelocity.X;
            this.RelativePosition.Y += this.LinearVelocity.Y;
            this.Rotation += this.AngularVelocity;

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