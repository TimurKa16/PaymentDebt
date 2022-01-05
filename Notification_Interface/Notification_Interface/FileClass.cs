using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Notification_Interface
{
    public static class FileClass
    { 
        public static void SaveNotes(List<Note> notes, string path)
        {
            string text = "";
            for (int i = 0; i < notes.Count; i++)
            {
                text += i.ToString() + "\t" + notes[i].mail.ToString() + "\t" +
                    notes[i].name.ToString() + "\t" + notes[i].dealNumber.ToString() + "\t" +
                    notes[i].dealDate.ToString() + "\t" + notes[i].dueDate.ToString() + "\t" +
                    notes[i].time1.ToString() + "\t" + notes[i].time2.ToString() + "\t" +
                    notes[i].time3.ToString() + "\t" + notes[i].time4.ToString() + "\r\n";
            }
            StreamWriter file = new StreamWriter(path);
            file.Write(text);
            file.Close();
        }

        public static void ReadNotes(ref List<Note> notes, string path)
        {
            notes = new List<Note>();

            string[] text = File.ReadAllLines(path, Encoding.UTF8);

            for (int i = 0; i < text.Length; i++)
            {
                notes.Add(new Note());

                string[] buf = text[i].Split('\t');
                notes[i].id = Convert.ToInt32(buf[0]);
                notes[i].mail = buf[1];
                notes[i].name = buf[2];
                notes[i].dealNumber = buf[3];
                notes[i].dealDate = buf[4];
                notes[i].dueDate = buf[5];
                notes[i].time1 = buf[6];
                notes[i].time2 = buf[7];
                notes[i].time3 = buf[8];
                notes[i].time4 = buf[9];
            }

        }
    }
}

