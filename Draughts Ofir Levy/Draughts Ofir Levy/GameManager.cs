using System;
using System.Collections.Generic;

namespace B18_Ex05_Oron_304812712_Ofir_204219711
{
    public class GameManager
    {

        private const int DontHaveMoves = -2;
        private const int DidntCaptured = -1;
        private const int Default = -1;

        private Player[] m_Players = new Player[2];
        private Piece[] m_Matrix;
        private BoardMatrix m_BoardMatrixObject;
        private bool m_GameOver;
        private readonly int m_BoardSize;
        private readonly bool m_IsPlayer;
        private int m_MoveFromClicked;
        private int m_MoveToClicked;
        private int m_Turn;
        private eCodeForMessage m_Msg;

        List<AvailableObject> AvailableMovesArray = new List<AvailableObject>();
        List<AvailableObject> AvailableCapturesArray = new List<AvailableObject>();
        List<AvailableObject> AvailableMoveForSingle = new List<AvailableObject>();
        List<AvailableObject> AvailableCaputreForSingle = new List<AvailableObject>();

        public Piece[] Matrix
        {
            get { return m_Matrix; }
        }

        public int Turn
        {
            get { return m_Turn; }
        }

        public GameManager(int i_BoardSize, bool i_IsPlayer)
        {
            m_BoardSize = i_BoardSize;
            m_IsPlayer = i_IsPlayer;
            setNewGame();
        }

        private void setNewGame()
        {
            m_GameOver = false;
            m_Msg = eCodeForMessage.NoMsg;
            SetPlayers();
            m_BoardMatrixObject = new BoardMatrix(m_BoardSize, ref m_Players, ref m_Matrix);
        }

        private void SetPlayers()
        {
            m_Players[0] = new Player(Piece.Player1, m_BoardSize, true);
            m_Players[1] = new Player(Piece.Player2, m_BoardSize, m_IsPlayer);
        }

        public void MoveFromButtonClicked(int i_Index)
        {
            m_MoveFromClicked = i_Index;
        }

        public void MoveToButtonClicked(int i_Index)
        {
            m_MoveToClicked = i_Index;
        }

        public eCodeForMessage Move()
        {
            if (!m_GameOver)
            {
                m_Msg = eCodeForMessage.NoMsg;
                createMoves(m_Turn);

                if (m_MoveFromClicked >= 0 && m_MoveToClicked >= 0)
                {
                    if (AvailableCapturesArray.Count > 0)
                    {
                        captureMove();
                    }
                    else
                    {
                        regularMove();
                    }
                }
            }

            return m_Msg;
        }

        private void captureMove()
        {
            for (int i = 0; i < AvailableCapturesArray.Count; i++)
            {
                if (m_MoveFromClicked == AvailableCapturesArray[i].GetIndexInMatrix())
                {
                    if (m_MoveToClicked == AvailableCapturesArray[i].indexMoveTo)
                    {
                        doThisMove(i);
                        if (m_Msg == eCodeForMessage.InvalidMove || m_Msg == eCodeForMessage.MustCapture)
                        {
                            m_Msg = eCodeForMessage.NoMsg;
                        }
                    }
                    else
                    {
                        m_Msg = eCodeForMessage.MustCapture;
                    }
                }
                else
                {
                    m_Msg = eCodeForMessage.MustCapture;
                }
            }
        }

        private void regularMove()
        {
            for (int i = 0; i < AvailableMovesArray.Count; i++)
            {
                if (m_MoveFromClicked == AvailableMovesArray[i].GetIndexInMatrix())
                {
                    if (m_MoveToClicked == AvailableMovesArray[i].indexMoveTo)
                    {
                        doThisMove(i);
                        if (m_Msg == eCodeForMessage.InvalidMove || m_Msg == eCodeForMessage.MustCapture)
                        {
                            m_Msg = eCodeForMessage.NoMsg;
                        }
                    }
                    else
                    {
                        m_Msg = eCodeForMessage.InvalidMove;
                    }
                }
                else
                {
                    m_Msg = eCodeForMessage.InvalidMove;
                }
            }
        }

        private void doThisMove(int i_Index)
        {
            chooseMove(m_Turn, i_Index);
            m_Turn = (m_Turn + 1) % 2;
            m_MoveFromClicked = Default;
            m_MoveToClicked = Default;
            if (!m_Players[m_Turn].IsPlayer)
            {
                Move();
            }
        }

