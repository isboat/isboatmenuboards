using System.Collections.Generic;

namespace MenuBoards.Web.ViewModels
{
    public class MenuItem
    {
        public string MenuId { get; set; }

        public string SlideId { get; set; }

        public string Id { get; set; }

        public string Label { get; set; }

        public decimal Price { get; set; }

        public string SizeText { get; set; }

        public string SubText { get; set; }

        public string ImageUrl { get; set; }
    }
}