namespace BeetrootSoup.MonogameToolkit.Layout
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Xna.Framework.Graphics;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class TextureSerializationConverter : JsonConverter
    {
        private readonly IList<Texture2D> Textures;

        public TextureSerializationConverter(IList<Texture2D> textures)
        {
            this.Textures = textures;
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Texture2D));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var texture = (Texture2D)value;
            var itemObj = new JObject();

            itemObj.Add("TextureName", new JValue(texture.Name));
            itemObj.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}