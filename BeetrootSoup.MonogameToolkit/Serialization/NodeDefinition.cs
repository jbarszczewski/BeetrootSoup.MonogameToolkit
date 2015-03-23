namespace BeetrootSoup.MonogameToolkit.Serialization
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    public class NodeDefinition
    {
        public string Type;

        public string Name;

        public Vector2 Position;

        public float Rotation;

        public Vector2 LinearVelocity;

        public float AngularVelocity;

        public string Texture;

        public Vector2 Scale;

        public IList<string> Effects;

        public IList<NodeDefinition> Nodes;

        public NodeDefinition()
        {
            this.Nodes = new List<NodeDefinition>();
            this.Effects = new List<string>();
        }
    }
}
