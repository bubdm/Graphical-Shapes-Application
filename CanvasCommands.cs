using System.Drawing;

namespace ASEDemo
{
    public class CanvasCommands
    {
        // instance data
        readonly Graphics g;
        readonly Pen pen;
        int xPos, yPos; //pens position

        public CanvasCommands(Graphics g)
        {
            this.g = g;
            xPos = yPos = 5;
            pen = new Pen(Color.Red, 1);

            g.DrawEllipse(pen, xPos, yPos, 5, 5); //makes that dot, so pens visible
        }

        public void FillPen(string mode)
        {
            if (mode == "on")
            {
                pen.Width = 100;
            }
            if (mode == "off")
            {
                pen.Width = 0;
            }
        }

        public void PenColour(string a)
        {
            if (a == "red")
            {
                pen.Color = Color.Red;
            }
            else if (a == "green")
            {
                pen.Color = Color.Green;
            }
            else if (a == "black")
            {
                pen.Color = Color.Black;
            }
            else if (a == "blue")
            {
                pen.Color = Color.Blue;
            }
        }

        public void MovePen(int loc, int loc2)
        {
            xPos = loc;
            yPos = loc2;
            g.DrawEllipse(pen, xPos, yPos, 5, 5); //makes that dot, so pens visible
        }

        public void Clear()
        {
            g.Clear(SystemColors.Control);
        }

        public void Reset()
        {
            MovePen(0, 0);    //moves pen back to normal
        }

        public void DrawTo(int toX, int toY)
        {
            g.DrawLine(pen, xPos, yPos, toX, toY);
            xPos = toX;
            yPos = toY;
        }

        public void DrawCircle(float radius)
        {
            g.DrawEllipse(pen, xPos, yPos, radius + radius, radius + radius);   //so one sole value (R) as param
        }

        public void DrawRectangle(int width, int height)
        {
            g.DrawRectangle(pen, xPos, yPos, width, height);
        }

        public void DrawTriangle(int point1, int point2)
        {
            int x = point1;
            int y = point2;

            Point[] points = {          //can draw any triangle with this
                new Point(100, 100),
                new Point(65, x),       // 30,15
                new Point(y, 100)
            };
            g.DrawPolygon(pen, points);
        }
    }
}