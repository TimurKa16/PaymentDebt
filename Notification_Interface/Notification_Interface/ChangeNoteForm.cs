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
    public partial class ChangeNoteForm : Form
    {
        private int noteId;
        private List<Note> notes;
        List<string> lbList;
        public int changeMode;
        public string path;

        public ChangeNoteForm(List<Note> notes, int noteId, List<string> lbList, int changeMode, string path)
        {
            InitializeComponent();
            this.lbList = lbList;
            this.changeMode = changeMode;
            this.path = path;
            this.noteId = noteId;
            this.notes = notes;

        }

        private void ChangeNote_Load(object sender, EventArgs e)
        {
            if (changeMode == 0)
                CreateNote();
            else
                ChangeNote();
        }

        public void CreateNote()
        {
            Draw(0);
            //for (int i = 0; i < button.Length; i++)
            //    if(button)
            //DBClass.CreateNote(ref note);
        }

        public void ChangeNote()
        {
            Draw(1);
        }











        Label[] lbHorizontal;
        TextBox[][] button;

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

                if (i < 3)
                {
                    lbHorizontal[i].Size = new Size(255, 50);
                    a += 145;
                }

                a += 79;
            }
            this.ActiveControl = lbHorizontal[0];
        }

        private void Create_buttons()
        {
            button = new TextBox[1][];
            for (int i = 0; i < 1; i++)
                button[i] = new TextBox[lbList.Count - 1];
            int a = 15, b = 99, k = 0;
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < lbList.Count - 1; j++)
                {
                    button[i][j] = new TextBox();
                    button[i][j].Name = i.ToString() + j.ToString();
                    button[i][j].Parent = this;
                    button[i][j].Multiline = true;
                    button[i][j].Left = a;
                    button[i][j].Top = b;
                    button[i][j].Size = new Size(80, 25);
                    button[i][j].ForeColor = Color.Black;
                    button[i][j].BackColor = Color.White;
                    button[i][j].TextAlign = HorizontalAlignment.Center;
                    button[i][j].BorderStyle = BorderStyle.FixedSingle;
                    this.button[i][j].KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownHandler);
                    button[i][j].BringToFront();

                    if (j < 3)
                    {
                        button[i][j].Size = new Size(255, 25);
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

        private void Draw(int draw)
        {
            Deltb();
            Dellb();
            Create_Labels();
            Create_buttons();

            if (draw == 1)
            {

                button[0][0].Text = notes[noteId].mail;
                button[0][1].Text = notes[noteId].name;
                button[0][2].Text = notes[noteId].dealNumber;
                button[0][3].Text = notes[noteId].dealDate;
                button[0][4].Text = notes[noteId].dueDate;
                button[0][5].Text = notes[noteId].time1;
                button[0][6].Text = notes[noteId].time2;
                button[0][7].Text = notes[noteId].time3;
                button[0][8].Text = notes[noteId].time4;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool isEmpty = false;
            for (int i = 0; i < 9; i++)
                if (button[0][i].Text == "" || button[0][i].Text == null)
                    if (i < 5)
                        isEmpty = true;
                    else
                        button[0][i].Text = 0.ToString();

            if (changeMode == 0)
            {
                if (!isEmpty)
                {
                    noteId = notes.Count;
                    notes.Add(new Note());

                    notes[noteId].id = noteId;
                    notes[noteId].mail = button[0][0].Text;
                    notes[noteId].name = button[0][1].Text;
                    notes[noteId].dealNumber = button[0][2].Text;
                    notes[noteId].dealDate = button[0][3].Text;
                    notes[noteId].dueDate = button[0][4].Text;
                    notes[noteId].time1 = button[0][5].Text;
                    notes[noteId].time2 = button[0][6].Text;
                    notes[noteId].time3 = button[0][7].Text;
                    notes[noteId].time4 = button[0][8].Text;

                    FileClass.SaveNotes(notes, path);
                    //DBClass.CreateNote(note);
                    Close();
                }
                else
                    MessageBox.Show("Заполните поля!");
            }
            else
            {
                if (!isEmpty)
                {
                    notes[noteId].mail = button[0][0].Text;
                    notes[noteId].name = button[0][1].Text;
                    notes[noteId].dealNumber = button[0][2].Text;
                    notes[noteId].dealDate = button[0][3].Text;
                    notes[noteId].dueDate = button[0][4].Text;
                    notes[noteId].time1 = button[0][5].Text;
                    notes[noteId].time2 = button[0][6].Text;
                    notes[noteId].time3 = button[0][7].Text;
                    notes[noteId].time4 = button[0][8].Text;

                    FileClass.SaveNotes(notes, path);
                    //DBClass.ChangeNote(note);
                    Close();
                }
                else
                    MessageBox.Show("Заполните поля!");
            }

        }
        private void KeyDownHandler(object sender, KeyEventArgs e)
        {
            TextBox bt = sender as TextBox;
            int row = Convert.ToInt32(bt.Name[0].ToString());
            if (e.KeyCode == Keys.Enter)
            {
                bt.Text = bt.Text.TrimEnd(new char[] { '\n', '\r' });
                button1.PerformClick();
            }
        }
    }
}
