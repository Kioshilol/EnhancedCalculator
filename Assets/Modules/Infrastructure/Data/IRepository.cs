namespace Infrastructure.Data
{
    public interface IRepository<T>
    {
        T Load();
        void Save(T data);
    }
}
