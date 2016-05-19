using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
namespace DatabaseMVC
{
    public class Memory
    {
        static StreamReader sr;
        public static void Save(Model m)
        {
            // файл должен выглядеть следующим образом (как CSV таблица)
            // Вопрос;Ответ1#переход1;Ответ2#переход2;Ответ3#переход3...
            // Вопрос;Ответ1#переход1;Ответ2#переход2;Ответ3#переход3...

            //Вообще структура имеет вид конечного детерменированного автомата без циклов
        }

        public static void LoadFile()
        {
            /*
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "KUR|*.kur";
                openFileDialog1.Title = "Select a kur File";

                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    sr = new StreamReader(openFileDialog1.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
            */

            sr = new StreamReader(@"C:\Users\iampi\Documents\test1.kur");
        }

        public static Model.Message GetMessage(int skip)
        {
            string[] str = null;
            Model.Message m = null;
            for (int i = 0; i < skip + 1; i++)
            {
                str = sr.ReadLine().Split(';');//??
            }

            //      Варианты вопросов
            //  1. с вариантами без картинки
            //  2. без вариантов без картинки
            //  3. с вариантами с картинкой
            //  4. без вариантов с картинкой

            if (str[0] == "1")
            {
                string[] anss = str[3].Split('#');
                string[] answers = new string[anss.Length], IsTrue = new string[anss.Length];

                int k = 0;
                for (int i = 0; i < anss.Length; i++)
                {
                    string[] a = anss[i].Split('@');
                    answers[k] = a[0];
                    IsTrue[k] = a[1];
                    k++;
                }

                Model.Message.Answer[] ans = new Model.Message.Answer[k];
                for (int i = 0; i < k; i++)
                {
                    ans[i] = new Model.Message.Answer(answers[i], Convert.ToBoolean(IsTrue[i]));
                }
                return new Model.Message(str[1], str[2], Convert.ToBoolean(str[4]), str[5], Convert.ToBoolean(str[6]), str[7], ans);
            }
            else
            {
                return new Model.Message(str[1], str[2], Convert.ToBoolean(str[4]), str[5], Convert.ToBoolean(str[6]), str[7], str[3]);
            }
        }
    public static bool EOF()
        {
            return sr.EndOfStream;
        }
    }
}

