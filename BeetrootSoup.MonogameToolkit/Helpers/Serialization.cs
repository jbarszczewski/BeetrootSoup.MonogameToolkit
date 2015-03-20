namespace BeetrootSoup.MonogameToolkit.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using BeetrootSoup.MonogameToolkit.Layout;

    using Microsoft.Xna.Framework.Graphics;

    using Newtonsoft.Json;
    public static class Serialization
    {
        public static IList<Texture2D> Textures;

        static Serialization()
        {
            Textures = new List<Texture2D>();
        }

        public static string SerializeNode(this Node node)
        {
            return JsonConvert.SerializeObject(
                node,
                Formatting.Indented,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, TypeNameHandling = TypeNameHandling.Auto, PreserveReferencesHandling = PreserveReferencesHandling.Objects, Converters = new List<JsonConverter>{ new TextureSerializationConverter(Textures)}});
        }


        public static Node DeserializeNode(string jsonString, Type objectType)
        {
            //Node result;
            //switch (objectType.Name)
            //{
            //    case "Scene":
            //        result = JsonConvert.DeserializeObject<Scene>(jsonString, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            //        break;
            //    default:
            //        result = JsonConvert.DeserializeObject<Node>(jsonString, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            //        break;
            //}
            
            //SetOwners(result);
            //return result;
            throw new NotImplementedException();
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
