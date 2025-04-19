namespace Tank
{
    /// <summary>
    /// The idea got from the video https://www.youtube.com/watch?v=GccTtyXI_kA
    /// </summary>
    public partial class Form1 : Form
    {
        private int _speed;
        private int _score = 0;
        private int countInRow = 0;
        private readonly int rows = 3;
        private int Total
        {
            get { return countInRow * rows; }
        }

        private List<int> _evaluatedTankIds = new List<int>();
        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            timer1.Stop();
            Controls.Clear();
            _evaluatedTankIds.Clear();

            countInRow = (int)Math.Floor((decimal)Width / 100);
            int id = 0;
            for (int i = 0; i < countInRow; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    CreateRowOfTanks(i, j, id);
                    id++;
                }
            }

            timer1.Start();
            _score = 0;
            Text = $"Your score is: {_score}/{Total}";

            Fly = new PictureBox();
            Fly.Image = Properties.Resources.fly;
            Fly.Size = new Size(100, 100);
            Fly.Location = new Point((int)(Width / 2 - Fly.Width / 2), Height - Fly.Height * 2);
            Fly.Name = "Fly";
            Fly.Tag = "Fly";
            Controls.Add(Fly);

        }

        private void CreateRowOfTanks(int i, int j, int id)
        {
            Tank enemyBox = new Tank(id);
            enemyBox.Location = new Point(i * 100, j * 100);
            Controls.Add(enemyBox);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                _speed = -30;
            }

            if (e.KeyCode == Keys.Right)
            {
                _speed = 30;
            }

            if (e.KeyCode == Keys.Space)
            {
                Shot();
            }

            if (e.KeyCode == Keys.F5)
            {
                InitializeGame();
            }
        }

        private void Shot()
        {
            PictureBox shotBox = new PictureBox();
            shotBox.Image = Properties.Resources.bullet;
            shotBox.Size = new Size(50, 50);
            shotBox.Location = new Point(Fly.Left + shotBox.Width / 2, Fly.Top - shotBox.Height);
            shotBox.Tag = "Bullet";
            Controls.Add(shotBox);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Fly.Left += _speed;

            if (Fly.Left <= 0)
            {
                Fly.Left = 0;
            }

            if (Fly.Left >= Width - Fly.Width)
            {
                Fly.Left = Width - Fly.Width;
            }

            foreach (PictureBox control in Controls)
            {
                if (control.Tag == "Enemy")
                {
                    control.Left += 10;
                    if (control.Left > Width)
                    {
                        control.Left = 0;
                        control.Top += 100;
                    }

                    if (control.Bounds.IntersectsWith(Fly.Bounds))
                    {
                        timer1.Stop();
                        MessageBox.Show("You are losers. Buy defusers!", "Loser!");
                    }
                }

                if (control.Tag == "Bullet")
                {
                    control.Top -= 20;
                    if (control.Top < 0)
                    {
                        Controls.Remove(control);
                    }
                }
            }

            foreach (PictureBox x in Controls)
            {
                foreach (PictureBox y in Controls)
                {
                    if (x.Tag == "Enemy" &&
                        y.Tag == "Bullet" &&
                        x.Bounds.IntersectsWith(y.Bounds))
                    {
                        if (!_evaluatedTankIds.Contains(((Tank)x).Id))
                        {
                            _evaluatedTankIds.Add(((Tank)x).Id);
                        }
                        else
                        {
                            continue;
                        }

                        Controls.Remove(x);
                        Controls.Remove(y);

                        _score++;
                        Text = $"Your score is: {_score}/{Total}";

                        if (_score >= Total)
                        {
                            timer1.Stop();
                            MessageBox.Show("And the neeeeew! Champion of the world!", "You winner!");
                        }
                    }
                }
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                _speed = 0;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            InitializeGame();
        }
    }
}   
