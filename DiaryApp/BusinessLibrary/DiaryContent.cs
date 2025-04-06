using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary;

namespace BusinessLibrary
{
    public class DiaryContent
    {
        //creating a reference point object
        private DiaryOperation context = new DiaryOperation();

        //Lists the diaries written by the user
        public List<Diary> GetDiaries(User user)
        {
            //Returns the user model
            return context.GetDiaries(user);
        }

        //Returns the result of adding a diary as positive/negative
        public bool AddDiary(Diary diary)
        {
            return context.AddDiary(diary);
        }

        //Returns the result of deleting a diary as positive/negative
        public bool DeleteDiary(Diary diary)
        {
            return context.DeleteDiary(diary);
        }

        //Returns the result of updating a diary as positive/negative
        public bool UpdateDiary(Diary diary)
        {
            return context.UpdateDiary(diary);
        }
    }
}
