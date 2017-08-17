using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using XO_Game.Usercontrol;

namespace XO_Game
{
    /// <summary>
    /// Interaction logic for frmSingleGame.xaml
    /// </summary>
    public partial class frmSingleGame : Window
    {

        public frmSingleGame()
        {
            InitializeComponent();
        }
        public frmSingleGame(bool multiplayer)
        {
            InitializeComponent();
            _multiplayer = multiplayer;
        }
        bool _multiplayer = false;
        int _Turn = 0;
        int _AgentTurn = 1;
        int _ManTurn = 0;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnItem0.PathXo = XOtype.o;
            FillAllItemsEmpty();
            AIAgent();
        }

        private void FillAllItemsEmpty()
        {
            btnItem1.PathXo = btnItem2.PathXo = btnItem3.PathXo = btnItem4.PathXo = btnItem5.PathXo = btnItem6.PathXo = btnItem7.PathXo = btnItem8.PathXo = btnItem9.PathXo = XOtype.empty;
        }

        private void btn_Reset_Click(object sender, RoutedEventArgs e)
        {
            _Turn = 0;
            _ManTurn = _AgentTurn;
            _AgentTurn = 1 - _ManTurn;
            EnableAndDisableButtons(true);
            FillAllItemsEmpty();
            AIAgent();
        }

        private void FillItem(Usercontrol.ButtonXO btnXO)
        {
            if (btnXO.PathXo==XOtype.empty)
            {
                if (_Turn == 0)
                {
                    btnXO.PathXo = XOtype.o;
                    _Turn = 1;
                    btnItem0.PathXo = XOtype.x;
                }
                else if (_Turn == 1)
                {
                    btnXO.PathXo = XOtype.x;
                    _Turn = 0;
                    btnItem0.PathXo = XOtype.o;
                }
            }
            else
                return;
            bool IsDooz=CheckDooz();
            if (IsDooz==true)
            {
                MessageBox.Show("Dooz!!!");
                EnableAndDisableButtons(false);
                return;                
            }
            AIAgent();
        }

        private void EnableAndDisableButtons(bool IsEnable)
        {
            btnItem1.IsEnabled = btnItem2.IsEnabled = btnItem3.IsEnabled = btnItem4.IsEnabled = btnItem5.IsEnabled
                = btnItem6.IsEnabled = btnItem7.IsEnabled = btnItem8.IsEnabled = btnItem9.IsEnabled = IsEnable;
        }

        private bool CheckDooz()
        {
            if (btnItem1.PathXo != XOtype.empty && btnItem1.PathXo == btnItem2.PathXo && btnItem2.PathXo == btnItem3.PathXo)
                return true;
            if (btnItem4.PathXo != XOtype.empty && btnItem4.PathXo == btnItem5.PathXo && btnItem5.PathXo == btnItem6.PathXo)
                return true;
            if (btnItem7.PathXo != XOtype.empty && btnItem7.PathXo == btnItem8.PathXo && btnItem8.PathXo == btnItem9.PathXo)
                return true;

            if (btnItem1.PathXo != XOtype.empty && btnItem1.PathXo == btnItem4.PathXo && btnItem4.PathXo == btnItem7.PathXo)
                return true;
            if (btnItem2.PathXo != XOtype.empty && btnItem2.PathXo == btnItem5.PathXo && btnItem5.PathXo == btnItem8.PathXo)
                return true;
            if (btnItem3.PathXo != XOtype.empty && btnItem3.PathXo == btnItem6.PathXo && btnItem6.PathXo == btnItem9.PathXo)
                return true;

            if (btnItem1.PathXo != XOtype.empty && btnItem1.PathXo == btnItem5.PathXo && btnItem5.PathXo == btnItem9.PathXo)
                return true;
            if (btnItem3.PathXo != XOtype.empty && btnItem3.PathXo == btnItem5.PathXo && btnItem5.PathXo == btnItem7.PathXo)
                return true;
            return false;

        }


        private void AIAgent()
        {
            if (_multiplayer)
                return;
            if (_Turn == _ManTurn)
                return;
            bool isEmpty = false;
            for (int i = 1; i <= 9; i++)
            {
                ButtonXO btnEmp = (ButtonXO)this.FindName("btnItem" + i);
                if (btnEmp.PathXo == XOtype.empty)
                    isEmpty = true;
            }
            if (!isEmpty)
                return;
            
            EnableAndDisableButtons(false);

            Random r = new Random(DateTime.Now.Millisecond);
            int rand = (r.Next(0, 100) % 9) + 1;
            ButtonXO btn = (ButtonXO)this.FindName("btnItem" + rand);

            while (btn.PathXo!=XOtype.empty)
            {
                rand = (r.Next(0, 100) % 9) + 1;
                btn = (ButtonXO)this.FindName("btnItem" + rand);
            }
            FillItem(btn);
            EnableAndDisableButtons(true);

        }

        private void btnItem1_Click(object sender, RoutedEventArgs e)
        {
            ButtonXO btn = (ButtonXO)sender;
            FillItem(btn);
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow frm = new MainWindow();
            frm.Show();
            this.Close();
        }
        
    }
}
