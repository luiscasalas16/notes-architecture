namespace NCA.Common.Application.Repositories
{
    public interface IUnitWork
    {
        bool HasActiveTransaction { get; }
        public Task<IUnitWorkTransaction> Transaction();
        Task Save();
        Task Dispose();
    }
}
