namespace MenuBoards.Interfaces.DataAccess
{
    public interface ITimeStampRepository
    {
        void UpdateTimeStamp(string slideId);

        string GetTimeStamp(string slideId);
    }
}