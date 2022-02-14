using System.ComponentModel.DataAnnotations.Schema;

namespace hafta1WebApi
{
    public class User
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Role { get; set; }
        public string RefreshToken { get; set; }
      
    }
}
