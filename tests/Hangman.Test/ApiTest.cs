using Xunit;
using Hangman.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Hangman.Api.ViewModels;
using Hangman.Api.Storages;

namespace Hangman.Test
{
    public class ApiTest
    {
        private GameController controller;

        public ApiTest()
        {
            this.controller = new GameController(new InMemoryGameStorage());
        }

        [Fact]
        public void ShouldStartNewGameWhenPostingToNew()
        {
            var result = this.controller.New("banana") as OkObjectResult;
            var content = result.Value as GameStateResult;
            Assert.NotNull(result);
            Assert.NotNull(content.Id);
            Assert.Equal(content.Letters, new char[] { '_', '_', '_', '_', '_', '_' });
            Assert.Equal(content.Status, GameStatus.InProgress);
            Assert.Null(content.Word);
            Assert.Equal(content.Guesses, new char[0]);
        }

        [Fact]
        public void ShouldAddToGuessListWhenPostingToGameGuess()
        {
            var game = this.controller.New("banana") as OkObjectResult;
            var content = game.Value as GameStateResult;
            
            var result = controller.Guess(content.Id, 'a') as OkObjectResult;
            content = result.Value as GameStateResult;
            
            Assert.NotNull(result);
            Assert.Equal(content.Letters, new char[] { '_', 'a', '_', 'a', '_', 'a' });
            Assert.Equal(content.Status, GameStatus.InProgress);
            Assert.Null(content.Word);
            Assert.Equal(content.Guesses, new char[] { 'a' });
        }

        [Fact]
        public void ShouldReturnWordWhenGameIsWon()
        {
            var game = this.controller.New("banana") as OkObjectResult;
            var content = game.Value as GameStateResult;
            controller.Guess(content.Id, 'a');
            controller.Guess(content.Id, 'b');

            var result = controller.Guess(content.Id, 'n') as OkObjectResult;
            content = result.Value as GameStateResult;

            Assert.NotNull(result);
            Assert.Equal(content.Letters, new char[] { 'b', 'a', 'n', 'a', 'n', 'a' });
            Assert.Equal(content.Status, GameStatus.Won);
            Assert.Equal(content.Word, "banana");
            Assert.Equal(content.Guesses, new char[] { 'a', 'b', 'n' });
        }

        [Fact]
        public void ShouldReturnWordWhenGameIsLost()
        {
            var game = this.controller.New("banana") as OkObjectResult;
            var content = game.Value as GameStateResult;
            controller.Guess(content.Id, '1');
            controller.Guess(content.Id, '2');
            controller.Guess(content.Id, '3');
            controller.Guess(content.Id, '4');

            var result = controller.Guess(content.Id, '5') as OkObjectResult;
            content = result.Value as GameStateResult;

            Assert.NotNull(result);
            Assert.Equal(content.Letters, new char[] { '_', '_', '_', '_', '_', '_' });
            Assert.Equal(content.Status, GameStatus.Lost);
            Assert.Equal(content.Word, "banana");
            Assert.Equal(content.Guesses, new char[] { '1', '2', '3', '4', '5' });
        }
    }
}
