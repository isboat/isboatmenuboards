using System;
using System.Collections.Generic;
using System.Linq;
using MenuBoards.Interfaces.DataAccess;
using MenuBoards.Web.ViewModels;
using MenuBoards.Web.ViewModels.Images;

namespace MenuBoards.DataAccess
{
    public class MenuItemRepository: TimeStampRepository, IMenuItemRepository
    {
        private readonly List<MenuItem> items = new List<MenuItem>();

        private readonly ITimeStampRepository _timeStampRepository;

        public MenuItemRepository(ITimeStampRepository timeStampRepository)
        {
            _timeStampRepository = timeStampRepository;
        }

        // Consider using Unit of work pattern;

        public List<MenuItem> GetMenuItems(string menuId)
        {
            return this.items.FindAll(x => x.MenuId == menuId);
        }

        // Consider using Unit of work pattern;
        public BaseResponse SaveMenuItem(MenuItem item)
        {
            try
            {
                var existing = this.items.FirstOrDefault(x => x.Id == item.Id);

                if (existing != null)
                {
                    this.items.Remove(existing);
                }

                this.items.Add(item);

                this._timeStampRepository.UpdateTimeStamp(item.SlideId);

                return new BaseResponse { Success = true };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new BaseResponse();
            }
        }

        // Consider using Unit of work pattern;
        public DeleteResponse DeleteMenuItem(DeleteItem item)
        {
            var response = new DeleteResponse();
            try
            {
                var m = this.items.FirstOrDefault(x => x.Id == item.Id);
                response.ItemId = m?.MenuId;

                this.items.Remove(m);
                response.Success = true;

                this._timeStampRepository.UpdateTimeStamp(item.SlideId);

                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return response;
            }
        }

        // Consider using Unit of work pattern;
        public MenuItem GetItem(string id)
        {
            return this.items.FirstOrDefault(x => x.Id == id);
        }

        public BaseResponse UpdateImageUrl(AddImage image)
        {
            var item = this.items.FirstOrDefault(x => x.Id == image.ItemId);
            var response = new BaseResponse();
            if (item == null)
            {
                return response;
            }

            item.ImageUrl = image.SelectedImage;

            response.Success = true;

            this._timeStampRepository.UpdateTimeStamp(image.SlideId);
            return response;
        }
    }
}