        private void createMoves(int i_IndexOfPlayer)
        {
            AvailableMovesArray.Clear();
            AvailableCapturesArray.Clear();
            creatAvailiableMoves(ref m_Matrix, i_IndexOfPlayer);
            if (!m_Players[i_IndexOfPlayer].IsPlayer && !m_GameOver)
            {
                int randomPick;
                if (AvailableCapturesArray.Count > 0)
                {
                    randomPick = new Random().Next(0, AvailableCapturesArray.Count - 1);
                    chooseMove(i_IndexOfPlayer, randomPick);
                    m_Turn = (m_Turn + 1) % 2;
                }
                else
                {
                    if (AvailableMovesArray.Count == 0)
                    {
                        m_Msg = eCodeForMessage.Tie;
                    }
                    else
                    {
                        randomPick = new Random().Next(0, AvailableMovesArray.Count - 1);
                        chooseMove(i_IndexOfPlayer, randomPick);
                        m_Turn = (m_Turn + 1) % 2;
                    }
                }
            }
        }

        private void chooseMove(int i_IndexOfPlayer, int i_InputMoveTo)
        {
            checkSituationOfGame(i_IndexOfPlayer, i_InputMoveTo);
        }

        private void checkSituationOfGame(int i_IndexOfPlayer, int i_InputMoveTo)
        {
            int situationOfGame = compareNextMoveInputToPossiableMoves(i_IndexOfPlayer, i_InputMoveTo, false);

            if (situationOfGame == DontHaveMoves)
            {
                m_GameOver = true;
                if (checkIfHavePossiableMovesAndCaptures(ref m_Matrix, (i_IndexOfPlayer + 1) % 2)) //check if other player is out of moves
                {
                    GameOver(eWinDraw.Won, i_IndexOfPlayer);
                }
                else
                {
                    GameOver(eWinDraw.Tie, i_IndexOfPlayer);
                }
            }

            while (situationOfGame >= 0 && !m_GameOver)
            {
                if (m_Players[(i_IndexOfPlayer + 1) % 2].NotLeftPieces())
                {
                    m_GameOver = true;
                    GameOver(eWinDraw.Won, i_IndexOfPlayer);
                    break;
                }
                AvailableMovesArray.Clear();
                AvailableCapturesArray.Clear();
                availableMove(m_Players[i_IndexOfPlayer].GetListMensPieceByIndexInMatrix(situationOfGame), ref m_Matrix);

                if (AvailableCapturesArray.Count > 0)
                {
                    if (!m_Players[i_IndexOfPlayer].IsPlayer)
                    {
                        i_InputMoveTo = new Random().Next(0, AvailableCapturesArray.Count - 1);
                    }
                    else
                    {
                        // player need to choose capure again option
                    }

                    situationOfGame = compareNextMoveInputToPossiableMoves(i_IndexOfPlayer, i_InputMoveTo, true);
                }
                else
                {
                    situationOfGame = DidntCaptured;
                }
            }
        }

        private int compareNextMoveInputToPossiableMoves(int i_IndexOfPlayer, int i_InputMoveTo, bool i_AlreadyCaptured)
        {
            int indexOfMenInTheMensPieceToCheckIfWillCaptureAgain = DidntCaptured;

            if (AvailableCapturesArray.Count > 0) //can capture
            {
                int indexInMatrixJustLeft = AvailableCapturesArray[i_InputMoveTo].GetIndexInMatrix(); //save position of index in matrix
                m_Players[(i_IndexOfPlayer + 1) % 2].DeletePieceInMensPieces(m_Matrix[AvailableCapturesArray[i_InputMoveTo].indexInMatrixToDelete].IndexInMensPlaces, ref m_Matrix); //delete the piece captured from the oponenet list
                Piece pieceToUpdateMatrix = m_Players[i_IndexOfPlayer].ChangeValues(AvailableCapturesArray[i_InputMoveTo], ref m_BoardMatrixObject, ref m_Matrix); //changes value in men list pieces 
                deletePieceInTheMatrix(ref m_Matrix, AvailableCapturesArray[i_InputMoveTo].indexInMatrixToDelete); //delete the matrix the piece captured
                indexOfMenInTheMensPieceToCheckIfWillCaptureAgain = AvailableCapturesArray[i_InputMoveTo].indexMoveTo; //save the new index in matrix to check if can capture again
                m_Matrix[AvailableCapturesArray[i_InputMoveTo].indexMoveTo] = pieceToUpdateMatrix;
                deletePieceInTheMatrix(ref m_Matrix, indexInMatrixJustLeft); //delete piece from matrix

            }
            else if (AvailableMovesArray.Count > 0 && AvailableCapturesArray.Count == 0 && i_AlreadyCaptured == false) //can move
            {
                int indexInMatrixJustLeft = AvailableMovesArray[i_InputMoveTo].GetIndexInMatrix();
                Piece pieceToUpdateMatrix = m_Players[i_IndexOfPlayer].ChangeValues(AvailableMovesArray[i_InputMoveTo], ref m_BoardMatrixObject, ref m_Matrix);
                m_Matrix[AvailableMovesArray[i_InputMoveTo].indexMoveTo] = pieceToUpdateMatrix;
                deletePieceInTheMatrix(ref m_Matrix, indexInMatrixJustLeft);
            }
            else // can't move or capture
            {
                indexOfMenInTheMensPieceToCheckIfWillCaptureAgain = DontHaveMoves;
            }

            AvailableMovesArray.Clear();
            AvailableCapturesArray.Clear();

            return indexOfMenInTheMensPieceToCheckIfWillCaptureAgain;
        }

