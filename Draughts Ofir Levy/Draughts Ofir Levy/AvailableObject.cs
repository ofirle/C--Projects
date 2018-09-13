using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B18_Ex05_Oron_304812712_Ofir_204219711
{
    public class AvailableObject
    {
        private int m_IndexMoveTo;
        private int m_IndexInMatrixToDelete;
        private readonly Piece m_Piece;

        public int indexMoveTo
        {
            get { return m_IndexMoveTo; }
            set { m_IndexMoveTo = value; }
        }

        public int indexInMatrixToDelete
        {
            get { return m_IndexInMatrixToDelete; }
            set { m_IndexInMatrixToDelete = value; }
        }

        public int GetIndexInMatrix()
        {
            return m_Piece.IndexInMatrix;
        }

        public int GetIndexInMensPlaces()
        {
            return m_Piece.IndexInMensPlaces;
        }

        public char GetSign()
        {
            return m_Piece.Sign;
        }

        public char GetKingSignOfPiece()
        {
            return m_Piece.GetSignKing();
        }

        public AvailableObject(Piece i_Piece, int i_IndexInMatrixToDelete, int i_IndexMoveTo)
        {
            m_Piece = i_Piece;
            m_IndexInMatrixToDelete = i_IndexInMatrixToDelete;
            m_IndexMoveTo = i_IndexMoveTo;
        }
    }
}
