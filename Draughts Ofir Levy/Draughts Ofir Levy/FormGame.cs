using System;
using System.Drawing;
using System.Windows.Forms;

namespace B18_Ex05_Oron_304812712_Ofir_204219711
{
    public class FormGame : Form
    {
        private const int ButtonSize = 60;

        private readonly FormGameSettings m_GameSettings = new FormGameSettings();
        private readonly Label m_LabelPlayer1Name = new Label();
        private readonly Label m_LabelPlayer2Name = new Label();
        private readonly Label m_LabelPlayer1Score = new Label();
        private readonly Label m_LabelPlayer2Score = new Label();

        private Button[,] m_ButtonMatrix;

        private GameManager GM;
        private int[] m_Scores = new int[2];
        private int m_BoardSize;
        private bool m_IsPlayer;
        private string m_Player1Name;
        private string m_Player2Name;

        public FormGame()
        {
            
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Text = @"Damka";
        }

        private string getPlayerName(int index)
        {
            if (index == Piece.Player1)
            {
                return m_Player1Name;
            }
            else
            {
                return m_Player2Name;
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (m_GameSettings.ShowDialog() == DialogResult.OK)
            {
                InitializeParameters();
                InitializeComponent();
            }
            else
            {
                Close();
            }
        }

        private void InitializeParameters()
        {
            m_BoardSize = m_GameSettings.BoardSize;
            m_IsPlayer = m_GameSettings.Player2Checked;
            m_Player1Name = m_GameSettings.Player1Name;
            m_Player2Name = m_GameSettings.Player2Name;
        }

        private void InitializeComponent()
        {
            m_LabelPlayer1Score.Text = String.Format("{0}: {1}", m_Player1Name, m_Scores[0]);
            m_LabelPlayer1Score.Font = new Font("Arial", 12, FontStyle.Bold);
            m_LabelPlayer1Score.Location = new Point(20, 10);
            m_LabelPlayer1Score.AutoSize = true;

            m_LabelPlayer2Score.Text = String.Format("{0}: {1}", m_Player2Name, m_Scores[1]);
            m_LabelPlayer2Score.Font = new Font("Arial", 12, FontStyle.Bold);
            m_LabelPlayer2Score.Location = new Point(m_LabelPlayer1Score.Right + 40, 10);
            m_LabelPlayer2Score.AutoSize = true;

            GM = new GameManager(m_BoardSize, m_IsPlayer);
            generateButtonsMatrix(m_BoardSize);

            Size = new Size(m_BoardSize * ButtonSize + 60, m_BoardSize * ButtonSize + 100);

            Controls.AddRange(new Control[] { m_LabelPlayer1Score, m_LabelPlayer2Score });
        }

        private void generateButtonsMatrix(int i_BoardSize)
        {
            int buttonNumber = 0;
            m_ButtonMatrix = new Button[i_BoardSize, i_BoardSize];

            for (int x = 0; x < i_BoardSize; x++)
            {
                for (int y = 0; y < i_BoardSize; y++)
                {
                    m_ButtonMatrix[y, x] = new Button()
                    {
                        Width = 60,
                        Height = 60,
                        Location = new Point(y * ButtonSize + 20, x * ButtonSize + 40)
                    };

                    if ((y + x) % 2 != 0)
                    {
                        char sign = GM.Matrix[buttonNumber].Sign;
                        m_ButtonMatrix[y, x].Tag = buttonNumber++;
                        m_ButtonMatrix[y, x].Click += new EventHandler(moveFromInMatrixButton_Click);
                        setButtonsBackGround(m_ButtonMatrix[y, x], sign);
                    }
                    else
                    {
                        m_ButtonMatrix[y, x].BackColor = Color.Gray;
                        m_ButtonMatrix[y, x].Enabled = false;
                        m_ButtonMatrix[y, x].Tag = -1;
                    }

                    Controls.Add(m_ButtonMatrix[y, x]);
                }
            }
        }

        private void moveFromInMatrixButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button.BackgroundImage != null)
            {
                button.BackColor = Color.LightBlue;
                GM.MoveFromButtonClicked((int)button.Tag);
                foreach (Button mbutton in m_ButtonMatrix)
                {
                    if ((int)mbutton.Tag >= 0)
                    {
                        mbutton.Click -= new EventHandler(moveFromInMatrixButton_Click);
                        mbutton.Click += new EventHandler(moveToInMatrixButton_Click);
                    }
                }
            }
        }

