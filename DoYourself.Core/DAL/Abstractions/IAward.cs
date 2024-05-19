

namespace DoYourself.Core.DAL.Abstractions
{
    public interface IAward
    {
        public Guid Id { get; }
        public Guid TeamId { get; }
        public string Name { get; }
        public string Description { get; }
        public string Picture { get; }
        public int Price { get; }
        public int MinLevel { get; }
    }
}
