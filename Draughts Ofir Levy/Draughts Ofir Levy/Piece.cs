using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B18_Ex05_Oron_304812712_Ofir_204219711
{
    public class Piece
    {
        public const int Player1 = 0;
        public const int Player2 = 1;

        public const char Player1Men = 'X';
        public const char Player2Men = 'O';
        public const char Player1King = 'K';
        public const char Player2King = 'U';
        public const char Space = ' ';


        private int m_IndexInMatrix;
        private int m_IndexInMensPlaces;
        private int m_IndexOfPlayer;
        private char m_Sign;
        private bool m_IsKing;
        private bool m_IsEmpty;


        public Piece(int i_IndexOfPlayer, int i_IndexInMatrix, int i_IndexInMensPlaces)
        {
            m_IndexOfPlayer = i_IndexOfPlayer;
            m_IndexInMatrix = i_IndexInMatrix;
            m_IndexInMensPlaces = i_IndexInMensPlaces;

            if (m_IndexOfPlayer == Player1)
            {
                m_Sign = Player1Men;
                m_IsEmpty = false;
            }
            else if (m_IndexOfPlayer == Player2)
            {
                m_Sign = Player2Men;
                m_IsEmpty = false;
            }
            else
            {
                m_Sign = Space;
                m_IsEmpty = true;
            }

            m_IsKing = false;
        }

        public bool IsKing
        {
            get { return m_IsKing; }
            set { m_IsKing = value; }
        }

        public bool IsEmpty
        {
            get { return m_IsEmpty; }
            set { m_IsEmpty = value; }
        }

        public int IndexInMatrix
        {
            get { return m_IndexInMatrix; }
            set { m_IndexInMatrix = value; }
        }

        public int IndexInMensPlaces
        {
            get { return m_IndexInMensPlaces; }
            set { m_IndexInMensPlaces = value; }
        }

        public int IndexOfPlayer
        {
            get { return m_IndexOfPlayer; }
            set { m_IndexOfPlayer = value; }
        }

        public char Sign
        {
            get { return m_Sign; }
            set { m_Sign = value; }
        }

        public char GetSignMen()
        {
            char signMen = Space;
            if (IsKing)
            {
                if (m_Sign == Player1King)
                {
                    signMen = Player1Men;
                }
                else
                {
                    signMen = Player2Men;
                }
            }
            else
            {
                if (m_Sign == Player1Men)
                {
                    signMen = Player1Men;
                }
                else
                {
                    signMen = Player2Men;
                }
            }

            return signMen;
        }

        public char GetSignKing()
        {
            char signMen = Space;
            if (IsKing)
            {
                if (m_Sign == Player1King)
                {
                    signMen = Player1King;
                }
                else
                {
                    signMen = Player2King;
                }
            }
            else
            {
                if (m_Sign == Player1Men)
                {
                    signMen = Player1King;
                }
                else
                {
                    signMen = Player2King;
                }
            }

            return signMen;
        }

        public void setAsDeleted(int i_Index)
        {
            m_IndexInMensPlaces = -1;
            m_IndexOfPlayer = -1;
            m_IndexInMatrix = i_Index;
            m_Sign = ' ';
            m_IsKing = false;
            m_IsEmpty = true;
        }

        public override string ToString()
        {
            return string.Format("{0}", m_Sign);
        }
    }
}