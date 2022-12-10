using System.Collections.Generic;
using ConsoleApplication1.Authentication;

namespace CTest.Authentication
{
    public class UserContextMock : IUserContext
    {

        public List<User> users = new List<User>();

        public int UserAmount()
        {
            return users.Count;
        }

        public List<User> FindAll()
        {
            return users;
        }

        public User GetUser(int i)
        {
            return users[i];
        }

        public User FindByEmail(string email)
        {
            return users.Find(n => n.Email == email);
        }

        public bool ExistsByEmail(string email)
        {
            return users.Exists(n => n.Email == email);
        }

        public User NewUser(string password, string name, string email)
        {
            User newUser = new User{Password = password, Username = name, Email = email};
            
            users.Add(newUser);

            return newUser;
        }
        
    }
}