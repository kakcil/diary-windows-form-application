using System.Collections.Generic;
using DataLibrary;
using System;

namespace CommonLibrary
{
    public class DiaryOperation
    {
        //Get user's diaries with the given model
        public List<Diary> GetDiaries(User user)
        {
            List<Diary> diaries = new List<Diary>();
            List<string[]> records = Database.ReadAllFromCsv(Database.GetDiariesCsvPath());
            
            foreach (string[] record in records)
            {
                if (record.Length >= 4 && int.Parse(record[1]) == user.Id)
                {
                    var diary = new Diary
                    {
                        Id = int.Parse(record[0]),
                        User = user,
                        Blog = record[2],
                        Date = DateTime.Parse(record[3])
                    };
                    diaries.Add(diary);
                }
            }
            
            return diaries;
        }

        //Returns the result of adding a diary with the given model
        public bool AddDiary(Diary diary)
        {
            // Generate new ID
            int newId = Database.GetNextId(Database.GetDiariesCsvPath());
            diary.Id = newId;
            
            string[] values = new string[]
            {
                newId.ToString(),
                diary.User.Id.ToString(),
                diary.Blog,
                diary.Date.ToString()
            };
            
            return Database.AppendToCsv(Database.GetDiariesCsvPath(), values);
        }

        //Returns the result of deleting a diary with the given model
        public bool DeleteDiary(Diary diary)
        {
            return Database.DeleteFromCsv(Database.GetDiariesCsvPath(), 0, diary.Id.ToString());
        }

        //Returns the result of updating a diary with the given model
        public bool UpdateDiary(Diary diary)
        {
            string[] values = new string[]
            {
                diary.Id.ToString(),
                diary.User.Id.ToString(),
                diary.Blog,
                diary.Date.ToString()
            };
            
            return Database.UpdateCsvRecord(Database.GetDiariesCsvPath(), 0, diary.Id.ToString(), values);
        }
    }
}
