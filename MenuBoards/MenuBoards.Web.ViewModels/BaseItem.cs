using System;

namespace MenuBoards.Web.ViewModels
{
    public class BaseItem
    {
        public string Id { get; set; }

        public string AccountId { get; set; }

        public string Name { get; set; }

        #region Base Settings

        public string GoLiveDateTime { get; set; }

        #endregion
    }
}