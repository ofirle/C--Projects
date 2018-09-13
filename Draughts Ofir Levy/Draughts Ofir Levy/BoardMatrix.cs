using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B18_Ex05_Oron_304812712_Ofir_204219711
{
    public class BoardMatrix
    {
        private readonly int m_SizeOfBoard;

        public BoardMatrix(int i_SizeOfBoard, ref Player[] Players, ref Piece[] io_Matrix)
        {
            m_SizeOfBoard = i_SizeOfBoard;
            io_Matrix = new Piece[(m_SizeOfBoard * m_SizeOfBoard) / 2];
            setMatrixByPlayers(ref io_Matrix, ref Players);
        }

        private void setMatrixByPlayers(ref Piece[] io_Matrix, ref Player[] io_Players)
        {
            setMatrixToZero(ref io_Matrix);
            int numOfMens = (m_SizeOfBoard / 2 - 1) * m_SizeOfBoard / 2;
            for (int k = 0; k < 2; k++)
            {
                for (int i = 0; i < numOfMens; i++)
                {
                    io_Matrix[io_Players[k].GetIndexInMatrix(i)] = io_Players[k].GetListMensPiece(i);
                }
            }
        }

        private void setMatrixToZero(ref Piece[] io_Matrix)
        {
            int numOfPieces = m_SizeOfBoard * m_SizeOfBoard / 2;

            for (int i = 0; i < numOfPieces; i++)
            {
                Piece piece = new Piece(-1, i, -1);
                io_Matrix[i] = piece;
            }
        }

        public void CheckAvailableGoDown(ref Piece io_Piece, ref Piece[] io_Matrix, ref List<AvailableObject> io_AvailableMoveForSingle, ref List<AvailableObject> io_AvailableCaputreForSingle)
        {
            int sizeOfBoardArr = m_SizeOfBoard * m_SizeOfBoard / 2;
            int RightDownLineisEven = io_Piece.IndexInMatrix + m_SizeOfBoard / 2;
            int LeftDownLineisEven = io_Piece.IndexInMatrix + (m_SizeOfBoard / 2) - 1;
            int RightDownLineisOdd = io_Piece.IndexInMatrix + m_SizeOfBoard / 2 + 1;
            int LeftDownLineisOdd = io_Piece.IndexInMatrix + (m_SizeOfBoard / 2);
            int RightDownCaptureOpponentLineisEven = RightDownLineisOdd + m_SizeOfBoard / 2;
            int LeftDownCaptureOpponentLineisEven = LeftDownLineisOdd + (m_SizeOfBoard / 2) - 1;
            int RightDownCaptureOpponentLineisOdd = RightDownLineisEven + m_SizeOfBoard / 2 + 1;
            int LeftDownCaptureOpponentLineisOdd = LeftDownLineisEven + (m_SizeOfBoard / 2);
            bool edgeRight = io_Piece.IndexInMatrix % m_SizeOfBoard == (m_SizeOfBoard / 2) - 1;
            bool edgeLeft = io_Piece.IndexInMatrix % m_SizeOfBoard == (m_SizeOfBoard / 2);
            bool i_NextStepIsEdgeRight = RightDownLineisEven % m_SizeOfBoard == (m_SizeOfBoard / 2) - 1;
            bool i_NextStepIsEdgeLeft = LeftDownLineisOdd % m_SizeOfBoard == (m_SizeOfBoard / 2);

            if (io_Piece.IndexInMatrix < sizeOfBoardArr - m_SizeOfBoard / 2) // if not in the last line and go down
            {
                if (((io_Piece.IndexInMatrix / (m_SizeOfBoard / 2) + 1) % 2) == 0) // if line is even
                {
                    checkIfNextStepIsFree(ref io_Piece, RightDownLineisEven, RightDownCaptureOpponentLineisEven, i_NextStepIsEdgeRight, ref io_Matrix, ref io_AvailableMoveForSingle, ref io_AvailableCaputreForSingle);
                    if (!edgeLeft) // if i not in the left edge
                    {
                        checkIfNextStepIsFree(ref io_Piece, LeftDownLineisEven, LeftDownCaptureOpponentLineisEven, false, ref io_Matrix, ref io_AvailableMoveForSingle, ref io_AvailableCaputreForSingle);
                    }
                }
                else // if line is odd
                {
                    checkIfNextStepIsFree(ref io_Piece, LeftDownLineisOdd, LeftDownCaptureOpponentLineisOdd, i_NextStepIsEdgeLeft, ref io_Matrix, ref io_AvailableMoveForSingle, ref io_AvailableCaputreForSingle);
                    if (!edgeRight) //if i not in the right edge
                    {
                        checkIfNextStepIsFree(ref io_Piece, RightDownLineisOdd, RightDownCaptureOpponentLineisOdd, false, ref io_Matrix, ref io_AvailableMoveForSingle, ref io_AvailableCaputreForSingle);
                    }
                }
            }
        }

        public void CheckAvailableGoUp(ref Piece io_Piece, ref Piece[] io_Matrix, ref List<AvailableObject> io_AvailableMoveForSingle, ref List<AvailableObject> io_AvailableCaputreForSingle)
        {
            int RightUpLineisEven = io_Piece.IndexInMatrix - m_SizeOfBoard / 2;
            int LeftUpLineisEven = io_Piece.IndexInMatrix - (m_SizeOfBoard / 2) - 1;
            int RightUpLineisOdd = io_Piece.IndexInMatrix - (m_SizeOfBoard / 2) + 1;
            int LeftUpLineisOdd = io_Piece.IndexInMatrix - m_SizeOfBoard / 2;
            int RightUpCaptureOpponentLineisEven = RightUpLineisOdd - m_SizeOfBoard / 2;
            int LeftUpCaptureOpponentLineisEven = LeftUpLineisOdd - (m_SizeOfBoard / 2) - 1;
            int RightUpCaptureOpponentLineisOdd = RightUpLineisEven - m_SizeOfBoard / 2 + 1;
            int LeftUpCaptureOpponentLineisOdd = LeftUpLineisEven - (m_SizeOfBoard / 2);
            bool edgeRight = io_Piece.IndexInMatrix % m_SizeOfBoard == (m_SizeOfBoard / 2) - 1;
            bool edgeLeft = io_Piece.IndexInMatrix % m_SizeOfBoard == (m_SizeOfBoard / 2);
            bool i_NextStepIsEdgeRight = RightUpLineisEven % m_SizeOfBoard == (m_SizeOfBoard / 2) - 1;
            bool i_NextStepIsEdgeLeft = LeftUpLineisOdd % m_SizeOfBoard == (m_SizeOfBoard / 2);

            if (io_Piece.IndexInMatrix >= m_SizeOfBoard / 2) //if not in the first line and go up
            {
                if (((io_Piece.IndexInMatrix / (m_SizeOfBoard / 2) + 1) % 2) == 0) //if line is even
                {
                    checkIfNextStepIsFree(ref io_Piece, RightUpLineisEven, RightUpCaptureOpponentLineisEven, i_NextStepIsEdgeRight, ref io_Matrix, ref io_AvailableMoveForSingle, ref io_AvailableCaputreForSingle);
                    if (!edgeLeft) //if i not in the left edge
                    {
                        checkIfNextStepIsFree(ref io_Piece, LeftUpLineisEven, LeftUpCaptureOpponentLineisEven, false, ref io_Matrix, ref io_AvailableMoveForSingle, ref io_AvailableCaputreForSingle);
                    }
                }
                else //if line is odd
                {
                    checkIfNextStepIsFree(ref io_Piece, LeftUpLineisOdd, LeftUpCaptureOpponentLineisOdd, i_NextStepIsEdgeLeft, ref io_Matrix, ref io_AvailableMoveForSingle, ref io_AvailableCaputreForSingle);
                    if (!edgeRight) //if i not in the right edge
                    {
                        checkIfNextStepIsFree(ref io_Piece, RightUpLineisOdd, RightUpCaptureOpponentLineisOdd, false, ref io_Matrix, ref io_AvailableMoveForSingle, ref io_AvailableCaputreForSingle);
                    }
                }
            }
        }

        public char IsKingOrMen(AvailableObject i_AvailableObject, ref Piece[] io_Matrix)
        {
            char sign;

            if (GotToTheEnd(i_AvailableObject))
            {
                sign = i_AvailableObject.GetKingSignOfPiece();
            }
            else
            {
                sign = io_Matrix[i_AvailableObject.GetIndexInMatrix()].Sign;
            }

            return sign;
        }

        public bool GotToTheEnd(AvailableObject i_AvailableObject) //check if men got to the end to become a king
        {
            bool checkifKing;

            if ((i_AvailableObject.GetSign() == 'X' && i_AvailableObject.indexMoveTo < m_SizeOfBoard / 2) || (i_AvailableObject.GetSign() == 'O' && i_AvailableObject.indexMoveTo >= m_SizeOfBoard * m_SizeOfBoard / 2 - m_SizeOfBoard / 2))
            {
                checkifKing = true;
            }
            else
            {
                checkifKing = false;
            }

            return checkifKing;
        }

        private void checkIfNextStepIsFree(ref Piece io_Piece, int i_MoveToInTheMatrix, int i_MoveToInTheMatrixCapture, bool i_NextStepIsEdge, ref Piece[] io_Matrix, ref List<AvailableObject> io_AvailableMoveForSingle, ref List<AvailableObject> io_AvailableCaputreForSingle)
        {
            int sizeOfBoardArr = m_SizeOfBoard * m_SizeOfBoard / 2;

            if (checkIfMoveIsFree(ref io_Matrix, i_MoveToInTheMatrix)) //in case of move
            {
                insertToArrOfOptions(ref io_AvailableMoveForSingle, ref io_Piece, -1, i_MoveToInTheMatrix);
            }
            else if (i_MoveToInTheMatrixCapture >= 0 && i_MoveToInTheMatrixCapture < sizeOfBoardArr && !i_NextStepIsEdge)
            {
                if (checkIfCaptureIsFree(ref io_Matrix, ref io_Piece, i_MoveToInTheMatrix, i_MoveToInTheMatrixCapture))
                {
                    insertToArrOfOptions(ref io_AvailableCaputreForSingle, ref io_Piece, i_MoveToInTheMatrix, i_MoveToInTheMatrixCapture);
                }
            }
        }

        private void insertToArrOfOptions(ref List<AvailableObject> io_List, ref Piece io_Piece, int io_IndexInMatrixToDelete, int i_MoveToInTheMatrix)
        {
            io_List.Add(new AvailableObject(io_Piece, io_IndexInMatrixToDelete, i_MoveToInTheMatrix));
        }

        private bool checkIfMoveIsFree(ref Piece[] io_Matrix, int i_MoveToInTheMatrix)
        {
            return (io_Matrix[i_MoveToInTheMatrix].IsEmpty);
        }

        private bool checkIfCaptureIsFree(ref Piece[] io_Matrix, ref Piece io_Piece, int i_MoveToInTheMatrix, int i_MoveToInTheMatrixCapture)
        {
            bool isFree;
            if (!io_Matrix[i_MoveToInTheMatrix].IsEmpty && (io_Matrix[i_MoveToInTheMatrix].Sign != io_Piece.GetSignMen()) && (io_Matrix[i_MoveToInTheMatrix].Sign != io_Piece.GetSignKing()) && io_Matrix[i_MoveToInTheMatrixCapture].IsEmpty)
            {
                isFree = true;
            }
            else
            {
                isFree = false;
            }

            return isFree;
        }
    }
}

