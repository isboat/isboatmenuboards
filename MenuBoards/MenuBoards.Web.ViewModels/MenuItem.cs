using System.Collections.Generic;

namespace MenuBoards.Web.ViewModels
{
    public class MenuItem
    {
        public string Id { get; set; }

        public string Label { get; set; }

        public decimal Price { get; set; }

        public string SizeText { get; set; }

        public string SubText { get; set; }
    }
}