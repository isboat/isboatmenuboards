using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MenuBoards.Interfaces.DataAccess;
using MenuBoards.Web.ViewModels;
using MySql.Data.MySqlClient;

namespace MenuBoards.DataAccess
{
    public class MenuRepository: BaseRepository, IMenuRepository
    {
        private readonly ITimeStampRepository _timeStampRepository;
        
        public MenuRepository(ITimeStampRepository timeStampRepository)
        {
            _timeStampRepository = timeStampRepository;
        }

        public List<Menu> GetMenus(string slideId)
        {
            var menus = new List<Menu>();
            try
            {
                var query = $"select * from menus where slideid='{slideId}'";

                using (var connection = new MySqlConnection(this.ConnectionString))
                {
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        connection.Open();

                        var record = cmd.ExecuteReader();

                        while (record.Read())
                        {
                            menus.Add(
                                new Menu
                                {
                                    SlideId = record["slideid"].ToString(),
                                    MainMenuHeading = record["heading"].ToString(),
                                    Position = Convert.ToInt32(record["position"].ToString()),
                                    Id = record["id"].ToString()
                                });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                //throw;
            }

            return menus;
        }

        public BaseResponse SaveMenu(Menu menu)
        {
            var baseResponse = new BaseResponse();
            try
            {
                string query;

                // New menu item
                if (string.IsNullOrEmpty(menu.Id))
                {
                    menu.Id = this.GenerateId();
                    menu.Position = this.GetMenus(menu.SlideId).Count;

                    query =
                        $"insert into menus(id, slideid, position, heading) values('{menu.Id}', '{menu.SlideId}','{menu.Position}','{menu.MainMenuHeading}')";
                }
                else
                {
                    query =
                        $"update menus set position='{menu.Position}', heading='{menu.MainMenuHeading}' where id='{menu.Id}'";
                }

                var record = this.ExecuteNonQuery(query);

                if (record == 1)
                {
                    this._timeStampRepository.UpdateTimeStamp(menu.SlideId);
                    baseResponse.Success = true;
                }

                return baseResponse;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return baseResponse;
            }
        }

        public DeleteResponse DeleteMenu(DeleteItem item)
        {
            var response = new DeleteResponse { ItemId = item.SlideId};


            try
            {
                var query = $"delete from menus where id='{item.Id}'";

                var record = this.ExecuteNonQuery(query);

                if (record == 1)
                {
                    this._timeStampRepository.UpdateTimeStamp(item.SlideId);
                    response.Success = true;
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                //throw;
            }

            return response;
        }

        public Menu GetMenu(string id)
        {
            try
            {
                var query = $"select * from menus where id='{id}'";

                using (var connection = new MySqlConnection(this.ConnectionString))
                {
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        connection.Open();

                        var record = cmd.ExecuteReader();

                        if(record.Read())
                        {
                            return new Menu
                            {
                                SlideId = record["slideid"].ToString(),
                                MainMenuHeading = record["heading"].ToString(),
                                Position = Convert.ToInt32(record["position"].ToString()),
                                Id = record["id"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                //throw;
            }

            return null;
        }

        public BaseResponse MoveMenu(string id, string slideId, MoveDirection direction)
        {
            var slideMenus = this.GetMenus(slideId);
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
                
                slideMenus.ForEach(m =>
                {
                    this.SaveMenu(m);
                });

                this._timeStampRepository.UpdateTimeStamp(slideId);

                return new BaseResponse { Success = true};
            }

            return new BaseResponse($"No such menu with id={id}, slideid={slideId}");
        }
    }
}