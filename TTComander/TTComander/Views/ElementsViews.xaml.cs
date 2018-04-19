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
using TTComander.MyData;

namespace TTComander.Views
{
    public partial class ElementsViews : UserControl
    {
        public DiscElement discElement;
        /// <summary>
        /// Modelowanie User Control. If sprawdza czy discElement jest Folderem i 
        /// w zależności od tego przyporządkowuje ścieżkę do obrazka
        /// </summary>
        /// <param name="discElement">pobieramy obiekt discElement typu DiscElement</param>
        public ElementsViews(DiscElement discElement)
        {
            this.discElement = discElement;
            InitializeComponent();

            name.Content = discElement.Name;
            creationTime.Content = discElement.CreationTime;
            if(discElement is MyDirectory)
            {
                image.Source = new BitmapImage(new Uri(@"\MyData\Images\directory.bmp", UriKind.RelativeOrAbsolute));
            }
            else
            {
                image.Source = new BitmapImage(new Uri(@"\MyData\Images\file.bmp", UriKind.RelativeOrAbsolute));
            }
            
            
        }

       
        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// Tworzymy event który działa po kliknięciu checkBoxa
        /// </summary>
        public delegate void checkedEvent();
        public event checkedEvent checkedChangeEv;

        private void checkBox_Click(object sender, RoutedEventArgs e)
        {
            if (checkedChangeEv != null)
            {
                checkedChangeEv.Invoke();
                
            }
        }


    }
}
