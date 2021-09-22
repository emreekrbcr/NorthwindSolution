using Core.Entities.Abstract;

namespace Core.Entities.Concrete.Dtos
{
    public class UserForLoginDto : IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}