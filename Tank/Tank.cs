namespace Tank;

public class Tank : PictureBox
{
    public int Id { get; set; }
    public Tank(int id)
    {
        Image = Properties.Resources.tank;
        Size = new Size(100, 100);
        Tag = "Enemy";
        Id = id;
    }

    //public int X { get; set; }
    //public int Y { get; set; }
    //public int Speed { get; set; }
    //public int Direction { get; set; } // 0 - left, 1 - right
}