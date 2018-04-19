using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using TTComander.Views;

namespace TTComander
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FileSystemWatcher fileSystemWatcher = new FileSystemWatcher(directoryPath.Text);
            fileSystemWatcher.Created += FileSystemWatcher_Created;
            fileSystemWatcher.EnableRaisingEvents = true;

            directoryPath2.Text = directoryPath.Text;
         
            try
            {
                GenerateFilesList1();
                //------------------------------------------------------------------------------
                GenerateFilesList2();
            }
            catch (Exception)
            {
                MessageBox.Show("podana ścieżka nie istnieje");
            }
        }
        /// <summary>
        /// Magiczny kod, który jest
        /// </summary>

        private void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {

            Application.Current.Dispatcher.BeginInvoke(
            System.Windows.Threading.DispatcherPriority.Background,
            new Action(() =>
            {
                GenerateFilesList1();
            }));
        }
        /// <summary>
        /// Metoda aktywowana przez event CheckedChangeEv, która zmienia kolor zaznaczonego UserControl
        /// </summary>
        private void CheckedChangeEv()
        {
            foreach (object i in listOfUserControls1.Children)
            {
                ElementsViews elementViews = (ElementsViews)i;
                if (elementViews.checkBox.IsChecked.Value)
                {
                    elementViews.Background = new SolidColorBrush(Color.FromRgb(116, 166, 247));
                }
                else
                {
                    elementViews.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                }
            }
            foreach (object i in listOfUserControls2.Children)
            {
                ElementsViews elementViews = (ElementsViews)i;
                if (elementViews.checkBox.IsChecked.Value)
                {
                    elementViews.Background = new SolidColorBrush(Color.FromRgb(116, 166, 247));
                }
                else
                {
                    elementViews.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                }
            }
        }
        /// <summary>
        ///  odświeżamy listę podfolderów i plików w folderze
        /// </summary>

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GenerateFilesList1();
                GenerateFilesList2();
            }
            catch(Exception )
            {
                MessageBox.Show("podana ścieżka nie istnieje");
            }
        }
        /// <summary>
        /// Metoda generuje naszą listę podfolderów i plików w folderze dla 1 stack panelu
        /// </summary>
        private void GenerateFilesList1()
        {
            MyDirectory myDirectory1 = new MyDirectory(directoryPath.Text);
            listOfUserControls1.Children.Clear();

            List<DiscElement> subElements1 = myDirectory1.GetDiscElements();

            foreach (DiscElement discElement in subElements1)
            {
                ElementsViews discElementViews = new ElementsViews(discElement);

                listOfUserControls1.Children.Add(discElementViews);
                discElementViews.checkedChangeEv += CheckedChangeEv;

            }

        }
        /// <summary>
        /// Metoda generuje naszą listę podfolderów i plików w folderze dla 2 stack panelu
        /// </summary>
        private void GenerateFilesList2()
        {
            MyDirectory myDirectory2 = new MyDirectory(directoryPath2.Text);
            listOfUserControls2.Children.Clear();

            List<DiscElement> subElements2 = myDirectory2.GetDiscElements();

            foreach (DiscElement discElement in subElements2)
            {
                ElementsViews discElementViews = new ElementsViews(discElement);

                listOfUserControls2.Children.Add(discElementViews);
                discElementViews.checkedChangeEv += CheckedChangeEv;

            }
        }
       
        /// <summary>
        /// otwieramy zaznaczony folder i wyświetlamy jego podfoldery i i pliki
        /// jeżeli zaznaczony element jest plikiem otwieramy go za pomocą domyślnego edytora
        /// </summary>

        private void button_Click(object sender, RoutedEventArgs e)
        {
            foreach (object ob in listOfUserControls1.Children)
            {

                ElementsViews elementViews = (ElementsViews)ob;
                if (elementViews.checkBox.IsChecked.Value)

                {
                    if (elementViews.discElement is MyDirectory)
                    {
                        string path = elementViews.discElement.Path;
                        try
                        {
                            Open_new_folder1(path);
                        }
                        catch(Exception )
                        {
                            MessageBox.Show("Brak dostępu do folderu");
                        }
                    }

                    else
                    {
                        string path = elementViews.discElement.Path;
                        try
                        {
                            System.Diagnostics.Process.Start(path);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("wystąpił błąd w trakcie otwierania pliku");
                        }
                    }
                    break;
                }
            }
            //---------------------------------------------
            foreach (object ok in listOfUserControls2.Children)
            {

                ElementsViews elementViews = (ElementsViews)ok;
                if (elementViews.checkBox.IsChecked.Value)

                {
                    if (elementViews.discElement is MyDirectory)
                    {
                        string path = elementViews.discElement.Path;
                        try
                        {
                            Open_new_folder2(path);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("brak dostępu do folderu");
                        }
                    }
                    else
                    {
                        string path = elementViews.discElement.Path;
                        try
                        {
                            System.Diagnostics.Process.Start(path);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("wystąpił błąd w trakcie otwierania pliku");
                        }
                    }
                    break;
                }
            }

        }
        /// <summary>
        /// pobieramy sciezke do zaznaczonego folderu i tworzymy liste jego podfolderow i plikow
        /// </summary>
        private void Open_new_folder1(string path)
        {
            directoryPath.Text = path;
            MyDirectory myDirectory1 = new MyDirectory(directoryPath.Text);
            listOfUserControls1.Children.Clear();


            List<DiscElement> subElements1 = myDirectory1.GetDiscElements();

            foreach (DiscElement discElement in subElements1)
            {
                ElementsViews discElementViews = new ElementsViews(discElement);

                listOfUserControls1.Children.Add(discElementViews);
                discElementViews.checkedChangeEv += CheckedChangeEv;

            }
        }
        //----------------------------------------------
        private void Open_new_folder2(string path)
        {
            directoryPath2.Text = path;
            MyDirectory myDirectory2 = new MyDirectory(directoryPath2.Text);
            listOfUserControls2.Children.Clear();


            List<DiscElement> subElements2 = myDirectory2.GetDiscElements();

            foreach (DiscElement discElement in subElements2)
            {
                ElementsViews discElementViews = new ElementsViews(discElement);

                listOfUserControls2.Children.Add(discElementViews);
                discElementViews.checkedChangeEv += CheckedChangeEv;

            }
        }

        /// <summary>
        /// usuwamy zaznaczone pliki metodą File.Delete (Nie usuwa folderow)
        /// </summary>


        private void button1_Click(object sender, RoutedEventArgs e)
        {

            foreach (object pi in listOfUserControls1.Children)
            {
                ElementsViews elementViews = (ElementsViews)pi;
                if (elementViews.checkBox.IsChecked.Value)
                {
                    try
                    {
                        System.IO.File.Delete(elementViews.discElement.Path);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        MessageBox.Show("Brak dostępu do pliku");
                    }
                    catch (System.IO.IOException)
                    {
                        MessageBox.Show("Plik w użyciu");
                    }
                }

            }
            //----------------------------------------------------
            foreach (object pi in listOfUserControls2.Children)
            {
                ElementsViews elementViews = (ElementsViews)pi;
                if (elementViews.checkBox.IsChecked.Value)
                {
                    try
                    {
                        System.IO.File.Delete(elementViews.discElement.Path);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        MessageBox.Show("Brak dostępu do pliku");
                    }
                    catch (System.IO.IOException)
                    {
                        MessageBox.Show("Plik w użyciu");
                    }
                }

            }
            GenerateFilesList1();
            GenerateFilesList2();
        }


        /// <summary>
        /// Tworzymy nowy folder. Nazwe nowego folderu nalezy wpisac do textboxa 
        /// </summary>

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            string path = directoryPath.Text;
            try
            {
                System.IO.Directory.CreateDirectory(path);

            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("wystąpił błąd");
            }
            catch (ArgumentException)
            {
                MessageBox.Show("wystąpił błąd");
            }
            GenerateFilesList1();
            GenerateFilesList2();
        }
        //----------------------------------------------------

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            string path = directoryPath2.Text;
            try
            {
                System.IO.Directory.CreateDirectory(path);

            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("wystąpił błąd");
            }
            catch (ArgumentException)
            {
                MessageBox.Show("wystąpił błąd");
            }
            GenerateFilesList2();
            GenerateFilesList1();
        }



        /// <summary>
        /// Przechodzimy do folderu nadrzednego. Sciezke pobieramy metoda Directory.GetParent(path)
        /// path pobieramy z textboxa
        /// </summary>

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            string path = directoryPath.Text;
            try
            {
                string path2 = System.IO.Directory.GetParent(path).FullName;
                directoryPath.Text = path2;
            }
            catch (Exception)
            {
                MessageBox.Show("Folder nadrzędny nie istnieje");
            }
            GenerateFilesList1();


        }
        //----------------------------------------------------------------

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            string path = directoryPath2.Text;
            try
            {
                string path2 = System.IO.Directory.GetParent(path).FullName;
                directoryPath2.Text = path2;
            }
            catch (Exception)
            {
                MessageBox.Show("Folder nadrzędny nie istnieje");
            }
            GenerateFilesList2();
        }


        /// <summary>
        /// Jesli zaznaczony element jest plikiem kopiujemy go do folderu po lewej stronie
        /// Jesli jest folderem to tworzymy nowy folder i kopiujemy wszystkie jego pliki
        /// </summary>

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            foreach (object k in listOfUserControls2.Children)
            {
                ElementsViews elementViews = (ElementsViews)k;
                if (elementViews.checkBox.IsChecked.Value)
                {
                    if (elementViews.discElement is MyFile)
                    {
                        try
                        {
                            string fName = elementViews.discElement.Name;
                            string path = elementViews.discElement.Path;
                            File.Copy(path, System.IO.Path.Combine(directoryPath.Text, fName));
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("nie można skopiować pliku");
                        }
                    }
                    //-----------------------------
                    else
                    {
                        try
                        {
                            string dirName = elementViews.discElement.Name;
                            string pathx = System.IO.Path.Combine(directoryPath.Text, dirName);
                            Directory.CreateDirectory(pathx);
                            MyDirectory myDirectory1 = new MyDirectory(elementViews.discElement.Path);
                            List<MyFile> subElements = myDirectory1.GetAllFiles();
                            foreach (MyFile file in subElements)
                            {
                                string fName = file.Name;
                                File.Copy(file.Path, System.IO.Path.Combine(pathx, fName));

                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Nie można skopiować folderu");
                        }
                    }
                }
            }
            GenerateFilesList1();
        }


        /// <summary>
        /// Sortujemy liste po nazwie (A-Z)
        /// </summary>

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            MyDirectory myDirectory1 = new MyDirectory(directoryPath.Text);
            listOfUserControls1.Children.Clear();

            List<DiscElement> subElements1 = myDirectory1.GetDiscElements();
            subElements1.Sort((x, y) => string.Compare(x.Name, y.Name));

            foreach (DiscElement discElement in subElements1)
            {
                ElementsViews discElementViews = new ElementsViews(discElement);

                listOfUserControls1.Children.Add(discElementViews);

                discElementViews.checkedChangeEv += CheckedChangeEv;

            }

        }


        //----------------------------------------------------------------
        private void button9_Click(object sender, RoutedEventArgs e)
        {
            MyDirectory myDirectory2 = new MyDirectory(directoryPath2.Text);
            listOfUserControls2.Children.Clear();

            List<DiscElement> subElements2 = myDirectory2.GetDiscElements();
            subElements2.Sort((x, y) => string.Compare(x.Name, y.Name));

            foreach (DiscElement discElement in subElements2)
            {
                ElementsViews discElementViews = new ElementsViews(discElement);

                listOfUserControls2.Children.Add(discElementViews);

                discElementViews.checkedChangeEv += CheckedChangeEv;

            }
        }


        /// <summary>
        /// Sortujemy listę po dacie utworzenia (od najstarszych do najmłodszych)
        /// </summary>

        private void button10_Click(object sender, RoutedEventArgs e)
        {
            MyDirectory myDirectory1 = new MyDirectory(directoryPath.Text);
            listOfUserControls1.Children.Clear();

            List<DiscElement> subElements1 = myDirectory1.GetDiscElements();
            subElements1.Sort((x, y) => DateTime.Compare(x.CreationTime, y.CreationTime));

            foreach (DiscElement discElement in subElements1)
            {
                ElementsViews discElementViews = new ElementsViews(discElement);

                listOfUserControls1.Children.Add(discElementViews);

                discElementViews.checkedChangeEv += CheckedChangeEv;

            }
        }
        //-------------------------------------------------

        private void button11_Click(object sender, RoutedEventArgs e)
        {
            MyDirectory myDirectory2 = new MyDirectory(directoryPath2.Text);
            listOfUserControls2.Children.Clear();

            List<DiscElement> subElements2 = myDirectory2.GetDiscElements();
            subElements2.Sort((x, y) => DateTime.Compare(x.CreationTime, y.CreationTime));

            foreach (DiscElement discElement in subElements2)
            {
                ElementsViews discElementViews = new ElementsViews(discElement);

                listOfUserControls2.Children.Add(discElementViews);

                discElementViews.checkedChangeEv += CheckedChangeEv;

            }
        }


        /// <summary>
        /// Wyszukujemy wszyskie pliki których nazwy zawieraja podany przez
        /// nas ciag znakow w textboxie 
        /// </summary>

        private void button12_Click(object sender, RoutedEventArgs e)
        {
            MyDirectory myDirectory1 = new MyDirectory(directoryPath.Text);
            listOfUserControls1.Children.Clear();

            List<DiscElement> subElements1 = myDirectory1.GetDiscElements();
            List<DiscElement> results = subElements1.FindAll(x => x.Name.Contains(textBox.Text));
            foreach (DiscElement discElement in results)
            {
                {
                    ElementsViews discElementViews = new ElementsViews(discElement);

                    listOfUserControls1.Children.Add(discElementViews);
                    discElementViews.checkedChangeEv += CheckedChangeEv;
                }
            }
            //------------------------------------------------------------
            MyDirectory myDirectory2 = new MyDirectory(directoryPath2.Text);
            listOfUserControls2.Children.Clear();

            List<DiscElement> subElements2 = myDirectory2.GetDiscElements();
            List<DiscElement> results2 = subElements2.FindAll(x => x.Name.Contains(textBox.Text));
            foreach (DiscElement discElement in results2)
            {
                ElementsViews discElementViews = new ElementsViews(discElement);

                listOfUserControls2.Children.Add(discElementViews);
                discElementViews.checkedChangeEv += CheckedChangeEv;

            }
        }
        //-------------------------------------------------------------
        


    }
}
