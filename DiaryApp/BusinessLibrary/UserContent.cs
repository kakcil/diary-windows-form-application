using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary;


namespace BusinessLibrary
{
    public class UserContent
    {
        private UserOperation context = new UserOperation();

        //Returns user model for the entered information
        public User GetUser(string Name, string Password)
        {
            return context.GetUser(Name, Password);
        }

        //Returns the result of adding a user as positive/negative
        public bool AddUser(User user)
        {
            bool result = false;
            User value = context.GetUser(user.Name, user.Password);
            if (value == null)
            {
                result = context.AddUser(user);
            }
            return result;
        }

    }
}
