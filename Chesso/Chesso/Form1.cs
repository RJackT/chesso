using System.Drawing.Text;


namespace Chesso
{
    public partial class Form1 : Form
    {

        /// <summary>
        ///  Classes and other enum type stuff for organization
        /// </summary>
        /// 

        enum pieces : int
        {
            none,
            pawn_white,
            knight_white,
            bishop_white,
            rook_white,
            queen_white,
            king_white,
            pawn_black,
            knight_black,
            bishop_black,
            rook_black,
            queen_black,
            king_black
        }
        enum box_mark : int
        {
            none,
            moveable,
            capture,
            mate,
            selected
        }
        public class board_class
        {

            public boxes?[][] box { get; set; }


            public board_class()
            {
                box = new boxes?[][]
                {
                new boxes?[] { new boxes { pieceID = (int)pieces.rook_white }, new boxes { pieceID = (int)pieces.knight_white }, new boxes { pieceID = (int)pieces.bishop_white }, new boxes { pieceID = (int)pieces.queen_white }, new boxes { pieceID = (int)pieces.king_white }, new boxes { pieceID = (int)pieces.bishop_white }, new boxes { pieceID = (int)pieces.knight_white }, new boxes{ pieceID = (int)pieces.rook_white } },
                new boxes?[] { new boxes{ pieceID = (int)pieces.pawn_white }, new boxes { pieceID = (int)pieces.pawn_white }, new boxes { pieceID = (int)pieces.pawn_white }, new boxes { pieceID = (int)pieces.pawn_white }, new boxes { pieceID = (int)pieces.pawn_white }, new boxes { pieceID = (int)pieces.pawn_white }, new boxes { pieceID = (int)pieces.pawn_white }, new boxes{ pieceID = (int)pieces.pawn_white } },
                new boxes?[] { new boxes { pieceID = (int)pieces.none }, new boxes { pieceID = (int)pieces.none }, new boxes { pieceID = (int)pieces.none }, new boxes { pieceID = (int)pieces.none }, new boxes { pieceID = (int)pieces.none }, new boxes { pieceID = (int)pieces.none }, new boxes { pieceID = (int)pieces.none }, new boxes { pieceID = (int)pieces.none } },
                new boxes?[] { new boxes { pieceID = (int)pieces.none }, new boxes { pieceID = (int)pieces.none }, new boxes { pieceID = (int)pieces.none }, new boxes { pieceID = (int)pieces.none }, new boxes { pieceID = (int)pieces.none }, new boxes { pieceID = (int)pieces.none }, new boxes { pieceID = (int)pieces.none }, new boxes { pieceID = (int)pieces.none } },
                new boxes?[] { new boxes { pieceID = (int)pieces.none }, new boxes { pieceID = (int)pieces.none }, new boxes { pieceID = (int)pieces.none }, new boxes { pieceID = (int)pieces.none }, new boxes { pieceID = (int)pieces.none }, new boxes { pieceID = (int)pieces.none }, new boxes { pieceID = (int)pieces.none }, new boxes { pieceID = (int)pieces.none } },
                new boxes?[] { new boxes { pieceID = (int)pieces.none }, new boxes { pieceID = (int)pieces.none }, new boxes { pieceID = (int)pieces.none }, new boxes { pieceID = (int)pieces.none }, new boxes { pieceID = (int)pieces.none }, new boxes { pieceID = (int)pieces.none }, new boxes { pieceID = (int)pieces.none }, new boxes { pieceID = (int)pieces.none } },
                new boxes?[] { new boxes{ pieceID = (int)pieces.pawn_black }, new boxes { pieceID = (int)pieces.pawn_black }, new boxes { pieceID = (int)pieces.pawn_black }, new boxes { pieceID = (int)pieces.pawn_black }, new boxes { pieceID = (int)pieces.pawn_black }, new boxes { pieceID = (int)pieces.pawn_black }, new boxes { pieceID = (int)pieces.pawn_black }, new boxes{ pieceID = (int)pieces.pawn_black } },
                new boxes?[] { new boxes { pieceID = (int)pieces.rook_black }, new boxes { pieceID = (int)pieces.knight_black }, new boxes { pieceID = (int)pieces.bishop_black }, new boxes { pieceID = (int)pieces.queen_black }, new boxes { pieceID = (int)pieces.king_black }, new boxes { pieceID = (int)pieces.bishop_black }, new boxes { pieceID = (int)pieces.knight_black }, new boxes{ pieceID = (int)pieces.rook_black } },
                };
            }

