using System;
using System.Collections.Generic;
using MenuBoards.Interfaces.DataAccess;

namespace MenuBoards.DataAccess
{
    public class TimeStampRepository: ITimeStampRepository
    {
        private readonly Dictionary<string, string> _dateTimeStamp = new Dictionary<string, string>();

        public void UpdateTimeStamp(string slideId)
        {
            this._dateTimeStamp[slideId] = DateTime.Now.Ticks.ToString();
        }

        public string GetTimeStamp(string slideId)
        {
            return this._dateTimeStamp.ContainsKey(slideId) ? this._dateTimeStamp[slideId] : null;
        }
    }
}