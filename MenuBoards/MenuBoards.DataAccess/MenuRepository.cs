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
                if (string.IsNullOrEmpty(menu.Id))
                {
                    menu.Id = Guid.NewGuid().ToString().Replace("-", "");
                    menu.Position = this.menus.FindAll(x => x.SlideId == menu.SlideId).Count;
                    this.menus.Add(menu);
                }
                else
                {
                    var existing = this.menus.FirstOrDefault(x => x.Id == menu.Id);
                    if (existing != null)
                    {
                        existing.MainMenuHeading = menu.MainMenuHeading;
                        existing.MenuItems = menu.MenuItems;
                        existing.Position = menu.Position;
                    }
                }
                
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

        public BaseResponse MoveMenu(string id, string slideId, MoveDirection direction)
        {
            var slideMenus = this.menus.FindAll(x => x.SlideId == slideId);
            var curItem = slideMenus.FirstOrDefault(x => x.Id == id);
            if (curItem != null)
            {
                switch (direction)
                {
                    case MoveDirection.Top:
                        if (curItem.Position > 0)
                        {
                            curItem.Position = 0;
                            foreach (var menu in slideMenus.Where(x => x.Id != curItem.Id))
                            {
                                menu.Position += 1;
                            }
                        }
                        break;
                    case MoveDirection.Bottom:
                        var lastPositions = slideMenus.Count - 1;
                        if (curItem.Position != lastPositions)
                        {
                            curItem.Position = lastPositions;
                            foreach (var menu in slideMenus.Where(x => x.Id != curItem.Id))
                            {
                                menu.Position -= 1;
                            }
                        }
                        break;
                    case MoveDirection.Up:
                        curItem.Position -= 1;
                        foreach (var menu in slideMenus.Where(x => x.Id != curItem.Id && x.Position >= curItem.Position))
                        {
                            menu.Position += 1;
                        }
                        break;
                    case MoveDirection.Down:
                        curItem.Position += 1;
                        foreach (var menu in slideMenus.Where(x => x.Id != curItem.Id && x.Position <= curItem.Position))
                        {
                            menu.Position -= 1;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
                }

                this._timeStampRepository.UpdateTimeStamp(slideId);

                return new BaseResponse { Success = true};
            }

            return new BaseResponse($"No such menu with id={id}, slideid={slideId}");
        }
    }
}