using DataLibrary;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CommonLibrary
{
    public class UserOperation
    {
        public User GetUser(string name, string password)
        {
            List<string[]> records = Database.ReadAllFromCsv(Database.GetUsersCsvPath());
            
            foreach (string[] record in records)
            {
                if (record.Length >= 3 && record[1] == name && record[2] == password)
                {
                    User user = new User
                    {
                        Id = int.Parse(record[0]),
                        Name = record[1],
                        Password = record[2]
                    };
                    return user;
                }
            }
            
            return null;
        }

        public bool AddUser(User user)
        {
            // Check if user already exists
            if (GetUser(user.Name, user.Password) != null)
            {
                return false;
            }
            
            // Generate new ID
            int newId = Database.GetNextId(Database.GetUsersCsvPath());
            user.Id = newId;
            
            string[] values = new string[]
            {
                newId.ToString(),
                user.Name,
                user.Password
            };
            
            bool result = Database.AppendToCsv(Database.GetUsersCsvPath(), values);
            
            if (result)
            {
                MessageBox.Show("Successfully Registered! Please Login!", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            
            return result;
        }
    }
}