            public void resetMarkings()
            {
                for (int i = 0; i < box.Length; i++)
                {
                    for (int j = 0; j < box[i].Length; j++) { box[i][j].marked = (int)box_mark.none; }
                }
            }

            public class boxes
            {
                public int pieceID; // 0 = no piece
                public int marked = 0; // check, movable, kill.
                public bool active = false;
                public int colour()
                {
                    if (pieceID == 0) return 0; 
                    else if (pieceID > 6) return 1;
                    else if (pieceID < 7) return 2;
                    return -1;
                    
                }
            }
        }

        /// <summary>
        /// Semi-globals go under here
        /// </summary>
        public board_class board = new board_class();
        PrivateFontCollection pfc = new PrivateFontCollection();
        Font pieceFont;
        board_class.boxes active_piece;
        public static bool bStarted = false;
        public static bool bWhite = true;
        public static int sideToMove = 2;

        public static bool bBlack_checked = false;
        public static bool bWhite_checked = false;

        public static bool bUpdate = true;




        public Form1()
        {
            InitializeComponent();

            this.MouseClick += mouseClick;
        }

        private void button1_Click(object sender, EventArgs e) // Start as white button
        {
            bStarted = true;
            bWhite = true;
        }
        private void button2_Click(object sender, EventArgs e) // start as black button
        {
            bStarted = true;
            bWhite = false;
        }

