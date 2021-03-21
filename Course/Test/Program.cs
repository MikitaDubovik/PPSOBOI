using DAL;
using DAL.Interface.DTO;

namespace Test
{
    public class Program
    {
        static void Main(string[] args)
        {
            UserRepository tt = new UserRepository();

            DalUser user = new DalUser
            {
                UserId = 1,
                Email = "123@123.by",
                Login = "log",
                Name = "name",
                Password = "password",
                Phone = "33-333-333",
                ProfilePhoto = new byte[256],
                Roles = "UserId"
            };

            tt.Insert(user);
        }
    }
}
