using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MenuBoards.Interfaces.DataAccess;
using MenuBoards.Web.ViewModels;
using MySql.Data.MySqlClient;

namespace MenuBoards.DataAccess
{
    public class SlideRepository: BaseRepository, ISlideRepository
    {
        private readonly ITimeStampRepository _timeStampRepository;

        public SlideRepository(ITimeStampRepository timeStampRepository)
        {
            _timeStampRepository = timeStampRepository;
        }

        // Consider using Unit of work pattern;
        public string CreateMenuSlide(MenuSlide model)
        {
            model.Id = this.GenerateId();

            try
            {
                var query =
                    $"insert into slides(id, name, golivedatetime, type, accountid) values('{model.Id}', '{model.Name}', '{model.GoLiveDateTime}', '{0}', '{model.AccountId}')";

                var record = this.ExecuteNonQuery(query);
                
                if (record == 1)
                {
                    this._timeStampRepository.UpdateTimeStamp(model.Id);
                    return model.Id;
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public MenuSlide GetMenuSlide(string id)
        {
            try
            {
                var query = $"select * from slides where id='{id}'";

                using (var connection = new MySqlConnection(this.ConnectionString))
                {
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        connection.Open();

                        var record = cmd.ExecuteReader();

                        if (record.Read())
                        {
                            return new MenuSlide
                            {
                                AccountId = record["id"].ToString(),
                                Name = record["name"].ToString(),
                                GoLiveDateTime = record["golivedatetime"].ToString(),
                                SlideType = SlideType.Menu,
                                Id = record["id"].ToString()
                            };
                        }

                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // Consider using Unit of work pattern;
        public List<MenuSlide> GetAllMenuSlides(string accountId)
        {
            var slides = new List<MenuSlide>();
            try
            {
                var query = $"select * from slides where accountid='{accountId}'";

                using (var connection = new MySqlConnection(this.ConnectionString))
                {
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        connection.Open();

                        var record = cmd.ExecuteReader();

                        while (record.Read())
                        {
                            slides.Add(
                                new MenuSlide
                                {
                                    AccountId = record["id"].ToString(),
                                    Name = record["name"].ToString(),
                                    GoLiveDateTime = record["golivedatetime"].ToString(),
                                    SlideType = SlideType.Menu,
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

            return slides;
        }

        public List<MenuSlide> GetAccountVisibleSlides(string account)
        {
            return this.GetAllMenuSlides(account);
        }

        // Consider using Unit of work pattern;
        public BaseResponse DeleteSlide(string slideId)
        {
            var baseResponse = new BaseResponse();

            try
            {
                var query = $"delete from slides where id='{slideId}'";

                var record = this.ExecuteNonQuery(query);

                if (record == 1)
                {
                    this._timeStampRepository.UpdateTimeStamp(slideId);
                    baseResponse.Success = true;
                }
            }
            catch (Exception ex)
            {
                //return null;
            }

            return baseResponse;
        }
    }
}