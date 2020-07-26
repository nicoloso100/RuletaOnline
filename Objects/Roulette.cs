using MongoDB.Bson.Serialization.Attributes;

namespace RuletaOnline.Objects
{
    public sealed class Roulette : IRoulette
    {
        private readonly long id;
        private readonly RouletteStates state;
        public Roulette(long id, RouletteStates state)
        {
            this.id = id;
            this.state = state;
        }

        public long GetId()
        {
            return id;
        }

        public RouletteStates GetState()
        {
            return state;
        }
    }
}