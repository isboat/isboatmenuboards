using System;
using System.Collections.Generic;
using System.Linq;
using MenuBoards.Interfaces.DataAccess;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.DataAccess
{
    public class MenuSlideRepository: IMenuSlideRepository
    {
        private List<MenuSlide> menuSlides = new List<MenuSlide>();
        private List<Menu> menus = new List<Menu>();

        private Dictionary<string, string> dateTimeStamp = new Dictionary<string, string>();

        public string CreateMenuSlide(MenuSlide model)
        {
            model.Id = (this.menuSlides.Count + 1).ToString();
            this.menuSlides.Add(model);
            return model.Id;
        }

        public List<MenuSlide> GetAllMenuSlides()
        {
            return this.menuSlides;
        }

        public BaseResponse DeleteSlide(string slideId)
        {
            try
            {
                var m = this.menuSlides.FirstOrDefault(x => x.Id == slideId);
                this.menuSlides.Remove(m);
                return new BaseResponse { Success = true };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new BaseResponse();
            }
        }

        public BaseResponse SaveMenu(Menu menu)
        {
            try
            {
                var existing = this.menus.FirstOrDefault(x => x.Id == menu.Id);

                if (existing != null)
                {
                    this.menus.Remove(existing);
                }

                this.menus.Add(menu);
                return new BaseResponse {Success = true};
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new BaseResponse();
            }
        }

        public List<Menu> GetAllSlideMenus(string slideId)
        {
            try
            {
                return this.menus.FindAll(x => x.SlideId == slideId).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public DeleteResponse DeleteMenu(string menuId)
        {
            var response = new DeleteResponse();
            try
            {
                var m = this.menus.FirstOrDefault(x => x.Id == menuId);
                response.SlideId = m?.SlideId;

                this.menus.Remove(m);
                response.Success = true;
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return response;
            }
        }

        public void SetDateTimeStamp(string slideId, string dateTimeStamp)
        {
            if (!string.IsNullOrEmpty(slideId) && !string.IsNullOrEmpty(dateTimeStamp))
            {
                this.dateTimeStamp[slideId] = dateTimeStamp;
            }
        }

        public string GetDateTimeStamp(string slideId)
        {
            return this.dateTimeStamp.ContainsKey(slideId) ? this.dateTimeStamp[slideId] : null;
        }
    }
}