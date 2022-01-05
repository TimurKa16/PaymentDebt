using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Payment_Notification
{


    public partial class Main_Menu : Form
    {

        DateTime morningTime;
        DateTime eveningTime;

        int morning = 11;
        int evening = 15;

        int noteCounter = 0;

        Mail mail;

        string login = "";
        string myName = "";
        string password = "";


        List<Note> allNotes;
        List<Note> selectedNotes;

        string path = "Notes.txt";
        string pathToHtml = "messageFile.html";

        MessageClass messageClass;
        private void Main_Menu_Load(object sender, EventArgs e)
        {

            try
            {
                CleanUpNotes();

                SelectNotes();
                UpdateLabel();
                MessageBox.Show(label1.Text);

                if (selectedNotes.Count != 0)
                {
                    InitializeMail();
                    InitializeTime();
                    UpdateLabel();
                    if (noteCounter == 0)
                    {
                        Close();
                        Application.Exit();
                    }


                    if ((morningTime - DateTime.Now).TotalSeconds > 30)
                    {
                        Thread.Sleep(Convert.ToInt32((morningTime - DateTime.Now).TotalMilliseconds));
                        morningMail();

                    }
                    else if ((DateTime.Now - morningTime).TotalMinutes < 60)
                    {
                        morningMail();
                    }

                    UpdateLabel();
                    UpdateFile();

                    if ((eveningTime - DateTime.Now).TotalSeconds > 30)
                    {
                        Thread.Sleep(Convert.ToInt32((eveningTime - DateTime.Now).TotalMilliseconds));
                        eveningMail();
                    }
                    else if ((DateTime.Now - eveningTime).TotalMinutes < 60)
                    {
                        eveningMail();
                    }
                    UpdateFile();
                    Close();
                    Application.Exit();
                }
                else
                {
                    UpdateFile();
                    Close();
                    Application.Exit();
                }


            }
            catch (Exception)
            {
                MessageBox.Show("Катастрофа\n\rПопробуйте ещё раз");
                Close();
                Application.Exit();
            }

        }


        public Main_Menu()
        {
            InitializeComponent();
        }

        public void InitializeMail()
        {
            mail = new Mail();
            mail.login = login;
            mail.myName = myName;
            mail.password = password;
            messageClass = new MessageClass();
        }

        private void SelectNotes()
        {
            FileClass.ReadNotes(ref allNotes, path);
            CheckNotes();
        }

        public void CheckNotes()
        {
            selectedNotes = new List<Note>();
            DateTime date;
            try
            {
                for (int i = 0; i < allNotes.Count; i++)
                {
                    string[] buf = allNotes[i].dueDate.Split('.');
                    date = new DateTime(Convert.ToInt32(buf[2]), Convert.ToInt32(buf[1]), Convert.ToInt32(buf[0]));
                    int dateDifference = Convert.ToInt32(((date - DateTime.Today).TotalDays));

                    if (dateDifference <= 1 && dateDifference >= 0)
                        selectedNotes.Add(allNotes[i]);
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Ошибка в записях!\r\nПроверьте запись");
            }
        }

        private void InitializeTime()
        {
            morningTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month,
                DateTime.Today.Day, morning, 0, 0);
            eveningTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month,
                DateTime.Today.Day, evening, 0, 0);
        }

        private bool IsToday(string date)
        {
            DateTime dateTime = new DateTime();
            
            string[] buf = date.Split('.');
            dateTime = new DateTime(Convert.ToInt32(buf[2]), Convert.ToInt32(buf[1]), Convert.ToInt32(buf[0]));
            if ((dateTime - DateTime.Today).TotalDays == 0)
                return true;
            else
                return false;
        }

        private bool IsTomorrow(string date)
        {
            DateTime dateTime = new DateTime();

            string[] buf = date.Split('.');
            dateTime = new DateTime(Convert.ToInt32(buf[2]), Convert.ToInt32(buf[1]), Convert.ToInt32(buf[0]));
            if ((dateTime - DateTime.Today).TotalDays == 1)
                return true;
            else
                return false;
        }

        private bool IsThreeDaysAgo(string date)
        {
            DateTime dateTime = new DateTime();

            string[] buf = date.Split('.');
            dateTime = new DateTime(Convert.ToInt32(buf[2]), Convert.ToInt32(buf[1]), Convert.ToInt32(buf[0]));
            if ((DateTime.Today - dateTime).TotalDays >= 3)
                return true;
            else
                return false;
        }


        private void morningMail()
        {
            SelectNotes();
            for (int i = 0; i < selectedNotes.Count; i++)
            {
                if(IsToday(selectedNotes[i].dueDate) && Convert.ToInt32(selectedNotes[i].time3) == 0)
                {
                    mail.SendMessage(selectedNotes[i].mail, selectedNotes[i].name,
                        messageClass.ReadMessage(pathToHtml, "сегодня наступил",
                        selectedNotes[i].dealNumber, selectedNotes[i].dealDate));

                    selectedNotes[i].time3 = 1.ToString();

                    MessageBox.Show("Отправлено сообщение\r\nПочта: " + selectedNotes[i].mail +
                        "\r\nНомер заявки: " + selectedNotes[i].dealNumber +
                        "\r\nСрок оплаты: " + selectedNotes[i].dueDate);
                }
                else
                if (IsTomorrow(selectedNotes[i].dueDate) && Convert.ToInt32(selectedNotes[i].time1) == 0)
                {
                    mail.SendMessage(selectedNotes[i].mail, selectedNotes[i].name,
                        messageClass.ReadMessage(pathToHtml, "завтра наступит",
                        selectedNotes[i].dealNumber, selectedNotes[i].dealDate));

                    selectedNotes[i].time1 = 1.ToString();

                    MessageBox.Show("Отправлено сообщение\r\nПочта: " + selectedNotes[i].mail +
                        "\r\nНомер заявки: " + selectedNotes[i].dealNumber +
                        "\r\nСрок оплаты: " + selectedNotes[i].dueDate);
                }
            }
        }

        private void eveningMail()
        {
            SelectNotes();

            for (int i = 0; i < selectedNotes.Count; i++)
            {
                if (IsToday(selectedNotes[i].dueDate) && Convert.ToInt32(selectedNotes[i].time4) == 0)
                {
                    mail.SendMessage(selectedNotes[i].mail, selectedNotes[i].name,
                        messageClass.ReadMessage(pathToHtml, "сегодня наступил",
                        selectedNotes[i].dealNumber, selectedNotes[i].dealDate));

                    selectedNotes[i].time4 = 1.ToString();

                    MessageBox.Show("Отправлено сообщение\r\nПочта: " + selectedNotes[i].mail +
                        "\r\nНомер заявки: " + selectedNotes[i].dealNumber +
                        "\r\nСрок оплаты: " + selectedNotes[i].dueDate);
                }

                if (IsTomorrow(selectedNotes[i].dueDate) && Convert.ToInt32(selectedNotes[i].time2) == 0)
                {
                    mail.SendMessage(selectedNotes[i].mail, selectedNotes[i].name,
                        messageClass.ReadMessage(pathToHtml, "завтра наступит",
                        selectedNotes[i].dealNumber, selectedNotes[i].dealDate));

                    selectedNotes[i].time2 = 1.ToString();

                    MessageBox.Show("Отправлено сообщение\r\nПочта: " + selectedNotes[i].mail +
                        "\r\nНомер заявки: " + selectedNotes[i].dealNumber +
                        "\r\nСрок оплаты: " + selectedNotes[i].dueDate);
                }
            }
        }

        private void CleanUpNotes()
        {
            FileClass.ReadNotes(ref allNotes, path);
            for (int i = 0; i < allNotes.Count; i++)
            {
                if (IsThreeDaysAgo(allNotes[i].dueDate))
                    allNotes.Remove(allNotes[i]);                    
            }
            FileClass.SaveNotes(allNotes, path);
        }

        private void UpdateFile()
        {
            for (int i = 0; i < allNotes.Count; i++)
                for (int j = 0; j < selectedNotes.Count; j++)
                    if (allNotes[i].id == selectedNotes[j].id)
                        allNotes[i] = selectedNotes[j];

            FileClass.SaveNotes(allNotes, path);
        }

        private void UpdateInformation()
        {
            SelectNotes();
        }


        private void UpdateLabel()
        {
            string buf = "";
            noteCounter = 0;


            for (int i = 0; i < selectedNotes.Count; i++)
            {
                if (IsToday(selectedNotes[i].dueDate))
                {
                    if (Convert.ToInt32(selectedNotes[i].time3) == 0 || Convert.ToInt32(selectedNotes[i].time4) == 0)
                    {
                        buf += "\r\n\r\nПочта: " + selectedNotes[i].mail +
                            "\r\nНомер заявки: " + selectedNotes[i].dealNumber +
                            "\r\nСрок оплаты: " + selectedNotes[i].dueDate;
                        noteCounter++;
                    }
                }
                else
                {
                    if (IsTomorrow(selectedNotes[i].dueDate))
                        if (Convert.ToInt32(selectedNotes[i].time1) == 0 || Convert.ToInt32(selectedNotes[i].time2) == 0)
                        {
                            buf += "\r\n\r\nПочта: " + selectedNotes[i].mail +
                                "\r\nНомер заявки: " + selectedNotes[i].dealNumber +
                                "\r\nСрок оплаты: " + selectedNotes[i].dueDate;
                            noteCounter++;
                        }
                }
            }
            if (noteCounter == 0)
                label1.Text = "На сегодня нет заметок";
            else if (noteCounter == 1)
                label1.Text = "Ожидает отправки " + selectedNotes.Count + " сообщение\r\n";
            else if (noteCounter <= 4)
                label1.Text = "Ожидают отправки " + selectedNotes.Count + " сообщения\r\n";
            else if (noteCounter > 4)
                label1.Text = "Ожидает отправки " + selectedNotes.Count + " сообщений\r\n";

            label1.Text += buf;
        }


        const string applicationName= "Payment_Notification";
        public bool SetAutorunValue(bool autorun)
    {
        string ExePath = Application.ExecutablePath;
        RegistryKey reg;
        reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
        try
        {
            if (autorun)
                reg.SetValue(applicationName, ExePath);
            else
                reg.DeleteValue(applicationName);

            reg.Close();
        }
        catch
        {
            return false;
        }
        return true;
    }



        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void Main_Menu_Resize(object sender, EventArgs e)
        {
            
        }

        private void Main_Menu_Shown(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
