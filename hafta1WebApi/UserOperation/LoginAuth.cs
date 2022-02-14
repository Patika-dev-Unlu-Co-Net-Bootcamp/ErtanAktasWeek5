using hafta1WebApi.DBOperations;
using hafta1WebApi.TokenOperations;
using hafta1WebApi.TokenOperations.Models;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace hafta1WebApi.UserOperation
{
    public class LoginAuth
    {   
        public LoginUserModel Model { get; set; }
        private readonly UserDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public LoginAuth(UserDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;

        }
        //Modelden gelen veriler ile db kayıtları eşlenip sonuç geri dödürülmüştür.
        public string Handle()
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.Email == Model.Email && x.Password == Model.Password.Encryptor());
            if(user == null)
            {
                return "Kullanıcı Bulunamamıştır.";
            }
            else
            {

                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;

                _dbContext.SaveChanges();

                return token.AccessToken;
                
                
            }
            
           
        }
        //Kullanıcıdan gerekli alanları almak için böyle bir Model tasarlanmıştır.

        public class LoginUserModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
