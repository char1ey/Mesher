using System;
using System.Windows;
using System.Windows.Forms;
using Mesher.GraphicsCore;

namespace Mesher
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var timer = new Timer();
            timer.Interval = 1;
            timer.Tick += Timer_Tick;

            timer.Start();
        }

        private void Child_MouseClick(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            
        }

        private void renderContext_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}
