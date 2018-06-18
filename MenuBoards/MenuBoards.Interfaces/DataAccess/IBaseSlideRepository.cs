namespace MenuBoards.Interfaces.DataAccess
{
    public interface IBaseSlideRepository
    {
        void SetDateTimeStamp(string slideId, string dateTimeStamp);

        string GetDateTimeStamp(string slideId);
    }
}