﻿using MenuBoards.Web.ViewModels;

namespace MenuBoards.Interfaces.Web
{
    public interface IAccountService
    {
        BaseResponse Register(RegisterViewModel model);

        BaseResponse ChangePassword(string email, string curPassword, string newPasswd);
    }
}