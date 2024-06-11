namespace NCA.Common.Application.Repositories
{
    public interface IUnitWork
    {
        bool HasActiveTransaction { get; }
        public Task<IUnitWorkTransaction> Transaction();
        Task Save();
#pragma warning disable S2953 // Methods named "Dispose" should implement "IDisposable.Dispose"
        Task Dispose();
#pragma warning restore S2953 // Methods named "Dispose" should implement "IDisposable.Dispose"
    }
}