        private void creatAvailiableMoves(ref Piece[] io_Matrix, int i_IndexOfPlayer) //run on all the mens of the player and insert all the possiables that they can move
        {
            int playerMensCount = m_Players[i_IndexOfPlayer].GetSizeOfMensPlaces();

            for (int i = 0; i < playerMensCount; i++) //check if the men have a possability to move' if so the move go in a arr of all the possabilitis
            {
                availableMove(m_Players[i_IndexOfPlayer].GetListMensPiece(i), ref io_Matrix);
            }
        }

        private void availableMove(Piece io_Piece, ref Piece[] io_Matrix)
        {
            AvailableMoveForSingle.Clear();
            AvailableCaputreForSingle.Clear();
            if (io_Piece.Sign == Piece.Player1Men) //in case this is men that go up
            {
                m_BoardMatrixObject.CheckAvailableGoUp(ref io_Piece, ref io_Matrix, ref AvailableMoveForSingle,
                    ref AvailableCaputreForSingle);
            }
            else if (io_Piece.Sign == Piece.Player2Men) //in case this is men that go down
            {
                m_BoardMatrixObject.CheckAvailableGoDown(ref io_Piece, ref io_Matrix, ref AvailableMoveForSingle,
                    ref AvailableCaputreForSingle);
            }
            else //in case this is a king of any kind
            {
                m_BoardMatrixObject.CheckAvailableGoDown(ref io_Piece, ref io_Matrix, ref AvailableMoveForSingle,
                    ref AvailableCaputreForSingle);
                m_BoardMatrixObject.CheckAvailableGoUp(ref io_Piece, ref io_Matrix, ref AvailableMoveForSingle,
                    ref AvailableCaputreForSingle);
            }

            if (AvailableMoveForSingle.Count > 0)
            {
                AvailableMovesArray.AddRange(AvailableMoveForSingle);
            }

            if (AvailableCaputreForSingle.Count > 0)
            {
                AvailableCapturesArray.AddRange(AvailableCaputreForSingle);
            }
        }

        private bool checkIfHavePossiableMovesAndCaptures(ref Piece[] io_Matrix, int i_IndexOfPlayer)
        {
            creatAvailiableMoves(ref io_Matrix, i_IndexOfPlayer);
            return (AvailableCapturesArray.Count == 0 && AvailableMovesArray.Count == 0);
        }

        private void deletePieceInTheMatrix(ref Piece[] io_matrix, int indexInAcctualMatrix)
        {
            Piece piece = new Piece(Default, Default, Default);
            piece.setAsDeleted(indexInAcctualMatrix);
            io_matrix[indexInAcctualMatrix] = piece;
        }

        private void GameOver(eWinDraw i_Situation, int i_IndexOfPlayer)
        {
            if (i_Situation == eWinDraw.Won)
            {
                if (i_IndexOfPlayer == 0)
                {
                    m_Msg = eCodeForMessage.Player1Won;
                }
                else
                {
                    m_Msg = eCodeForMessage.Player2Won;
                }
            }

            else if (i_Situation == eWinDraw.Tie)
            {
                m_Msg = eCodeForMessage.Tie;
            }
        }

        private enum eWinDraw
        {
            Won = 1,
            Tie = 2
        }

        public enum eCodeForMessage
        {
            NoMsg = 0,
            Player1Won = 1,
            Player2Won = 2,
            Tie = 3,
            MustCapture = 4,
            InvalidMove = 5
        }
    }
}