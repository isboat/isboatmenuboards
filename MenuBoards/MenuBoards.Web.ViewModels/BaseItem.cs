using System;

namespace MenuBoards.Web.ViewModels
{
    public class BaseItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        #region Base Settings

        public DateTime GoLiveDateTime { get; set; }

        #endregion
    }
}