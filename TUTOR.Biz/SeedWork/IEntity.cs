namespace TUTOR.Biz.SeedWork
{
    public interface IEntity
    {
    }

    public interface IEntity<TIIdentity> : IEntity
    {
        TIIdentity Id { get; set; }
    }
}