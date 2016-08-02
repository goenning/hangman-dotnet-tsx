using System;

namespace Hangman.Api.Storages
{
    public interface IGameStorage
    {
        Game Get(Guid id);
        Guid Store(Game game);
    }
}