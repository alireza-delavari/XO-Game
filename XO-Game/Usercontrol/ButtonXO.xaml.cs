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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XO_Game.Usercontrol
{
    public enum XOtype
    {
        empty, x, o
    }

    /// <summary>
    /// Interaction logic for ButtonXO.xaml
    /// </summary>
    public partial class ButtonXO : UserControl
    {
        public ButtonXO()
        {
            InitializeComponent();
        }

        private XOtype pathXo { get; set; }
        public XOtype PathXo
        {
            get { return pathXo; }
            set {
                pathXo = value;
                if (pathXo == XOtype.empty)
                {
                    pathX.Visibility = Visibility.Hidden;
                    pathO.Visibility = Visibility.Hidden;
                }
                else if (pathXo == XOtype.o)
                {
                    pathO.Visibility = Visibility.Visible;
                    pathX.Visibility = Visibility.Hidden;
                }
                else if (pathXo == XOtype.x)
                {
                    pathO.Visibility = Visibility.Hidden;
                    pathX.Visibility = Visibility.Visible;
                }
            }
        }

        public delegate RoutedEventHandler click();
        public event RoutedEventHandler Click;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventHandler temp = Click;
            if (temp != null)
            {
                temp(this, e);
            }
        }
    }
}
