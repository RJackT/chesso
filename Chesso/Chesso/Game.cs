using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chesso
{
    public partial class Game : Form
    {
        public Game()
        {
            InitializeComponent();
        }

        private void drawChessBoard()
        {
            Graphics g = CreateGraphics();

            SolidBrush c1 = new System.Drawing.SolidBrush(System.Drawing.Color.WhiteSmoke);
            SolidBrush c2 = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

            for ( int i = 0; i < 80; i++ )
            {
                if( i % 2 == 0)
                {
                    g.FillRectangle(c1, new Rectangle((i-1)*100, 0, (i-1)*100, (i-1)*100));
                }
                else
                {
                    g.FillRectangle(c2, new Rectangle((i-1)*100, 0, (i-1)*100, (i-1)*100));
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            drawChessBoard();
        }
    }
}
