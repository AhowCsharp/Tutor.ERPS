namespace TUTOR.Biz.SeedWork
{
    public interface IDTO
    {
    }

    public interface IDTO<TIIdentity>
        : IDTO
    {
        public TIIdentity Id { get; set; }
    }
}