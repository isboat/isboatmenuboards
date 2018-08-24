namespace MenuBoards.Interfaces.Web
{
    public interface ISessionService
    {
        bool Contains(string key);
        T Get<T>(string key);
        void Remove(string key);
        void Set(string key, object obj);
    }
}