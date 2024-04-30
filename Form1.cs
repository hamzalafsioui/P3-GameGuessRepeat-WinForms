using P3_GameGuessRepeat.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P3_GameGuessRepeat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        stGameStatus GameStatus;
        struct stGameStatus
        {
            public short NumberOfRepetition;
            public string Number;
            public short Counter;
            public short Round;
            public short Correct;
            public short Wrong;
        }
        // Create a new instance of the Random class
        Random random = new Random();

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            ResetParameter();
            GameStatus.Round++;
            foreach(Button button in panel1.Controls)
            {
                button.Text = random.Next(10, 40).ToString();
            }
            lblNumber.Text = random.Next(10, 40).ToString();
            GameStatus.Number = lblNumber.Text;
            GameStatus.NumberOfRepetition = 0;
            foreach(Button button in panel1.Controls)
            {
                if (button.Text == GameStatus.Number)
                {
                    GameStatus.NumberOfRepetition++;
                }
            }
            notifyIcon1.Icon = SystemIcons.Application;
            notifyIcon1.BalloonTipText = "Time is Just 10 Seconds";
            notifyIcon1.BalloonTipTitle = "Guess Game";
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.ShowBalloonTip(1000);
            
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Text Box is Empty Please Full it", "attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                timer1.Enabled = false;

                if (GameStatus.NumberOfRepetition.ToString() == textBox1.Text)
                {
                    pictureBox1.Image = Resources.Correct;
                    lblResult.Text = "Correct -)";
                    lblResult.ForeColor = Color.Green;
                    GameStatus.Correct++;
                }
                else
                {
                    pictureBox1.Image = Resources.Wrong;
                    lblResult.Text = "Wrong (-";
                    lblResult.ForeColor = Color.Red;
                    GameStatus.Wrong++;
                }
                MessageBox.Show(lblResult.Text);
                if(MessageBox.Show("Do You want to Play Again?","Guess Game",MessageBoxButtons.OKCancel,MessageBoxIcon.Question)== DialogResult.OK)
                {
                    btnGenerate_Click(sender, e);
                }
                else
                {
                    panel1.Enabled = false;
                    btnGenerate.Enabled = false;
                    textBox1.Enabled = false;
                    btnCheck.Enabled = false;
                    MessageBox.Show("You play " + GameStatus.Round + " Round and You have " + GameStatus.Correct +
                        " Correct Answers " + GameStatus.Wrong + " Wrong Answers ", "Final Results",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
            }
        }
        private void ResetParameter()
        {
            pictureBox1.Image = Resources.Question;
            lblResult.Text = "In Progress";
            lblResult.ForeColor = Color.Blue;
            GameStatus.NumberOfRepetition = 0;
            GameStatus.Counter = 0;
            timer1.Enabled = true;
            textBox1.Clear();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GameStatus.Counter++;
            lblTimer.Text = GameStatus.Counter.ToString() + "s";
            if (GameStatus.Counter == 10)
            {
                timer1.Enabled= false;
                if(MessageBox.Show("Time Over", "End", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    btnGenerate_Click(sender, e);

                }

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnGenerate_Click(sender, e);
        }

       
    }
}
