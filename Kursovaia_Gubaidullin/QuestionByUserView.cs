using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DatabaseMVC
{

    public partial class QuestionByUserView : Form
    {
        bool NEW = true;
        bool CanSimpleViewed = true;

        //TODO СДЕЛАТЬ, наконец
        // быть может стоит вынести сохранение в другой класс. УТОЧНИТЬ 
        // и вообще много чего вынести в контроллер, слишком толстое представление
        public QuestionByUserView()
        {
            InitializeComponent();
        }

        private void QuestionTypeForm_Load(object sender, EventArgs e)
        {
            Clean();
        }
        private void Clean()
        {
            AnswerLabel.Visible = false;
            CorrectAnswerBox.Visible = false;
            AnswersGrid.Visible = false;
            ImageSelectionLabel.Visible = false;
            ImageDirectoryBox.Visible = false;
            ImageSelectionButton.Visible = false;
        }
        public void Prepare(EventHandler Settings_Click, EventHandler Help_Click)
        {
            this.Settings.Click += Settings_Click;
            this.Help.Click += Help_Click;
        }

        private void SimpleView_Click(object sender, EventArgs e)//сделано
        {
            if (NEW)
            {
                SourceBox.Visible = false;
            }
            else
            {
                if (CanSimpleViewed)
                {
                    SourceBox.Visible = false;
                }
                else
                    MessageBox.Show("Данный сценарий не может быть представлен в простом виде");
            }
        }

        private void SourceView_Click(object sender, EventArgs e)//сделано
        {
            SourceBox.Visible = true;
        }

        private void NextQuestion_Click(object sender, EventArgs e)//КАК?
        {
            if (QuestionBox.Text.Length != 0)
            {
                //      Варианты вопросов
                //  1. с вариантами без картинки
                //  2. без вариантов без картинки
                //  3. с вариантами с картинкой
                //  4. без вариантов с картинкой

                if (IsAnswerExistBox.SelectedIndex == 0)
                {
                    if (IsImageExistBox.SelectedIndex == 0)
                        SourceBox.Text += 3;
                    else
                        SourceBox.Text += 1;
                }
                else
                {
                    if (IsImageExistBox.SelectedIndex == 0)
                        SourceBox.Text += 4;
                    else
                        SourceBox.Text += 2;
                }
                SourceBox.Text += ';';
                SourceBox.Text += QuestionBox.Text;
                SourceBox.Text += ';';

                for (int z = 0; z < AnswersGrid.RowCount - 1; z++)
                {
                    if (z!=0)
                        SourceBox.Text += '#';//Переход
                    string q = Convert.ToString(AnswersGrid.Rows[z].Cells[0].Value);
                    string a = Convert.ToString(AnswersGrid.Rows[z].Cells[1].Value);
                    if (q.Length != 0 && a.Length != 0)
                    {
                        SourceBox.Text += q + '@'; //Ответ
                        SourceBox.Text += a; 
                    }
                }
                SourceBox.Text += ';';
                if (ExplanationBox.Text.Length == 0)
                    SourceBox.Text += 0;
                else
                {
                    SourceBox.Text += 1;
                    SourceBox.Text += ';';
                    SourceBox.Text += ExplanationBox.Text;
                }
                SourceBox.Text += ';';

                if (IsAnswerExistBox.SelectedIndex == 0)
                {
                    SourceBox.Text += 1;
                    SourceBox.Text += ImageDirectoryBox.Text;
                    
                }
                else
                    SourceBox.Text += 0;
                SourceBox.Text += ';';
                SourceBox.Text += '\n';
                Clean();
            }
        }
        private void NewDialog_Click(object sender, EventArgs e)//сделано
        {
            SourceBox.Clear();
            QuestionBox.Clear();
            AnswersGrid.Rows.Clear();
            NEW = true;
        }

        private void EditDialog_Click(object sender, EventArgs e)//сделано
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "KUR|*.kur";
                openFileDialog1.Title = "Select a kur File";

                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string path = openFileDialog1.FileName;
                    System.IO.StreamReader file = new System.IO.StreamReader(path, Encoding.Default);
                    SourceBox.Text = file.ReadToEnd();
                    file.Close();
                    NEW = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
        }


        private void OK_Click(object sender, EventArgs e)//сделано
        {
            NextQuestion_Click(sender, e);
            SaveFileDialog s = new SaveFileDialog();
            s.Filter = "KUR|*.kur";
            if (s.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Stream str = File.OpenWrite(s.FileName);
                StreamWriter wr = new StreamWriter(str);
                wr.Write(SourceBox.Text);
                wr.Dispose();
                wr.Close();
            }

            this.Close();
        }

        private void IsAnswerExistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            AnswerLabel.Visible = true;
            if (IsAnswerExistBox.SelectedIndex == 0)
            {
                CorrectAnswerBox.Visible = false;
                AnswersGrid.Visible = true;
            }
            else
            {
                CorrectAnswerBox.Visible = true;
                AnswersGrid.Visible = false;
            }
        }

        private void IsImageExistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ImageSelectionLabel.Visible = true;
            ImageDirectoryBox.Visible = true;
            ImageSelectionButton.Visible = true;
        }

        private void ImageSelectionButton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "jpg|*.jpg, bmp | *.bmp, gif | *.gif, png | *.png, tiff | *.tiff, ico | *.ico";
                openFileDialog1.Title = "Select an image";

                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ImageDirectoryBox.Text = openFileDialog1.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
        }
    }

}

