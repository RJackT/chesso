namespace Chesso
{
    public partial class Form1 : Form
    {
        public static bool bStarted = false;
        public static bool bWhite = true;

        enum pieces : int{
            empty,
            bond,
            knight,
            bishop,
            rook,
            queen,
            king
        }
        public class board {

            public boxes[][] box = new boxes[8][];
            
            public void init()
            {
                for(int i; i < box.Length; i++)
                {

                }
            }
            public class boxes
            {
                int pieceID; // 0 = no piece
                int marked; // check, movable, kill.
            }
        }

        

        public Form1()
        {
            InitializeComponent();
            

        }
        private void drawChessBoard()
        {
            Graphics g = CreateGraphics();

            SolidBrush c1 = new System.Drawing.SolidBrush(System.Drawing.Color.WhiteSmoke);
            SolidBrush c2 = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            for (int h = 0; h <= 8; h++)
            {
                for (int i = 0; i <= 8; i++)
                {
                    if (h % 2 == 0)
                        if (i % 2 == 0)
                        {
                            g.FillRectangle(c1, new Rectangle((i-1)*100, (h-1)*100, (i)*100, (h)*100));
                        }
                        else
                        {
                            g.FillRectangle(c2, new Rectangle((i-1)*100, (h-1)*100, (i)*100, (h)*100));
                        }
                    else
                        if (i % 2 != 0)
                    {
                        g.FillRectangle(c1, new Rectangle((i-1)*100, (h-1)*100, (i)*100, (h)*100));
                    }
                    else
                    {
                        g.FillRectangle(c2, new Rectangle((i-1)*100, (h-1)*100, (i)*100, (h)*100));
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bStarted = true;
            bWhite = true;
        }

        private void render()
        {
            //Real mouse pos is MousePosition.X-11, MousePosition.Y-45
            //SolidBrush c2 = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);
            if (bStarted)
            {
                Graphics g = CreateGraphics();

                
                button1.Visible = false; // start as white button
                button2.Visible = false; // Start as black button
                label1.Visible = false;
                drawChessBoard();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //do whatever you want             
            render();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TopMost = true;// funky shenanigans
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = (50); // 1/20 secs updaterar den
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bStarted = true;
            bWhite = false;
        }
    }
}