        private void moveToInMatrixButton_Click(object sender, EventArgs e)
        {
            GameManager.eCodeForMessage codeForMsg=GameManager.eCodeForMessage.NoMsg;
             Button button = sender as Button;

            if (button.BackgroundImage == null)
            {
                GM.MoveToButtonClicked((int)button.Tag);
                codeForMsg = GM.Move();
            }

            foreach (Button mbutton in m_ButtonMatrix)
            {
                if ((int)mbutton.Tag >= 0)
                {
                    char sign = GM.Matrix[(int)mbutton.Tag].Sign;
                    mbutton.Click -= new EventHandler(moveToInMatrixButton_Click);
                    mbutton.Click += new EventHandler(moveFromInMatrixButton_Click);
                    setButtonsBackGround(mbutton, sign);
                }
            }

            checkMsgCode(codeForMsg);
        }

        private void checkMsgCode(GameManager.eCodeForMessage i_CodeForMsg)
        {
            if (i_CodeForMsg == GameManager.eCodeForMessage.MustCapture || i_CodeForMsg == GameManager.eCodeForMessage.InvalidMove)
            {
                invalidMove(i_CodeForMsg);
            }
            else if (i_CodeForMsg == GameManager.eCodeForMessage.Player1Won || i_CodeForMsg == GameManager.eCodeForMessage.Player2Won || i_CodeForMsg == GameManager.eCodeForMessage.Tie)
            {
                gameOver(i_CodeForMsg);
            }
        }

        private void resetButtonsMatrix()
        {
            GM = new GameManager(m_BoardSize, m_IsPlayer);
            foreach (Button mbutton in m_ButtonMatrix)
            {
                if ((int)mbutton.Tag >= 0)
                {
                    char sign = GM.Matrix[(int)mbutton.Tag].Sign;
                    mbutton.Click -= new EventHandler(moveFromInMatrixButton_Click);
                    mbutton.Click += new EventHandler(moveToInMatrixButton_Click);
                    setButtonsBackGround(mbutton, sign);
                }
            }
        }

        private void setButtonsBackGround(Button i_Button, char i_Sign)
        {
            i_Button.BackColor = default(Color);
            if (i_Sign == 'X')
            {
                i_Button.BackgroundImage = Properties.Resources.RB;
                i_Button.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else if (i_Sign == 'O')
            {
                i_Button.BackgroundImage = Properties.Resources.RW;
                i_Button.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else if (i_Sign == 'K')
            {
                i_Button.BackgroundImage = Properties.Resources.KB;
                i_Button.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else if (i_Sign == 'U')
            {
                i_Button.BackgroundImage = Properties.Resources.KW;
                i_Button.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else
            {
                i_Button.BackgroundImage = null;
            }

        }

        private void invalidMove(GameManager.eCodeForMessage i_CodeForMsg)
        {
            string errorMsg = "";

            if (i_CodeForMsg == GameManager.eCodeForMessage.MustCapture)
            {
                errorMsg = String.Format("{0}: You Must Capture", getPlayerName(GM.Turn));
            }
            else if (i_CodeForMsg == GameManager.eCodeForMessage.InvalidMove)
            {
                errorMsg = String.Format("{0}: Invalid Move", getPlayerName(GM.Turn));
            }

            if (MessageBox.Show(errorMsg, @"Invalid Move", MessageBoxButtons.OK) == DialogResult.OK)
            {

            }
        }

        private void gameOver(GameManager.eCodeForMessage i_CodeForMsg)
        {
            string errorMsg = "";

            if (i_CodeForMsg == GameManager.eCodeForMessage.Player1Won)
            {
                errorMsg = string.Format("{0} Won! Another Round?",getPlayerName(Piece.Player1));
                m_Scores[0]++;
            }
            else if (i_CodeForMsg == GameManager.eCodeForMessage.Player2Won)
            {
                errorMsg = string.Format("{0} Won! Another Round?", getPlayerName(Piece.Player2));
                m_Scores[1]++;
            }
            else if (i_CodeForMsg == GameManager.eCodeForMessage.Tie)
            {
                errorMsg = @"Tie! /n Another Round?";
                m_Scores[0]++;
                m_Scores[1]++;
            }

            if (MessageBox.Show(errorMsg, @"Damka", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                m_LabelPlayer1Score.Text = String.Format("{0}: {1}", m_Player1Name, m_Scores[0]);
                m_LabelPlayer2Score.Text = String.Format("{0}: {1}", m_Player2Name, m_Scores[1]);
                resetButtonsMatrix();
            }
            else
            {
                Close();
            }
        }
    }
}
