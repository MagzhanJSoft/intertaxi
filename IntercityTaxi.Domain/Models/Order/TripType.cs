using IntercityTaxi.Domain.Interfaces;

namespace IntercityTaxi.Domain.Models.Order
{
    public class TripType
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        private TripType(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public static Result<TripType> Create(string name)
        {
            return Result<TripType>.Success(new TripType(Guid.NewGuid(), name));
        }
    }
}
