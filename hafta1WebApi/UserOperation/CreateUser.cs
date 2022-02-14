using hafta1WebApi.DBOperations;
using System;
using System.Linq;

namespace hafta1WebApi.UserOperation
{
    public class CreateUser
    {
        public CreateUserModel Model { get; set; }
        private readonly UserDbContext _dbContext;

        public CreateUser(UserDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        
        public void Handle()
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Email == Model.Email);

            if (user is not null)
            {
                throw new InvalidOperationException("User already in Records");

            }
            user = new User();

            user.Email = Model.Email;
            user.Password = Model.Password.Encryptor(); //Password şifrelenmiştir.

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            

        }

        public class CreateUserModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
