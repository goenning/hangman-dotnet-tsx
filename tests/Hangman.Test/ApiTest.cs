using Hangman.Api.Controllers;
using System.Web.Http.Results;
using Xunit;

namespace Hangman.Test
{
    public class ApiTest
    {
        [Fact]
        public void ShouldStartNewGameWhenPostingToNew()
        {
            var controller = new GameController();
            var result = controller.New("banana") as OkNegotiatedContentResult<GameStateResult>;
            Assert.NotNull(result);
            Assert.NotNull(result.Content.Id);
            Assert.Equal(result.Content.Letters, new char[] { '_', '_', '_', '_', '_', '_' });
            Assert.Equal(result.Content.Status, GameStatus.InProgress);
            Assert.Null(result.Content.Word);
            Assert.Equal(result.Content.Guesses, new char[0]);
        }

        [Fact]
        public void ShouldAddToGuessListWhenPostingToGameGuess()
        {
            var controller = new GameController();
            var game = controller.New("banana") as OkNegotiatedContentResult<GameStateResult>;
            var result = controller.Guess(game.Content.Id, 'a') as OkNegotiatedContentResult<GameStateResult>;
            Assert.NotNull(result);
            Assert.Equal(result.Content.Letters, new char[] { '_', 'a', '_', 'a', '_', 'a' });
            Assert.Equal(result.Content.Status, GameStatus.InProgress);
            Assert.Null(result.Content.Word);
            Assert.Equal(result.Content.Guesses, new char[] { 'a' });
        }

        [Fact]
        public void ShouldReturnWordWhenGameIsWon()
        {
            var controller = new GameController();
            var game = controller.New("banana") as OkNegotiatedContentResult<GameStateResult>;
            controller.Guess(game.Content.Id, 'a');
            controller.Guess(game.Content.Id, 'b');
            var result = controller.Guess(game.Content.Id, 'n') as OkNegotiatedContentResult<GameStateResult>;
            Assert.NotNull(result);
            Assert.Equal(result.Content.Letters, new char[] { 'b', 'a', 'n', 'a', 'n', 'a' });
            Assert.Equal(result.Content.Status, GameStatus.Won);
            Assert.Equal(result.Content.Word, "banana");
            Assert.Equal(result.Content.Guesses, new char[] { 'a', 'b', 'n' });
        }

        [Fact]
        public void ShouldReturnWordWhenGameIsLost()
        {
            var controller = new GameController();
            var game = controller.New("banana") as OkNegotiatedContentResult<GameStateResult>;
            controller.Guess(game.Content.Id, '1');
            controller.Guess(game.Content.Id, '2');
            controller.Guess(game.Content.Id, '3');
            controller.Guess(game.Content.Id, '4');
            var result = controller.Guess(game.Content.Id, '5') as OkNegotiatedContentResult<GameStateResult>;
            Assert.NotNull(result);
            Assert.Equal(result.Content.Letters, new char[] { '_', '_', '_', '_', '_', '_' });
            Assert.Equal(result.Content.Status, GameStatus.Lost);
            Assert.Equal(result.Content.Word, "banana");
            Assert.Equal(result.Content.Guesses, new char[] { '1', '2', '3', '4', '5' });
        }
    }
}
