using System;
using System.Drawing;
using System.Windows.Forms;

public class CustomProgressBar : ProgressBar
{
    private string scrollingText = "Example Text";
    private int scrollPosition = 0;
    private Timer scrollTimer;

    public CustomProgressBar()
    {
        this.SetStyle(ControlStyles.UserPaint, true);
        scrollTimer = new Timer();
        scrollTimer.Interval = 100; // Adjust the interval to control the speed
        scrollTimer.Tick += new EventHandler(OnTimerTick);
        scrollTimer.Start();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        Rectangle rect = this.ClientRectangle;
        Graphics g = e.Graphics;

        ProgressBarRenderer.DrawHorizontalBar(g, rect);
        rect.Inflate(-3, -3);

        if (Value > 0)
        {
            Rectangle clip = new Rectangle(rect.X, rect.Y, (int)Math.Round(((float)Value / Maximum) * rect.Width), rect.Height);
            ProgressBarRenderer.DrawHorizontalChunks(g, clip);
        }

        using (Brush textBrush = new SolidBrush(Color.Black))
        {
            SizeF textSize = g.MeasureString(scrollingText, this.Font);
            float textY = (rect.Height - textSize.Height) / 2;
            g.DrawString(scrollingText, this.Font, textBrush, scrollPosition, textY);
        }
    }

    private void OnTimerTick(object sender, EventArgs e)
    {
        scrollPosition -= 5; // Adjust the value to control the scrolling speed
        if (scrollPosition + this.Width < 0)
        {
            scrollPosition = this.Width;
        }
        this.Invalidate(); // This will cause the control to be redrawn
    }

    public string ScrollingText
    {
        get { return scrollingText; }
        set { scrollingText = value; }
    }

    public int ScrollSpeed
    {
        get { return scrollTimer.Interval; }
        set { scrollTimer.Interval = value; }
    }
}
