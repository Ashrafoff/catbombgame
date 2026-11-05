using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace catbombgame
{
    public partial class Form1 : Form
    {
        int bombIndex = -1;
        int xal = 0;
        PictureBox[] boxes;
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            boxes = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6 };

            for (int i = 0; i < boxes.Length; i++)
            {
                boxes[i].Click += PictureBox_Click;
                boxes[i].Cursor = Cursors.Hand;
            }

            button2.Click += button2_Click;
            YeniOyun();
        }

        private void YeniOyun()
        {
            if (boxes == null) return;

            xal = 0;
            label2.Text = "0";

            bombIndex = rnd.Next(0, boxes.Length);

            foreach (var box in boxes)
            {
                box.Image = Properties.Resources.question;
                box.Enabled = true;
            }
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox clickedBox = sender as PictureBox;
            if (clickedBox == null) return;

            int clickedIndex = Array.IndexOf(boxes, clickedBox);
            clickedBox.Enabled = false;

            if (clickedIndex == bombIndex)
            {
                clickedBox.Image = Properties.Resources.bomba;

                using (SoundPlayer player = new SoundPlayer(Properties.Resources.exp2))
                {
                    player.Play();
                }

                MessageBox.Show("Oyun bitdi", "Məğlubiyyət", MessageBoxButtons.OK, MessageBoxIcon.Information);

                foreach (var box in boxes)
                {
                    box.Enabled = false;
                }
            }
            else
            {
                clickedBox.Image = Properties.Resources.cat;
                xal++;
                label2.Text = xal.ToString();

                if (xal == 5)
                {
                    MessageBox.Show("Oyunu qazandınız, Yeni oyun üçün ok düyməsinə basın", "Qələbə", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    YeniOyun();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            YeniOyun();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}