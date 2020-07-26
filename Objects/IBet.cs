using RuletaOnline.Objects.Enums;

namespace RuletaOnline.Objects
{
    public interface IBet
    {
        long GetRouletteId();
        string GetUser();
        int GetAmount();
        int? GetBetNumber();
        RouletteColors? GetBetColor();
        BetTypes GetBetType();
    }
}