using System;
using System.Collections.Generic;
using System.Linq;
using MenuBoards.Interfaces.DataAccess;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.DataAccess
{
    public class MenuRepository: TimeStampRepository, IMenuRepository
    {
        private readonly ITimeStampRepository _timeStampRepository;

        private List<Menu> menus = new List<Menu>();

        public MenuRepository(ITimeStampRepository timeStampRepository)
        {
            _timeStampRepository = timeStampRepository;
        }

        public List<Menu> GetMenus(string slideId)
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

                this._timeStampRepository.UpdateTimeStamp(menu.SlideId);

                return new BaseResponse { Success = true };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new BaseResponse();
            }
        }

        public DeleteResponse DeleteMenu(string menuId)
        {
            var response = new DeleteResponse();
            try
            {
                var m = this.menus.FirstOrDefault(x => x.Id == menuId);
                if (m == null)
                {
                    response.Message = "Menu not found";
                    return response;
                }

                response.ItemId = m.SlideId;

                this.menus.Remove(m);
                response.Success = true;

                this._timeStampRepository.UpdateTimeStamp(response.ItemId);

                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return response;
            }
        }

        public Menu GetMenu(string id)
        {
            return this.menus.FirstOrDefault(m => m.Id == id);
        }
    }
}