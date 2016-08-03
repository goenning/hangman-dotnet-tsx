using System.Collections.Generic;
using Hangman.Api;
using Xunit;

namespace Hangman.Test
{
    public class GameSerializerTest
    {
        [Theory, MemberData("SerializeTestData")]
        public void SerializeTest(Game game, string expected)
        {
            string serialized = GameSerializer.Serialize(game);
            Assert.Equal(expected, serialized);
        }

        public static IEnumerable<object> SerializeTestData
        {
            get
            {
                var newHomeGame = new Game("Home");
                var homeGameGuessG = new Game("Home");
                homeGameGuessG.Guess('g');

                return new object[] { 
                    new object[] { newHomeGame, "{\"word\":\"Home\",\"guesses\":[]}" },
                    new object[] { homeGameGuessG, "{\"word\":\"Home\",\"guesses\":[\"g\"]}" }
                 };
            }
        }
        [Theory, MemberData("DeserializeTestData")]
        public void DeserializeTest(string json, Game expected)
        {
            Game game = GameSerializer.Deserialize(json);
            Assert.Equal(expected.Word, game.Word);
            Assert.Equal(expected.Guesses, game.Guesses);
            Assert.Equal(expected.RemainingMissesCount, game.RemainingMissesCount);
            Assert.Equal(expected.Letters, game.Letters);
        }

        public static IEnumerable<object> DeserializeTestData
        {
            get
            {
                var newHomeGame = new Game("Home");
                var homeGameGuessG = new Game("Home");
                homeGameGuessG.Guess('g');

                return new object[] { 
                    new object[] { "{\"word\":\"Home\",\"guesses\":[]}", newHomeGame },
                    new object[] { "{\"word\":\"Home\",\"guesses\":[\"g\"]}", homeGameGuessG }
                 };
            }
        }
    }
}
