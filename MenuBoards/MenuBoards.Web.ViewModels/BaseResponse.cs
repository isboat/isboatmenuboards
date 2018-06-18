namespace MenuBoards.Web.ViewModels
{
    public class BaseResponse
    {
        public BaseResponse(){}

        public BaseResponse(string message)
        {
            this.Message = message;
        }

        public bool Success { get; set; }

        public string Message { get; set; }
    }

    public class DeleteResponse : BaseResponse
    {
        public string SlideId { get; set; }
    }
}