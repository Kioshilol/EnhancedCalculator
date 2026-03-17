namespace Calculator.Data.Base
{
    public interface IRepository<T> 
    {
        T Load();
        void Save(T data);
    }
}
