using System;
using System.Collections.Generic;
using ConsoleApplication1.Authentication;

namespace CTest.Authentication
{
    public class UserContMockSecond : IUserContext
    {

        private static readonly List<User> UserList = new List<User>();
        
        public int UserAmount()
        {
            return UserList.Count;
        }

        public List<User> FindAll()
        {
            return UserList;
        }
        
        public User GetUser(int i)
        {
            return UserList[i];
        }

        public User FindByEmail(string email)
        {
            return UserList.Find(n => n.Email == email);
        }
        
        public Boolean ExistsByEmail(string email)
        {
            return UserList.Exists(n => n.Email == email);
        }


        public User NewUser(string password, string name, string email)
        {
            
            var token = new VerificationToken { Token = Guid.NewGuid().ToString(), ExpDate = DateTime.Now.AddDays(-1)};
            
            var newUser = new User{
                Password = password, 
                Username = name, 
                Email = email};

            newUser.Token = token;
            
            UserList.Add(newUser);

            return newUser;
        }
    }
}