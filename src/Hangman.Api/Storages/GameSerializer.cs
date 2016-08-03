using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hangman.Api
{
    public class GameConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Game);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            var word = obj.GetValue("word").Value<string>();
            var game = new Game(word);
            var guesses = obj.GetValue("guesses").AsJEnumerable();
            foreach(var guess in guesses)
                game.Guess(guess.Value<char>());

            return game;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Game game = value as Game;
            writer.WriteStartObject();
            writer.WritePropertyName("word");
            writer.WriteValue(game.Word);
            writer.WritePropertyName("guesses");
            writer.WriteStartArray();
            foreach (var guess in game.Guesses)
                writer.WriteValue(guess);
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
    }

    public static class GameSerializer
    {
        public static string Serialize(Game game)
        {
            return JsonConvert.SerializeObject(game, Formatting.None, new GameConverter());
        }
        public static Game Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<Game>(json, new GameConverter());
        }
    }
}
