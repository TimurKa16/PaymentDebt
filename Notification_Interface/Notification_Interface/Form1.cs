using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Notification_Interface
{
    public partial class Form1 : Form
    {
        //string path = "Z:\\КамРус\\Programs\\Напоминалка\\Notes.txt";
        string path = "Notes.txt";

        const int CHANGE_MODE = 1;
        const int CREATE_MODE = 0;
        List<Note> notes;
        
        public bool yes = false;
        public int noteId { set; get; }
        List<string> lbList;

        ChangeNoteForm changeNoteForm;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadLbList();
            FileClass.ReadNotes(ref notes, path);
            Draw();
        }

        private void LoadLbList()
        {
            lbList = new List<string> {"Id", "Почта", "Имя", "Номер счета", "Дата перевозки", "Срок оплаты",
                "За день утром", "За день вечером", "В день утром", "В день вечером" };
        }

        Label[] lbHorizontal;
        Button[][] button;

        private void Create_Labels()
        {
            lbHorizontal = new Label[lbList.Count - 1];
            int a = 15, b = 50;
            for (int i = 0; i < lbList.Count - 1; i++)
            {
                lbHorizontal[i] = new Label();
                lbHorizontal[i].Name = "lb" + i.ToString();
                lbHorizontal[i].Parent = this;
                lbHorizontal[i].BackColor = Color.DeepSkyBlue;
                lbHorizontal[i].Left = a;
                lbHorizontal[i].Top = b;
                lbHorizontal[i].Size = new Size(80, 50);
                lbHorizontal[i].Text = lbList[i + 1];
                lbHorizontal[i].ForeColor = Color.Black;
                lbHorizontal[i].Font = new Font(lbHorizontal[i].Font, FontStyle.Bold);
                lbHorizontal[i].Font = new Font(lbHorizontal[i].Font.Name, 9, lbHorizontal[i].Font.Style);
                lbHorizontal[i].TextAlign = ContentAlignment.MiddleCenter;
                lbHorizontal[i].BorderStyle = BorderStyle.FixedSingle;
                lbHorizontal[i].BringToFront();

                if(i < 3)
                {
                    lbHorizontal[i].Size = new Size(225, 50);
                    a += 145;
                }

                a += 79;
            }
            this.ActiveControl = lbHorizontal[0];
        }

        private void Create_buttons()
        {
            button = new Button[notes.Count][];
            for (int i = 0; i < notes.Count; i++)
                button[i] = new Button[lbList.Count - 1];
            int a = 15, b = 99, k = 0;
            for (int i = 0; i < notes.Count; i++)
            {
                for (int j = 0; j < lbList.Count - 1; j++)
                {
                    button[i][j] = new Button();
                    button[i][j].Name = i.ToString() + j.ToString();
                    button[i][j].Parent = this;
                    button[i][j].Left = a;
                    button[i][j].Top = b;
                    button[i][j].Size = new Size(80, 25);
                    button[i][j].ForeColor = Color.Black;
                    button[i][j].BackColor = Color.White;
                    button[i][j].FlatStyle = FlatStyle.Flat;
                    button[i][j].FlatAppearance.BorderSize = 1;
                    button[i][j].MouseEnter += new EventHandler(ButtonMouseEnter);
                    button[i][j].MouseLeave += new EventHandler(ButtonMouseLeave);
                    button[i][j].MouseClick += new MouseEventHandler(ButtonMouseClick);
                    button[i][j].ContextMenuStrip = contextMenuStrip2;
                    button[i][j].BringToFront();

                    if (j < 3)
                    {
                        button[i][j].Size = new Size(225, 25);
                        a += 145;
                    }

                    a += 79;
                    k++;
                }
                a = 15;
                b += 24;
            }

        }

        private void Deltb()
        {
            if (button != null)
                for (int i = 0; i < button.Length; i++)
                    for (int j = 0; j < button[i].Length; j++)
                        button[i][j].Dispose();
        }

        private void Dellb()
        {
            if (lbHorizontal != null)
                for (int i = 0; i < lbHorizontal.Length; i++)
                    lbHorizontal[i].Dispose();
        }

        private void Draw()
        {
            Deltb();
            Dellb();
            Create_Labels();
            Create_buttons();

            for (int i = 0; i < notes.Count; i++)
            {
                button[i][0].Text = notes[i].mail;
                button[i][1].Text = notes[i].name;
                button[i][2].Text = notes[i].dealNumber;
                button[i][3].Text = notes[i].dealDate;
                button[i][4].Text = notes[i].dueDate;
                button[i][5].Text = notes[i].time1;
                button[i][6].Text = notes[i].time2;
                button[i][7].Text = notes[i].time3;
                button[i][8].Text = notes[i].time4;

                //for (int j = 0; j < projectList[0].Length - 1; j++)
                //    button[i][j].Text = projectList[i][j + 1];
            }
        }

        private void ButtonMouseEnter(object sender, EventArgs e)
        {

            Button bt = sender as Button;
            int row = Convert.ToInt32(bt.Name[0].ToString());
            for (int i = 0; i < button[row].Length; i++)
                button[row][i].BackColor = Color.LightSkyBlue;
            noteId = row;
        }

        private void ButtonMouseLeave(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            int row = Convert.ToInt32(bt.Name[0].ToString());
            for (int i = 0; i < button[row].Length; i++)
                button[row][i].BackColor = Color.White;
        }

        private void ButtonMouseClick(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            int row = Convert.ToInt32(bt.Name[0].ToString());
            ChangeNote();
            yes = true;
        }




        private void ReloadForm()
        {
            FileClass.ReadNotes(ref notes, path);
            Draw();
        }


        private void CreateNote()
        {
            changeNoteForm = new ChangeNoteForm(notes, noteId, lbList, CREATE_MODE, path);
            Hide();
            changeNoteForm.ShowDialog();
            Show();
            ReloadForm();
        }

        private void ChangeNote()
        {
            changeNoteForm = new ChangeNoteForm(notes, noteId, lbList, CHANGE_MODE, path);
            Hide();
            changeNoteForm.ShowDialog();
            Show();
            ReloadForm();
        }


        private void создатьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateNote();

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateNote();
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNote();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

            ChangeNote();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DeleteNote();            
        }

        private void DeleteNote()
        {
            notes.Remove(notes[noteId]);
            FileClass.SaveNotes(notes, path);
            ReloadForm();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReloadForm();
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void обновитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ReloadForm();
        }

        private void обновитьToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ReloadForm();
        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
