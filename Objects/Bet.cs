using System;
using RuletaOnline.ExceptionMiddlewares;
using RuletaOnline.Objects.Enums;

namespace RuletaOnline.Objects
{
    public class Bet : IBet
    {
        private readonly long rouletteId;
        private readonly string user;
        private readonly int amount;
        private readonly int? betNumber;
        private readonly RouletteColors? betColor;
        public Bet(long rouletteId, string user, int amount, int? betNumber, string betColor)
        {
            ValidateAttributes(rouletteId: rouletteId, user: user, amount: amount, betNumber: betNumber, betColor: betColor);
            this.user = user;
            this.amount = amount;
            this.rouletteId = rouletteId;
            this.betNumber = betNumber;
            this.betColor = GetRouletteColorEnum(betColor);
        }
        private void ValidateAttributes(long rouletteId, string user, int amount, int? betNumber, string betColor)
        {
            if (string.IsNullOrEmpty(user))
                throw new HttpResponseException("No se ha ingresado un usuario.");
            if (amount <= 0)
                throw new HttpResponseException("La cantidad apostada no puede ser negativa.");
            if (amount > 10000)
                throw new HttpResponseException("La cantidad máxima a apostar es 10.000.");
            if (betNumber is null && string.IsNullOrEmpty(betColor))
                throw new HttpResponseException("Debe ingresar una apuesta.");
            if (!(betNumber is null) && !string.IsNullOrEmpty(betColor))
                throw new HttpResponseException("Solo puede ingresar un número o un color por apuesta.");
            if (!(betNumber is null) && betNumber < 0 || betNumber > 36)
                throw new HttpResponseException("El rango de apuesta debe ser entre el 0 y el 36.");
            if (!string.IsNullOrEmpty(betColor) && !Enum.IsDefined(typeof(RouletteColors), betColor))
                throw new HttpResponseException("El color ingresado debe ser negro o rojo.");
        }

        private RouletteColors? GetRouletteColorEnum(string betColor)
        {
            if (string.IsNullOrEmpty(betColor))
                return null;
            return (RouletteColors)System.Enum.Parse(typeof(RouletteColors), betColor);
        }

        public long GetRouletteId()
        {
            return rouletteId;
        }

        public string GetUser()
        {
            return user;
        }
        public int GetAmount()
        {
            return amount;
        }
        public int? GetBetNumber()
        {
            return betNumber;
        }
        public RouletteColors? GetBetColor()
        {
            return betColor;
        }

        public BetTypes GetBetType()
        {
            if (betNumber == null && betColor != null)
                return BetTypes.color;
            else
                return BetTypes.number;
        }
    }
}