using System;
using System.Drawing;
using System.Windows.Forms;

namespace B18_Ex05_Oron_304812712_Ofir_204219711
{
    public class FormGameSettings : Form
    {
        private readonly Label m_LabelBoardSize = new Label();
        private readonly Label m_LabelPlayers = new Label();
        private readonly Label m_LabelPlayer1 = new Label();
        private readonly Label m_LabelPlayer2 = new Label();

        private readonly RadioButton m_RadioButtonBoardSize6 = new RadioButton();
        private readonly RadioButton m_RadioButtonBoardSize8 = new RadioButton();
        private readonly RadioButton m_RadioButtonBoardSize10 = new RadioButton();

        private readonly TextBox m_TextboxPlayer1Name = new TextBox();
        private readonly TextBox m_TextboxPlayer2Name = new TextBox();
        private readonly CheckBox m_CheckBoxPlayer2 = new CheckBox();

        private readonly Button m_ButtonDone = new Button();

        public FormGameSettings()
        {
            Size = new Size(300, 250);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Text = @"Game Settings";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            m_LabelBoardSize.Text = @"Board Size:";
            m_LabelBoardSize.Location = new Point(10, 20);

            m_RadioButtonBoardSize6.Text = @"6x6";
            m_RadioButtonBoardSize6.Checked = true;
            m_RadioButtonBoardSize6.Location = new Point(10, 40);

            m_RadioButtonBoardSize8.Text = @"8x8";
            m_RadioButtonBoardSize8.Location = new Point(m_RadioButtonBoardSize6.Right, 40);

            m_RadioButtonBoardSize10.Text = @"10x10";
            m_RadioButtonBoardSize10.Location = new Point(m_RadioButtonBoardSize8.Right, 40);

            m_LabelPlayers.Text = @"Players:";
            m_LabelPlayers.Location = new Point(10, 70);

            m_LabelPlayer1.Text = @"Player 1:";
            m_LabelPlayer1.Location = new Point(30, 100);

            int player1TextBoxTop = m_LabelPlayer1.Top + m_LabelPlayer1.Height / 2;
            player1TextBoxTop -= m_TextboxPlayer1Name.Height / 2;

            m_TextboxPlayer1Name.Location = new Point(m_LabelPlayer1.Right, player1TextBoxTop);

            m_LabelPlayer2.Text = @"Player 2:";
            m_LabelPlayer2.Location = new Point(30, 130);

            int player2TextBoxTop = m_LabelPlayer2.Top + m_LabelPlayer2.Height / 2;
            player2TextBoxTop -= m_TextboxPlayer2Name.Height / 2;

            m_TextboxPlayer2Name.Location = new Point(m_LabelPlayer2.Right, player2TextBoxTop);
            m_TextboxPlayer2Name.Enabled = false;
            m_TextboxPlayer2Name.Text = @"Computer";

            m_CheckBoxPlayer2.Checked = false;
            m_CheckBoxPlayer2.Location = new Point(15, 125);

            m_ButtonDone.Text = @"Done";
            m_ButtonDone.Location = new Point(100, 170);

            Controls.AddRange(new Control[]
                {m_LabelBoardSize, m_RadioButtonBoardSize6, m_RadioButtonBoardSize8, m_RadioButtonBoardSize10, m_LabelPlayers, m_LabelPlayer1, m_TextboxPlayer1Name, m_LabelPlayer2, m_TextboxPlayer2Name, m_CheckBoxPlayer2, m_ButtonDone});

            m_CheckBoxPlayer2.Click += new EventHandler(m_CheckBoxPlayer2_Check);
            m_ButtonDone.Click += new EventHandler(m_ButtonDone_Click);
        }

        void m_CheckBoxPlayer2_Check(object sender, EventArgs e)
        {
            m_TextboxPlayer2Name.Enabled = m_TextboxPlayer2Name.Enabled == false;
        }

        void m_ButtonDone_Click(object sender, EventArgs e)
        {
            if (validPlayersNames())
            {
                DialogResult = DialogResult.OK;
                FormGame form = new FormGame();
                Close();
            }
        }

        private bool validPlayersNames()
        {
            bool validNames = false;
            if (m_TextboxPlayer1Name.Text != "" && m_TextboxPlayer2Name.Text != "")
            {
                if (m_TextboxPlayer1Name.Text.Length <= 20 && m_TextboxPlayer2Name.Text.Length <= 20)
                {
                    validNames = true;
                }
                else
                {
                    if (MessageBox.Show(
                            "Players Names Are Too Long! (Max 20 char)",
                            "Players Names",
                            MessageBoxButtons.RetryCancel,
                            MessageBoxIcon.Warning) == DialogResult.Retry)
                    {
                        validNames = false;
                    }
                }
            }
            else
            {
                if (MessageBox.Show(
                        "Players Names Cannot Be Empty!",
                        "Players Names",
                        MessageBoxButtons.RetryCancel,
                        MessageBoxIcon.Warning) == DialogResult.Retry)
                {
                    validNames = false;
                }
            }

            return validNames;
        }

        public string Player1Name
        {
            get { return m_TextboxPlayer1Name.Text; }
            set { m_TextboxPlayer1Name.Text = value; }
        }

        public string Player2Name
        {
            get { return m_TextboxPlayer2Name.Text; }
            set { m_TextboxPlayer2Name.Text = value; }
        }

        public bool Player2Checked
        {
            get { return m_CheckBoxPlayer2.Checked; }
        }

        public int BoardSize
        {
            get
            {
                int boardSize = 6;
                if (m_RadioButtonBoardSize6.Checked)
                {
                    boardSize = 6;
                }
                else if (m_RadioButtonBoardSize8.Checked)
                {
                    boardSize = 8;
                }
                else if (m_RadioButtonBoardSize10.Checked)
                {
                    boardSize = 10;
                }

                return boardSize;
            }
        }

    }
}
