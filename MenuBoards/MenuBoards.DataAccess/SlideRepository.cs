using System;
using System.Collections.Generic;
using System.Linq;
using MenuBoards.Interfaces.DataAccess;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.DataAccess
{
    public class SlideRepository: ISlideRepository
    {
        private readonly ITimeStampRepository _timeStampRepository;

        // Consider using Unit of work pattern;
        private readonly List<MenuSlide> _menuSlides = new List<MenuSlide>();

        public SlideRepository(ITimeStampRepository timeStampRepository)
        {
            _timeStampRepository = timeStampRepository;
        }

        // Consider using Unit of work pattern;
        public string CreateMenuSlide(MenuSlide model)
        {
            model.Id = (this._menuSlides.Count + 1).ToString();
            this._menuSlides.Add(model);

            this._timeStampRepository.UpdateTimeStamp(model.Id);

            return model.Id;
        }

        // Consider using Unit of work pattern;
        public List<MenuSlide> GetAllMenuSlides()
        {
            return this._menuSlides;
        }

        public List<MenuSlide> GetAccountVisibleSlides(string account)
        {
            return this.GetAllMenuSlides();
        }

        // Consider using Unit of work pattern;
        public BaseResponse DeleteSlide(string slideId)
        {
            try
            {
                var m = this._menuSlides.FirstOrDefault(x => x.Id == slideId);
                this._menuSlides.Remove(m);
                this._timeStampRepository.UpdateTimeStamp(slideId);
                return new BaseResponse { Success = true };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new BaseResponse();
            }
        }
    }
}