using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{

    //USER MODEL

    public class User
    {
        int _id;
        string _name;
        string _password;
        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
            }
        }

        public User()
        {

        }

        public User(int id, string name, string password)
        {
            this._id = id;
            this._name = name;
            this._password = password;
        }
    }
}
