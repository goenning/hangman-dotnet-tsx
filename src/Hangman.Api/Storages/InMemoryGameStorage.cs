using System;
using System.Collections.Generic;

namespace Hangman.Api.Storages
{
    public class InMemoryGameStorage : IGameStorage
    {
        private static Dictionary<Guid, Game> instances = new Dictionary<Guid, Game>();

        public Game Get(Guid id)
        {
            return instances[id];
        }

        public Guid Store(Game game)
        {
            var id = Guid.NewGuid();
            instances.Add(id, game);
            return id;
        }
    }
}