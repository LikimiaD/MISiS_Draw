namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private Rectangle[] rectangle;
        public Random random = new Random();
        public Form1()
        {
            InitializeComponent();
            label1.Text = "";
            rectangle = new Rectangle[]
            {
            new Rectangle(this, random.Next(0, 100),random.Next(10, 50),Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256)), Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256)), 0),
            new Rectangle(this, random.Next(10, 50),random.Next(10, 50),Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256)), Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256)),1),
            new Rectangle(this, random.Next(0, 100),random.Next(10, 50),Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256)), Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256)), 0),
            };
        }



        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var item in rectangle)
                item.Draw();
        }



        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < rectangle.Length; i++)
            {
                if (rectangle[i].CheckPoint(new Point(e.X, e.Y)))
                {
                    label1.Text = "Площадь " + rectangle[i].CalcArea();
                    rectangle[i].Draw(true);
                    MessageBox.Show("Вы нажали на треугольник");
                }
            }
        }
    }
    public class GeometricFigure
    {
        protected Point CentrPoint;
        protected float lineThickness;
        private Color fillColor;
        private Color lineColor;
        public Color FillColor
        {
            get
            {
                return fillColor;
            }
        }
        public Color LineColor { get => lineColor; }
        public GeometricFigure(Color fillColor, Color lineColor)
        {
            this.fillColor = fillColor;
            this.lineColor = lineColor;
        }
        protected void ChangeColor(Color fillColor, Color lineColor)
        {
            this.fillColor = fillColor;
            this.lineColor = lineColor;
        }
    }
    public class Rectangle : GeometricFigure
    {
        private Form form;
        private Graphics graphics;
        private Random random;
        private int width;
        private int height;
        private int checker;
        public Rectangle(Form form, int width, int height,
        Color fillColor, Color lineColor,int checker) : base(fillColor, lineColor)
        {
            this.form = form; this.width = width; this.height = height; this.checker = checker;
            random = new Random(width);
            CentrPoint = new Point()
            {
                X = random.Next(width, form.ClientSize.Width - width),
                Y = random.Next(height, form.ClientSize.Height - height)
            };
            lineThickness = 1;
            graphics = form.CreateGraphics();

         }
        public void Draw(bool fillRandom = false)
        {
            if (fillRandom)
            {
                if (checker != 1)
                {
                    int x = CentrPoint.X - width / 2; int y = CentrPoint.Y - height / 2;
                    ChangeColor(
                    Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256)),
                    Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256))
                    );
                    lineThickness = random.Next(0, 10);
                    graphics.FillRectangle(new SolidBrush(FillColor), x + (int)lineThickness / 2,
                    y + (int)lineThickness / 2, width, height);
                    graphics.DrawRectangle(new Pen(LineColor, lineThickness), x, y, width, height);
                }
                else
                {
                    int x = CentrPoint.X - width / 2; int y = CentrPoint.Y - height / 2;
                    ChangeColor(
                    Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256)),
                    Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256))
                    );
                    lineThickness = random.Next(0, 10);
                    graphics.FillEllipse(new SolidBrush(FillColor), x + (int)lineThickness / 2,
                    y + (int)lineThickness / 2, width, height);
                    graphics.DrawEllipse(new Pen(LineColor, lineThickness), x, y, width, height);
                }
            }
            else
                Draw();
        }
        private void Draw()
        {
            if (checker != 1)
            {
                int x = CentrPoint.X - width / 2;
                int y = CentrPoint.Y - height / 2;
                graphics.FillRectangle(new SolidBrush(FillColor), x + (int)lineThickness / 2,
                y + (int)lineThickness / 2, width, height);
                graphics.DrawRectangle(new Pen(LineColor, lineThickness), x, y, width, height);
            }
            else
            {
                int x = CentrPoint.X - width / 2;
                int y = CentrPoint.Y - height / 2;
                graphics.FillEllipse(new SolidBrush(FillColor), x + (int)lineThickness / 2,
                y + (int)lineThickness / 2, width, height);
                graphics.DrawEllipse(new Pen(LineColor, lineThickness), x, y, width, height);
            }
        }
        public string CalcArea()
        {
            return checker == 1 ? "круга " + Math.PI * width / 2 : "прямоугольника " + width * height;
        }
        public bool CheckPoint(Point point)
        {
            if ((point.X >= CentrPoint.X - width / 2) && (point.X <= CentrPoint.X + width / 2))
                if ((point.Y >= CentrPoint.Y - height / 2) && (point.Y <= CentrPoint.Y + height / 2))
                    return true;
            return false;
        }
    }
}