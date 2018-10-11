using System;
using System.Data;
using System.Linq;
using MenuBoards.Interfaces.DataAccess;
using MenuBoards.Web.ViewModels;
using MySql.Data.MySqlClient;

namespace MenuBoards.DataAccess
{
    public class AccountRepository: BaseRepository, IAccountRepository
    {
        public BaseResponse Register(RegisterViewModel model)
        {
            try
            {
                var query =
                    $"insert into accounts(email, firstname, lastname, password) values('{model.Email}', '{model.FirstName}', '{model.LastName}', '{this.Encript(model.Password)}')";

                var record = this.ExecuteNonQuery(query);

                return record == 1
                    ? new BaseResponse {Success = true}
                    : new BaseResponse {Message = "DB error"};
            }
            catch (Exception ex)
            {
                return new BaseResponse {Message = ex.Message};
            }
        }

        public UserViewModel Authenticate(string username, string password)
        {
            try
            {
                var encryptedPwd = this.Encript(password);
                var query = $"select * from accounts where email='{username}' and password='{encryptedPwd}'";

                using (var connection = new MySqlConnection(this.ConnectionString))
                {
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        connection.Open();

                        var record = cmd.ExecuteReader();

                        if (record.Read())
                        {
                            return new UserViewModel
                            {
                                AccountId = Convert.ToInt32(record["id"].ToString()),
                                Email = record["email"].ToString(),
                                FirstName = record["firstname"].ToString(),
                                LastName = record["lastname"].ToString()
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

        public BaseResponse ChangePassword(string email, string curPassword, string newPasswd)
        {
            var auth = this.Authenticate(email, curPassword);
            if (auth != null)
            {
                try
                {
                    var query =
                        $"update accounts set password='{this.Encript(newPasswd)}' where id='{auth.AccountId}'";

                    var record = this.ExecuteNonQuery(query);

                    return record == 1
                        ? new BaseResponse { Success = true }
                        : new BaseResponse { Message = "DB error" };
                }
                catch (Exception ex)
                {
                    return new BaseResponse { Message = ex.Message };
                }
            }

            return new BaseResponse();
        }

        private string Encript(string password)
        {
            var toCahr = password.ToCharArray();
            Array.Reverse(toCahr);
            return new string(toCahr);
        }
    }
}