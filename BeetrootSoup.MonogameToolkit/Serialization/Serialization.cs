﻿namespace BeetrootSoup.MonogameToolkit.Helpers
{
    using BeetrootSoup.MonogameToolkit.Layout;
    using BeetrootSoup.MonogameToolkit.Serialization;

    using Microsoft.Xna.Framework.Graphics;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public static class Serialization
    {
        public static IDictionary<string,Texture2D> Textures;

        public static IDictionary<string,Effect> Effects;

        public static JsonSerializerSettings SerializerSettings;

        public static SpriteBatch SpriteBatch;

        static Serialization()
        {
            Textures = new Dictionary<string, Texture2D>();
            Effects = new Dictionary<string, Effect>();
            SerializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
        }

        public static string SerializeNode(this Node node)
        {
            var nodeDefinition = CreateNodeDefinition(node);
            return JsonConvert.SerializeObject(
                nodeDefinition,
                SerializerSettings);
        }

        public static NodeDefinition CreateNodeDefinition(Node node)
        {
            var result = new NodeDefinition
                             {
                                 Position = node.RelativePosition,
                                 Rotation = node.Rotation,
                                 LinearVelocity = node.LinearVelocity,
                                 AngularVelocity = node.AngularVelocity
                             };

            if (node is Scene)
            {
                var sceneNode = node as Scene;
                result.Type = "Scene";
                result.Name = sceneNode.SceneName;
            }

            if (node is Layer)
            {
                var layerNode = node as Layer;
                result.Type = "Layer";
                foreach (Effect effect in layerNode.Effects)
                {
                    result.Effects.Add(effect.Name);
                }
            }

            if (node is SpriteObject)
            {
                var spriteNode = node as SpriteObject;
                result.Type = "SpriteObject";
                result.Texture = spriteNode.Texture.Name;
                result.Scale = spriteNode.Scale;
            }
            
            foreach (Node childNode in node.Nodes)
            {
                var childNodeDefinition = CreateNodeDefinition(childNode);
                result.Nodes.Add(childNodeDefinition);
            }

            return result;
        }

        public static Node DeserializeNode(string jsonString)
        {
            
            var nodeDefinition = JsonConvert.DeserializeObject<NodeDefinition>(jsonString, SerializerSettings);

            var result = CreateNode(nodeDefinition);
           // SetOwners(result);
            return result;
        }

        public static Node CreateNode(NodeDefinition nodeDefinition)
        {
            Node result;
            switch (nodeDefinition.Type)
            {
                case "Scene":
                    result = new Scene(nodeDefinition.Name);
                    break;
                case "Layer":
                    var resultLayer = new Layer(SpriteBatch);
                    foreach (string effectName in nodeDefinition.Effects)
                    {
                        if (Effects.ContainsKey(effectName))
                            resultLayer.AddEffect(Effects[effectName]);
                    }

                    result = resultLayer;
                    break;
                case "SpriteObject":
                    var resultSprite = new SpriteObject(SpriteBatch);
                    if (Textures.ContainsKey(nodeDefinition.Texture))
                        resultSprite.Texture = Textures[nodeDefinition.Texture];
                    resultSprite.Scale = nodeDefinition.Scale;
                    result = resultSprite;
                    break;
                case "ComplexObject":
                    result = new ComplexObject();
                    break;
                default:
                    throw new TypeInitializationException(nodeDefinition.Type, null);
            }

            result.RelativePosition = nodeDefinition.Position;
            result.Rotation = nodeDefinition.Rotation;
            result.LinearVelocity = nodeDefinition.LinearVelocity;
            result.AngularVelocity = nodeDefinition.AngularVelocity;

            foreach (NodeDefinition innerNode in nodeDefinition.Nodes)
            {
                var node = CreateNode(innerNode);
                node.Owner = result;
                result.Nodes.Add(node);
            }

            return result;
        }

        private static void SetOwners(Node node)
        {
            foreach (Node innerNode in node.Nodes)
            {
                innerNode.Owner = node;
                SetOwners(innerNode);
            }
        }
    }
}
