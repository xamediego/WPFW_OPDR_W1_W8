using System;
using System.Collections.Generic;

namespace ConsoleApplication1.Authentication
{
    public interface IUserContext
    {
        int UserAmount();

        List<User> FindAll();

        User GetUser(int i);
        User FindByEmail(string email);

        Boolean ExistsByEmail(string email);

        User NewUser(string password, string name, string email);
        
    }
}