using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B18_Ex05_Oron_304812712_Ofir_204219711
{
    public class Player
    {
        private readonly bool m_IsPlayer;
        private readonly int m_IndexOfPlayer;

        private List<Piece> MensPieces = new List<Piece>();

        public Player(int i_IndexOfPlayer, int i_SizeOfBoard, bool i_IsPlayer) // ctor
        {
            m_IsPlayer = i_IsPlayer;
            m_IndexOfPlayer = i_IndexOfPlayer;
            setPlayerMensPlaces(m_IndexOfPlayer, i_SizeOfBoard);

        }

        public bool IsPlayer
        {
            get { return m_IsPlayer; }
        }

        public int GetSizeOfMensPlaces()
        {
            return MensPieces.Count;
        }

        public int GetIndexInMatrix(int index)
        {
            return MensPieces[index].IndexInMatrix;
        }

        public Piece GetListMensPiece(int index)
        {
            return MensPieces[index];
        }

        public Piece GetListMensPieceByIndexInMatrix(int i_Index)
        {
            Piece piece = null;
            for (int i = 0; i < MensPieces.Count; i++)
            {
                if (MensPieces[i].IndexInMatrix == i_Index)
                {
                    piece = MensPieces[i];
                }
            }

            return piece;
        }

        private void setPlayerMensPlaces(int i_IndexOfPlayer, int i_SizeOfBoard)
        {
            int startMen = 0;
            int sizeOfBoardArr = i_SizeOfBoard * i_SizeOfBoard / 2;
            int numOfMensAndKings = (i_SizeOfBoard * i_SizeOfBoard / 4) - i_SizeOfBoard / 2;

            if (i_IndexOfPlayer == Piece.Player1)
            {
                startMen = sizeOfBoardArr - numOfMensAndKings;
            }

            for (int i = 0; i < numOfMensAndKings; i++)
            {
                MensPieces.Add(new Piece(i_IndexOfPlayer, startMen + i, i));
            }
        }

        internal void DeletePieceInMensPieces(int indexInMatrixToDelete, ref Piece[] io_matrix)
        {
            for (int i = indexInMatrixToDelete + 1; i < MensPieces.Count; i++)
            {
                io_matrix[MensPieces[i].IndexInMatrix].IndexInMensPlaces--;
            }

            MensPieces.RemoveAt(indexInMatrixToDelete);
        }

        internal Piece ChangeValues(AvailableObject i_AvailableObject, ref BoardMatrix io_BoardMatrixObject, ref Piece[] io_Matrix)
        {
            char sign = io_BoardMatrixObject.IsKingOrMen(i_AvailableObject, ref io_Matrix);

            if (io_BoardMatrixObject.GotToTheEnd(i_AvailableObject))
            {
                MensPieces[i_AvailableObject.GetIndexInMensPlaces()].IsKing = true;
                io_Matrix[MensPieces[i_AvailableObject.GetIndexInMensPlaces()].IndexInMatrix].IsKing = true;
            }

            MensPieces[i_AvailableObject.GetIndexInMensPlaces()].IndexInMatrix = i_AvailableObject.indexMoveTo;
            MensPieces[i_AvailableObject.GetIndexInMensPlaces()].Sign = sign;

            return MensPieces[i_AvailableObject.GetIndexInMensPlaces()];
        }

        internal void SetSignInMenPiecesByIndex(char i_Sign, int i_Index)
        {
            MensPieces[i_Index].Sign = i_Sign;
        }

        internal void UpdateMenPieces(AvailableObject i_AvailableObject)
        {
            MensPieces[i_AvailableObject.GetIndexInMatrix()].IndexInMatrix = i_AvailableObject.indexMoveTo;
        }

        internal bool NotLeftPieces()
        {
            bool isEmpty = MensPieces.Count == 0;

            return isEmpty;
        }
    }
}
