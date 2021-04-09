using System.Collections.Generic;

namespace BLL.DTO
{
	public class UserDTO
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public int Rating { get; set; }

        public int Money { get; set; }

        public virtual ICollection<UserВЕЩ> Friends { get; set; }
    }
}
