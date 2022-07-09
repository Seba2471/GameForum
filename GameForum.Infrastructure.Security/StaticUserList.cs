using GameForum.Infrastructure.Security.Models;

namespace GameForum.Infrastructure.Security
{
    public class StaticUserList
    {
        public static List<MyUser> Users()
        {
            List<MyUser> list = new List<MyUser>();

            MyUser u = new MyUser()
            {
                Email = "stefan@gmail.com",
                FirstName = "Stefan",
                LastName = "Karolos",
                Id = "qwertyuiop",
                UserName = "stefos"
            };

            MyUser u2 = new MyUser()
            {
                Email = "eve@gmail.com",
                FirstName = "Ewa",
                LastName = "Zakurka",
                Id = "asdfghjkl",
                UserName = "eve"
            };

            list.Add(u); list.Add(u2);

            return list;
        }
    }
}
