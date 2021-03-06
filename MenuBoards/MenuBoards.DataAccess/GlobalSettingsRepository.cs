﻿using System;
using System.Collections.Generic;
using MenuBoards.Interfaces.DataAccess;
using MenuBoards.Web.ViewModels.Displays;

namespace MenuBoards.DataAccess
{
    public class GlobalSettingsRepository: IGlobalSettingsRepository
    {
        private readonly Dictionary<string, string> displayCodes = new Dictionary<string, string>(); //account.code
        

        public DisplayCodeResponse VerifyDisplayCode(DisplayCode code)
        {
            var response = new DisplayCodeResponse();
            if (!this.displayCodes.ContainsKey(code.Account) || this.displayCodes[code.Account] != code.Code)
            {
                response.Message = "Code / Account doesn't exist";
                return response;
            }

            response.Success = true;
            return response;
        }

        public void CreateDisplayCode(string account)
        {
            var newCode = GenerateNewCode();
            if (!this.displayCodes.ContainsKey(account))
            {
                this.displayCodes.Add(account, newCode);
            }
            else
            {
                this.displayCodes[account] = newCode;
            }
        }

        public string GetDisplayCode(string account)
        {
            return this.displayCodes.ContainsKey(account)
                ? this.displayCodes[account]
                : null;
        }

        private string GenerateNewCode()
        {
            var newCode = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8);
            return newCode;
        }
    }
}