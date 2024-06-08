namespace NCA.Common.Application.Repositories
{
    public interface IUnitWorkTransaction : IDisposable
    {
        Task Commit();
        Task Rollback();
    }
}
