using MagicVilla_API.Models.DTO;

namespace MagicVilla_API.Data
{
    public static class VillaStore
    {
        public static List<VillaDto> VillaList = new List<VillaDto>
        {
            new VillaDto{Id=1, Name="Vista a la playa", Occupants=3, SquareMeters=50},
            new VillaDto{Id=2, Name="Vista a la piscina", Occupants=4, SquareMeters=70}
        };
    }
}