        private void drawChessBoard()
        {
            Graphics g = CreateGraphics();

            // Orkar inte döpa om färgerna till vad det faktiskt är

            SolidBrush white = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 238, 238, 210));  // Vit- ish
            SolidBrush black = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 118, 150, 86)); // grön
            SolidBrush moveable_white = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 214, 34, 129));
            SolidBrush moveable_black = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 226, 135, 77));
            SolidBrush selected_white = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 248, 244, 108));
            SolidBrush selected_black = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 248, 244, 108));
            SolidBrush capture_white = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 248, 244, 108));
            SolidBrush capture_black = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 187, 203, 43));
            SolidBrush mate_white = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 211, 99, 90));
            SolidBrush mate_black = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 181, 77, 59));


            for (int h = 0; h < 8; ++h)
            {
                for (int i = 0; i < 8; ++i)
                {
                    if (h % 2 == 0)
                        if (i % 2 == 0)
                        {

                            switch (board.box[i][h].marked)
                            {
                                case (int)box_mark.moveable:
                                    g.FillRectangle(moveable_white, new Rectangle((i) * 100, (h) * 100, (i + 1) * 100, (h + 1) * 100));
                                    break;
                                case (int)box_mark.capture:
                                    g.FillRectangle(capture_white, new Rectangle((i) * 100, (h) * 100, (i + 1) * 100, (h + 1) * 100));
                                    break;
                                case (int)box_mark.mate:
                                    g.FillRectangle(mate_white, new Rectangle((i) * 100, (h) * 100, (i + 1) * 100, (h + 1) * 100));
                                    break;
                                case (int)box_mark.selected:
                                    g.FillRectangle(selected_white, new Rectangle((i) * 100, (h) * 100, (i + 1) * 100, (h + 1) * 100));
                                    break;
                                default:
                                    g.FillRectangle(white, new Rectangle((i) * 100, (h) * 100, (i + 1) * 100, (h + 1) * 100));
                                    break;
                            }


                        }
                        else
                        {
                            switch (board.box[i][h].marked)
                            {
                                case (int)box_mark.moveable:
                                    g.FillRectangle(moveable_black, new Rectangle((i) * 100, (h) * 100, (i + 1) * 100, (h + 1) * 100));
                                    break;
                                case (int)box_mark.capture:
                                    g.FillRectangle(capture_black, new Rectangle((i) * 100, (h) * 100, (i + 1) * 100, (h + 1) * 100));
                                    break;
                                case (int)box_mark.mate:
                                    g.FillRectangle(mate_black, new Rectangle((i) * 100, (h) * 100, (i + 1) * 100, (h + 1) * 100));
                                    break;
                                case (int)box_mark.selected:
                                    g.FillRectangle(selected_black, new Rectangle((i) * 100, (h) * 100, (i + 1) * 100, (h + 1) * 100));
                                    break;
                                default:
                                    g.FillRectangle(black, new Rectangle((i) * 100, (h) * 100, (i + 1) * 100, (h + 1) * 100));
                                    break;
                            }
                        }
                    else
                          if (i % 2 != 0)
                    {
                        switch (board.box[i][h].marked)
                        {
                            case (int)box_mark.moveable:
                                g.FillRectangle(moveable_white, new Rectangle((i) * 100, (h) * 100, (i + 1) * 100, (h + 1) * 100));
                                break;
                            case (int)box_mark.capture:
                                g.FillRectangle(capture_white, new Rectangle((i) * 100, (h) * 100, (i + 1) * 100, (h + 1) * 100));
                                break;
                            case (int)box_mark.mate:
                                g.FillRectangle(mate_white, new Rectangle((i) * 100, (h) * 100, (i + 1) * 100, (h + 1) * 100));
                                break;
                            case (int)box_mark.selected:
                                g.FillRectangle(selected_white, new Rectangle((i) * 100, (h) * 100, (i + 1) * 100, (h + 1) * 100));
                                break;
                            default:
                                g.FillRectangle(white, new Rectangle((i) * 100, (h) * 100, (i + 1) * 100, (h + 1) * 100));
                                break;
                        }
                    }
                    else
                    {
                        switch (board.box[i][h].marked)
                        {
                            case (int)box_mark.moveable:
                                g.FillRectangle(moveable_black, new Rectangle((i) * 100, (h) * 100, (i + 1) * 100, (h + 1) * 100));
                                break;
                            case (int)box_mark.capture:
                                g.FillRectangle(capture_black, new Rectangle((i) * 100, (h) * 100, (i + 1) * 100, (h + 1) * 100));
                                break;
                            case (int)box_mark.mate:
                                g.FillRectangle(mate_black, new Rectangle((i) * 100, (h) * 100, (i + 1) * 100, (h + 1) * 100));
                                break;
                            case (int)box_mark.selected:
                                g.FillRectangle(selected_black, new Rectangle((i) * 100, (h) * 100, (i + 1) * 100, (h + 1) * 100));
                                break;
                            default:
                                g.FillRectangle(black, new Rectangle((i) * 100, (h) * 100, (i + 1) * 100, (h + 1) * 100));
                                break;
                        }
                    }
                }
            }
        }

        private void drawPieces()
        {
            Graphics g = CreateGraphics();
            SolidBrush black = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

            for (int h = 0; h < 8; ++h)
            {
                for (int i = 0; i < 8; ++i)
                {
                    switch (board.box[i][h].pieceID) // Ritar alla pieces på sina ställen
                    {
                        case (int)pieces.pawn_white:
                            g.DrawString("p", pieceFont, black, new Point((i) * 100, (h) * 100));
                            break;
                        case (int)pieces.knight_white:
                            g.DrawString("n", pieceFont, black, new Point((i) * 100, (h) * 100));
                            break;
                        case (int)pieces.bishop_white:
                            g.DrawString("b", pieceFont, black, new Point((i) * 100, (h) * 100));
                            break;
                        case (int)pieces.rook_white:
                            g.DrawString("r", pieceFont, black, new Point((i) * 100, (h) * 100));
                            break;
                        case (int)pieces.queen_white:
                            g.DrawString("q", pieceFont, black, new Point((i) * 100, (h) * 100));
                            break;
                        case (int)pieces.king_white:
                            g.DrawString("k", pieceFont, black, new Point((i) * 100, (h) * 100));
                            break;
                        case (int)pieces.pawn_black:
                            g.DrawString("o", pieceFont, black, new Point((i) * 100, (h) * 100));
                            break;
                        case (int)pieces.knight_black:
                            g.DrawString("m", pieceFont, black, new Point((i) * 100, (h) * 100));
                            break;
                        case (int)pieces.bishop_black:
                            g.DrawString("v", pieceFont, black, new Point((i) * 100, (h) * 100));
                            break;
                        case (int)pieces.rook_black:
                            g.DrawString("t", pieceFont, black, new Point((i) * 100, (h) * 100));
                            break;
                        case (int)pieces.queen_black:
                            g.DrawString("w", pieceFont, black, new Point((i) * 100, (h) * 100));
                            break;
                        case (int)pieces.king_black:
                            g.DrawString("l", pieceFont, black, new Point((i) * 100, (h) * 100));
                            break;
                    }
                }
            }
        }


        void setMarkings()
        {
            // Always Calculate kings

            for (int i = 0; i < board.box.Length; i++)
            {
                for (int j = 0; j < board.box[i].Length; j++)
                {
                    if (board.box[i][j].marked == (int)box_mark.selected)
                    {
                        //List<Point> moves = validMoves(i, j);
                        markLegalMove(i, j);
                    }
                }
            }
        }

        /// <summary>
        /// / DENNNNNNNNNNNNNNNNNA DEL ÄR SÅ JÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄVLA JOBBBIIGGGGGGGG SÅ MYCKET JÄVLA SMÅPILLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL.... IINTE SVÅRT MEN SMÅÅÅPILLLLLLLLLLLLLLLLLLLLL
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void markLegalMove(int x, int y)
        {
            switch (board.box[x][y].pieceID)
            {
                case (int)pieces.pawn_white:
                    if(x < 7)
                    {
                        int x1 = x + 1; // För att lättare applicera till alla 
                        int y1 = y;
                        
                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;


                        if(board.box[x][y].colour() != board.box[x+1][y+1].colour() && board.box[x + 1][y + 1].pieceID != (int)pieces.none)
                        {
                            board.box[x + 1][y + 1].marked = (int)box_mark.capture;
                            break;
                        }
                        if (board.box[x][y].colour() != board.box[x + 1][y - 1].colour() && board.box[x + 1][y - 1].pieceID != (int)pieces.none)
                        {
                            board.box[x + 1][y - 1].marked = (int)box_mark.capture;
                            break;
                        }
                    }
                    break;
                case (int)pieces.knight_white:
                    if (x < 6)
                    {

                        if (y < 7) if (board.box[x][y].colour() != board.box[x + 2][y + 1].colour() && board.box[x + 2][y + 1].pieceID != 0) board.box[x + 2][y + 1].marked = (int)box_mark.capture;
                            else if (board.box[x][y].colour() != board.box[x + 2][y + 1].colour())
                                board.box[x + 2][y + 1].marked = (int)box_mark.moveable;

                        if (y > 0) if (board.box[x][y].colour() != board.box[x + 2][y - 1].colour() && board.box[x + 2][y - 1].pieceID != 0) board.box[x + 2][y - 1].marked = (int)box_mark.capture;
                            else if (board.box[x][y].colour() != board.box[x + 2][y - 1].colour())
                                board.box[x + 2][y - 1].marked = (int)box_mark.moveable;

                    }
                    if (x >= 2)
                    {

                        if (y < 7) if (board.box[x][y].colour() != board.box[x - 2][y + 1].colour() && board.box[x - 2][y + 1].pieceID != 0) board.box[x - 2][y + 1].marked = (int)box_mark.capture;
                            else if (board.box[x][y].colour() != board.box[x - 2][y + 1].colour())
                                board.box[x - 2][y + 1].marked = (int)box_mark.moveable;

                        if (y > 0) if (board.box[x][y].colour() != board.box[x - 2][y - 1].colour() && board.box[x - 2][y - 1].pieceID != 0) board.box[x - 2][y - 1].marked = (int)box_mark.capture;
                            else if (board.box[x][y].colour() != board.box[x - 2][y - 1].colour())
                                board.box[x - 2][y - 1].marked = (int)box_mark.moveable;
                    }
                    if (x < 6)
                    {
                        if (y <= 5) if (board.box[x][y].colour() != board.box[x + 1][y + 2].colour() && board.box[x + 1][y + 2].pieceID != 0)
                                board.box[x + 1][y + 2].marked = (int)box_mark.capture;
                            else
                                if ((board.box[x][y].colour() != board.box[x + 1][y + 2].colour()))
                                board.box[x + 1][y + 2].marked = (int)box_mark.moveable;

                        if (y >= 2) if (board.box[x][y].colour() != board.box[x + 1][y - 2].colour() && board.box[x + 1][y - 2].pieceID != 0)
                                board.box[x + 1][y - 2].marked = (int)box_mark.capture;
                            else
                                if ((board.box[x][y].colour() != board.box[x + 1][y - 2].colour()))
                                board.box[x + 1][y - 2].marked = (int)box_mark.moveable;
                    }


                    if (x >= 2)
                    {
                        if (y < 6) if (board.box[x][y].colour() != board.box[x - 1][y + 2].colour() && board.box[x - 1][y + 2].pieceID != 0) board.box[x - 1][y + 2].marked = (int)box_mark.capture;
                            else if (board.box[x][y].colour() != board.box[x - 1][y + 2].colour())
                                board.box[x - 1][y + 2].marked = (int)box_mark.moveable;

                        if (y >= 2) if (board.box[x][y].colour() != board.box[x - 1][y - 2].colour() && board.box[x - 1][y - 2].pieceID != 0) board.box[x - 1][y - 2].marked = (int)box_mark.capture;
                            else if (board.box[x][y].colour() != board.box[x - 1][y - 2].colour())
                                board.box[x - 1][y - 2].marked = (int)box_mark.moveable;
                    }
                    break;
                case (int)pieces.bishop_white:
                    for (int i = 1; x + i <= 7 && y + i <= 7; i++)
                    {
                        int x1 = x + i; // För att lättare applicera till alla 
                        int y1 = y + i;
                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    for (int i = 1; x + i <= 7 && y - i >= 0; i++)
                    {
                        int x1 = x + i; // För att lättare applicera till alla 
                        int y1 = y - i;
                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    for (int i = 1; x - i >= 0 && y + i <= 7; i++)
                    {
                        int x1 = x - i; // För att lättare applicera till alla 
                        int y1 = y + i;
                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    for (int i = 1; x - i >= 0 && y - i >= 0; i++)
                    {
                        int x1 = x - i; // För att lättare applicera till alla 
                        int y1 = y - i;

                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    break;
                case (int)pieces.rook_white:
                    for (int i = 1; x + i <= 7; i++)
                    {
                        int x1 = x + i; // För att lättare applicera till alla 
                        int y1 = y;
                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    for (int i = 1; x - i >= 0; i++)
                    {
                        int x1 = x - i; // För att lättare applicera till alla 
                        int y1 = y;
                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    for (int i = 1; y + i <= 7; i++)
                    {
                        int x1 = x; // För att lättare applicera till alla 
                        int y1 = y + i;
                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    for (int i = 1; y - i >= 0; i++)
                    {
                        int x1 = x; // För att lättare applicera till alla 
                        int y1 = y - i;

                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    break;
                case (int)pieces.queen_white:
                    for (int i = 1; x + i <= 7; i++)
                    {
                        int x1 = x + i; // För att lättare applicera till alla 
                        int y1 = y;
                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    for (int i = 1; x - i >= 0; i++)
                    {
                        int x1 = x - i; // För att lättare applicera till alla 
                        int y1 = y;
                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    for (int i = 1; y + i <= 7; i++)
                    {
                        int x1 = x; // För att lättare applicera till alla 
                        int y1 = y + i;
                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    for (int i = 1; y - i >= 0; i++)
                    {
                        int x1 = x; // För att lättare applicera till alla 
                        int y1 = y - i;

                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    for (int i = 1; x + i <= 7 && y + i <= 7; i++)
                    {
                        int x1 = x + i; // För att lättare applicera till alla 
                        int y1 = y + i;
                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    for (int i = 1; x + i <= 7 && y - i >= 0; i++)
                    {
                        int x1 = x + i; // För att lättare applicera till alla 
                        int y1 = y - i;
                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    for (int i = 1; x - i >= 0 && y + i <= 7; i++)
                    {
                        int x1 = x - i; // För att lättare applicera till alla 
                        int y1 = y + i;
                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    for (int i = 1; x - i >= 0 && y - i >= 0; i++)
                    {
                        int x1 = x - i; // För att lättare applicera till alla 
                        int y1 = y - i;

                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    break;
                case (int)pieces.king_white:
                    if (x < 7)
                    {
                        if (y < 7) if (!(board.box[x + 1][y + 1].pieceID != 0 && board.box[x][y].colour() != board.box[x + 1][y + 1].colour())) board.box[x + 1][y + 1].marked = (int)box_mark.moveable;
                        if (y > 0) if (!(board.box[x + 1][y - 1].pieceID != 0 && board.box[x][y].colour() != board.box[x + 1][y + 1].colour())) board.box[x + 1][y - 1].marked = (int)box_mark.moveable;
                        if (!(board.box[x + 1][y].pieceID != 0 && board.box[x][y].colour() != board.box[x + 1][y].colour())) board.box[x + 1][y].marked = (int)box_mark.moveable;
                    }

                    if (y < 7) if (!(board.box[x][y + 1].pieceID != 0 && board.box[x][y].colour() != board.box[x][y + 1].colour())) board.box[x][y + 1].marked = (int)box_mark.moveable;
                    if (y > 0) if (!(board.box[x][y - 1].pieceID != 0 && board.box[x][y].colour() != board.box[x][y - 1].colour())) board.box[x][y - 1].marked = (int)box_mark.moveable;

                    if (x > 0)
                    {
                        if (y < 7) if (!(board.box[x - 1][y + 1].pieceID != 0 && board.box[x][y].colour() != board.box[x - 1][y + 1].colour()))  board.box[x - 1][y + 1].marked = (int)box_mark.moveable;
                        if (y > 0) if (!(board.box[x - 1][y - 1].pieceID != 0 && board.box[x][y].colour() != board.box[x - 1][y - 1].colour())) board.box[x - 1][y - 1].marked = (int)box_mark.moveable;
                        if (!(board.box[x - 1][y].pieceID != 0 && board.box[x][y].colour() != board.box[x - 1][y].colour())) board.box[x - 1][y].marked = (int)box_mark.moveable;
                    }
                    break;
                case (int)pieces.pawn_black:
                    if (x > 0)
                    {
                        int x1 = x - 1; // För att lättare applicera till alla 
                        int y1 = y;

                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;


                        if (board.box[x][y].colour() != board.box[x1][y + 1].colour() && board.box[x1][y + 1].pieceID != (int)pieces.none)
                        {
                            board.box[x1][y + 1].marked = (int)box_mark.capture;
                            break;
                        }
                        if (board.box[x][y].colour() != board.box[x1][y - 1].colour() && board.box[x1][y - 1].pieceID != (int)pieces.none)
                        {
                            board.box[x1][y - 1].marked = (int)box_mark.capture;
                            break;
                        }
                    }

                    break;
                case (int)pieces.knight_black:
                    if (x < 6)
                    {

                        if (y < 7) if (board.box[x][y].colour() != board.box[x + 2][y + 1].colour() && board.box[x + 2][y + 1].pieceID != 0) board.box[x + 2][y + 1].marked = (int)box_mark.capture;
                            else if (board.box[x][y].colour() != board.box[x + 2][y + 1].colour())
                                board.box[x + 2][y + 1].marked = (int)box_mark.moveable;

                        if (y > 0) if (board.box[x][y].colour() != board.box[x + 2][y - 1].colour() && board.box[x + 2][y - 1].pieceID != 0) board.box[x + 2][y - 1].marked = (int)box_mark.capture;
                            else if (board.box[x][y].colour() != board.box[x + 2][y - 1].colour())
                                board.box[x + 2][y - 1].marked = (int)box_mark.moveable;

                    }
                    if (x >= 2)
                    {

                        if (y < 7) if (board.box[x][y].colour() != board.box[x - 2][y + 1].colour() && board.box[x - 2][y + 1].pieceID != 0) board.box[x - 2][y + 1].marked = (int)box_mark.capture;
                            else if (board.box[x][y].colour() != board.box[x - 2][y + 1].colour())
                                board.box[x - 2][y + 1].marked = (int)box_mark.moveable;

                        if (y > 0) if (board.box[x][y].colour() != board.box[x - 2][y - 1].colour() && board.box[x - 2][y - 1].pieceID != 0) board.box[x - 2][y - 1].marked = (int)box_mark.capture;
                            else if (board.box[x][y].colour() != board.box[x - 2][y - 1].colour())
                                board.box[x - 2][y - 1].marked = (int)box_mark.moveable;
                    }
                    if (x < 6)
                    {
                        if (y <= 5) if (board.box[x][y].colour() != board.box[x + 1][y + 2].colour() && board.box[x + 1][y + 2].pieceID != 0)
                                board.box[x + 1][y + 2].marked = (int)box_mark.capture;
                            else
                                if ((board.box[x][y].colour() != board.box[x + 1][y + 2].colour()))
                                board.box[x + 1][y + 2].marked = (int)box_mark.moveable;

                        if (y >= 2) if (board.box[x][y].colour() != board.box[x + 1][y - 2].colour() && board.box[x + 1][y - 2].pieceID != 0)
                                board.box[x + 1][y - 2].marked = (int)box_mark.capture;
                            else
                                if ((board.box[x][y].colour() != board.box[x + 1][y - 2].colour()))
                                board.box[x + 1][y - 2].marked = (int)box_mark.moveable;
                    }


                    if (x >= 2)
                    {
                        if (y < 6) if (board.box[x][y].colour() != board.box[x - 1][y + 2].colour() && board.box[x - 1][y + 2].pieceID != 0) board.box[x - 1][y + 2].marked = (int)box_mark.capture;
                            else if (board.box[x][y].colour() != board.box[x - 1][y + 2].colour())
                                board.box[x - 1][y + 2].marked = (int)box_mark.moveable;

                        if (y >= 2) if (board.box[x][y].colour() != board.box[x - 1][y - 2].colour() && board.box[x - 1][y - 2].pieceID != 0) board.box[x - 1][y - 2].marked = (int)box_mark.capture;
                            else if (board.box[x][y].colour() != board.box[x - 1][y - 2].colour())
                                board.box[x - 1][y - 2].marked = (int)box_mark.moveable;
                    }
                    break;
                case (int)pieces.bishop_black:
                    for (int i = 1; x + i <= 7 && y + i <= 7; i++)
                    {
                        int x1 = x + i; // För att lättare applicera till alla 
                        int y1 = y + i;
                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }                  
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    for (int i = 1; x + i <= 7 && y - i >= 0; i++)
                    {
                        int x1 = x + i; // För att lättare applicera till alla 
                        int y1 = y - i;
                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    for (int i = 1; x - i >= 0 && y + i <= 7; i++) {
                        int x1 = x - i; // För att lättare applicera till alla 
                        int y1 = y + i;
                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    for (int i = 1; x - i >= 0 && y - i >= 0; i++) {
                        int x1 = x - i; // För att lättare applicera till alla 
                        int y1 = y - i;

                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    break;
                case (int)pieces.rook_black:
                    for (int i = 1; x + i <= 7; i++)
                    {
                        int x1 = x + i; // För att lättare applicera till alla 
                        int y1 = y;
                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    for (int i = 1; x - i >= 0; i++)
                    {
                        int x1 = x - i; // För att lättare applicera till alla 
                        int y1 = y;
                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    for (int i = 1; y + i <= 7; i++)
                    {
                        int x1 = x; // För att lättare applicera till alla 
                        int y1 = y + i;
                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    for (int i = 1; y - i >= 0; i++)
                    {
                        int x1 = x; // För att lättare applicera till alla 
                        int y1 = y - i;

                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    break;
                case (int)pieces.queen_black:
                    for (int i = 1; x + i <= 7; i++)
                    {
                        int x1 = x + i; // För att lättare applicera till alla 
                        int y1 = y;
                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    for (int i = 1; x - i >= 0; i++)
                    {
                        int x1 = x - i; // För att lättare applicera till alla 
                        int y1 = y;
                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    for (int i = 1; y + i <= 7; i++)
                    {
                        int x1 = x; // För att lättare applicera till alla 
                        int y1 = y + i;
                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    for (int i = 1; y - i >= 0; i++)
                    {
                        int x1 = x; // För att lättare applicera till alla 
                        int y1 = y - i;

                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    for (int i = 1; x + i <= 7 && y + i <= 7; i++)
                    {
                        int x1 = x + i; // För att lättare applicera till alla 
                        int y1 = y + i;
                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    for (int i = 1; x + i <= 7 && y - i >= 0; i++)
                    {
                        int x1 = x + i; // För att lättare applicera till alla 
                        int y1 = y - i;
                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    for (int i = 1; x - i >= 0 && y + i <= 7; i++)
                    {
                        int x1 = x - i; // För att lättare applicera till alla 
                        int y1 = y + i;
                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    for (int i = 1; x - i >= 0 && y - i >= 0; i++)
                    {
                        int x1 = x - i; // För att lättare applicera till alla 
                        int y1 = y - i;

                        if (board.box[x][y].colour() != board.box[x1][y1].colour())
                            if (board.box[x1][y1].pieceID != (int)pieces.none)
                            {
                                board.box[x1][y1].marked = (int)box_mark.capture;
                                break;
                            }
                            else
                                board.box[x1][y1].marked = (int)box_mark.moveable;
                        else break;
                    }
                    break;
                case (int)pieces.king_black:
                    if (x < 7)
                    {
                        if (y < 7) if (!(board.box[x + 1][y + 1].pieceID != 0 && board.box[x][y].colour() != board.box[x + 1][y + 1].colour())) board.box[x + 1][y + 1].marked = (int)box_mark.moveable;
                        if (y > 0) if (!(board.box[x + 1][y - 1].pieceID != 0 && board.box[x][y].colour() != board.box[x + 1][y + 1].colour())) board.box[x + 1][y - 1].marked = (int)box_mark.moveable;
                        if (!(board.box[x + 1][y].pieceID != 0 && board.box[x][y].colour() != board.box[x + 1][y].colour())) board.box[x + 1][y].marked = (int)box_mark.moveable;
                    }

                    if (y < 7) if (!(board.box[x][y + 1].pieceID != 0 && board.box[x][y].colour() != board.box[x][y + 1].colour())) board.box[x][y + 1].marked = (int)box_mark.moveable;
                    if (y > 0) if (!(board.box[x][y - 1].pieceID != 0 && board.box[x][y].colour() != board.box[x][y - 1].colour())) board.box[x][y - 1].marked = (int)box_mark.moveable;

                    if (x > 0)
                    {
                        if (y < 7) if (!(board.box[x - 1][y + 1].pieceID != 0 && board.box[x][y].colour() != board.box[x - 1][y + 1].colour())) board.box[x - 1][y + 1].marked = (int)box_mark.moveable;
                        if (y > 0) if (!(board.box[x - 1][y - 1].pieceID != 0 && board.box[x][y].colour() != board.box[x - 1][y - 1].colour())) board.box[x - 1][y - 1].marked = (int)box_mark.moveable;
                        if (!(board.box[x - 1][y].pieceID != 0 && board.box[x][y].colour() != board.box[x - 1][y].colour())) board.box[x - 1][y].marked = (int)box_mark.moveable;
                    }
                    break;

                default: break;
            }
        }
        void calculateBoard(board_class board)
        {



        }

        private void mouseClick(object sender, MouseEventArgs e)
        {
            int pX = e.X;
            int pY = e.Y;

            Graphics g = this.CreateGraphics();
            SolidBrush brush = new SolidBrush(Color.Black);


            int x = (int)Math.Floor((float)pX / 100);
            int y = (int)Math.Floor((float)pY / 100);
            if (active_piece != null)
            {
                if ((board.box[x][y].pieceID == (int)pieces.none || board.box[x][y].colour() != active_piece.colour()) && sideToMove == active_piece.colour())
                {
                    if (board.box[x][y].marked == (int)box_mark.moveable || board.box[x][y].marked == (int)box_mark.capture)
                    {
                        if (sideToMove == 1) sideToMove = 2; else sideToMove = 1;
                        board.box[x][y].pieceID = active_piece.pieceID;
                        active_piece.pieceID = (int)pieces.none;
                    }
                    

                    active_piece = null;
                }
            }

            board.resetMarkings();
            board.box[x][y].marked = (int)box_mark.selected;
            active_piece = board.box[x][y];

            bUpdate = true;
        }



        private void render()
        {
            //Real mouse pos is MousePosition.X-11, MousePosition.Y-45, av någon anledning är den inte där den ska
            //SolidBrush c2 = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);
            if (bStarted)
            {
                Graphics g = CreateGraphics();

                button1.Visible = false; // start as white button
                label1.Visible = false;
                if (bUpdate)
                {
                    setMarkings();
                    calculateBoard(board);
                    drawChessBoard();
                    drawPieces();
                    bUpdate = false;
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //do whatever you want             
            render();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = System.Reflection.Assembly.GetEntryAssembly().Location;
            path = System.IO.Path.GetFullPath(Path.Combine(path, @"..\..\..\..\Pieces//CASEFONT.ttf"));
            pfc.AddFontFile(@path);
            if (pieceFont == null)
                pieceFont = new Font(pfc.Families[0], 55, FontStyle.Regular);

            TopMost = true;// funky shenanigans
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = (100); // 1/20 secs updaterar den
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }
    }
}