namespace DoYourself.Core.DAL.Abstractions
{
    public interface ITag
    {
        public Guid Id { get; }
        public string Title { get; }
        public string Image { get; }
        public int Points { get; }
    }
}
