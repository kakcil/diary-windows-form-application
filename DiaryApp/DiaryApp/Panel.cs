using BusinessLibrary;
using CommonLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiaryApp
{
    public partial class Panel : Form
    {
        public Panel()
        {
            InitializeComponent();
        }


        User user;
        Diary diary;
        List<Diary> diaries;
        DiaryContent context;

        
        private void button5_Click(object sender, EventArgs e)
        {
            // Clear 
            dateTimePicker1.Value = DateTime.Now;
            textBox1.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Updates the diary in the data layer
            diary.Blog = textBox1.Text;
            diary.Date = dateTimePicker1.Value;
            context.UpdateDiary(diary);
            //Refresh table
            Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Delete diary record
            context.DeleteDiary(diary);
            //Refresh table
            Refresh();
        }

        private void Panel_Load(object sender, EventArgs e)
        {
            // Calls the user login screen
            LoginForm lgForm = new LoginForm();
            lgForm.ShowDialog();

            user = lgForm.user;

            if (user != null)
            {
                context = new DiaryContent();
                //Refresh table
                Refresh();
            }
        }

        private void Refresh()
        {
            //Ensures synchronization between presentation layer and business layer after operations
            diaries = context.GetDiaries(user);
            //Data is assigned to the table
            dataGridView1.DataSource = diaries;
            //Unnecessary columns are deleted
            dataGridView1.Columns.RemoveAt(2);

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Makes a new entry to the diary
            diary = new Diary();
            diary.Blog = textBox1.Text;
            diary.User = user;
            diary.Date = dateTimePicker1.Value;
            context.AddDiary(diary);
            //Refresh table
            Refresh();
        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            // Updates the diary when a selection is made from the list
            if (dataGridView1.SelectedRows.Count != 0)
            {
                diary = diaries[dataGridView1.SelectedRows[0].Index];
                textBox1.Text = diary.Blog;
                dateTimePicker1.Value = diary.Date;
            }
        }

    }
}
