using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public class Diary
    {
        //DIARY MODEL

        int _id;
        User _user;
        string _blog;
        DateTime _date;

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

        public User User
        {
            get
            {
                return _user;
            }

            set
            {
                _user = value;
            }
        }

        public string Blog
        {
            get
            {
                return _blog;
            }

            set
            {
                _blog = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return _date;
            }

            set
            {
                _date = value;
            }
        }

        public Diary()
        {

        }

        public Diary(int id, User user, string blog, DateTime date)
        {
            this._id = id;
            this._user = user;
            this._blog = blog;
            this._date = date;
        }
    }
}
