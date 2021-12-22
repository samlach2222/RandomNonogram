using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Logic
{
    /// <summary>
    /// HMI class that manages the display of the nonogram game board
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer dispatcherTimer;
        private readonly Clock chrono;
        private int errorCount = 0;
        private readonly int x = 11;
        private readonly int y = 11;
        private readonly int nbCases = 50;
        private readonly NonogramChest pc;
        private readonly Button[,] UItable;

        /// <summary>
        /// Constructor of the MainWindow class
        /// </summary>
        public MainWindow()
        {
            pc = new NonogramChest(x, y, nbCases);
            UItable = new Button[x, y];

            // Clock/Timer Part
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            chrono = new Clock();
            chrono.Start();
            dispatcherTimer.Start();

            InitializeComponent();

            TableInitialization();
            RowsIndexInitialization();
            ColumnsIndexInitialization();

            VictoryVerification();
        }

        /// <summary>
        /// Event occurring when the user left-clicks on an incorrect cell.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments of the event</param>
        private void BtnFalse_Click(object sender, EventArgs e) // if the case is false and the user do a Left Click
        {
            Button button = sender as Button;
            button.Background = Brushes.Red;
            button.IsHitTestVisible = false;
            errorCount++;
            UpdateErrorCount();
            VictoryVerification();
        }

        /// <summary>
        /// Event occurring when the user left-clicks on a correct cell.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments of the event</param>
        private void BtnTrue_Click(object sender, EventArgs e) // if the case is true and the user do a Left Click
        {
            Button button = sender as Button;
            if (button.Background == Brushes.Blue)
            {
                button.Background = Brushes.White;
            }
            else
            {
                button.Background = Brushes.Blue;
            }
            VictoryVerification();
        }

        /// <summary>
        /// Event occurring when the user right-clicks on an incorrect cell.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments of the event</param>
        private void BtnFalse_RightClick(object sender, EventArgs e) // if the case is false and the user do a Right Click
        {
            Button button = sender as Button;
            if(button.Background == Brushes.Green)
            {
                button.Background = Brushes.White;
            }
            else
            {
                button.Background = Brushes.Green;
            }
            VictoryVerification();
        }

        /// <summary>
        /// Event occurring when the user right-clicks on a correct cell.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments of the event</param>
        private void BtnTrue_RightClick(object sender, EventArgs e) // if the case is true and the user do a Right Click
        {
            Button button = sender as Button;
            if (button.Background == Brushes.Green)
            {
                button.Background = Brushes.White;
            }
            else
            {
                button.Background = Brushes.Green;
            }
            VictoryVerification();
        }

        /// <summary>
        /// Event occurring when the timer tick
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments of the event</param>
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            timer.Content = chrono.ToString();
        }

        /// <summary>
        /// Method to increase the error count when the user right-click on an incorrect button
        /// </summary>
        private void UpdateErrorCount()
        {
            errors.Content = "Errors : " + errorCount;
        }

        /// <summary>
        /// Method to initialize the game table
        /// </summary>
        private void TableInitialization()
        {
            for (int i = 1; i < x; i++)
            {
                for (int j = 1; j < y; j++)
                {
                    UItable[i, j] = new Button();
                    Grid.SetRow(UItable[i, j], i);
                    Grid.SetColumn(UItable[i, j], j);
                    grid.Children.Add(UItable[i, j]);

                    object resource = Application.Current.FindResource("btnPurple");
                    if (resource != null && resource.GetType() == typeof(Style))
                    {
                        UItable[i, j].Style = (Style)resource;
                    }

                    if (pc.Table[i, j])
                    {
                        UItable[i, j].Click += new RoutedEventHandler(BtnTrue_Click);
                        UItable[i, j].MouseDown += new MouseButtonEventHandler(BtnTrue_RightClick);
                    }
                    else
                    {
                        UItable[i, j].Click += new RoutedEventHandler(BtnFalse_Click);
                        UItable[i, j].MouseDown += new MouseButtonEventHandler(BtnFalse_RightClick);
                    }
                }
            }
        }

        /// <summary>
        /// Method to initilialize rows indexes
        /// </summary>
        private void RowsIndexInitialization()
        {
            for (int i = 0; i < x; i++) // rows index initialization
            {
                Label l = new Label
                {
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Center,
                    Content = pc.ListColumns()[i],
                    Foreground = Brushes.White
                };
                Grid.SetRow(l, i);
                Grid.SetColumn(l, 0);
                _ = grid.Children.Add(l); // ignore output
            }
        }

        /// <summary>
        /// Method to initialize columns indexes
        /// </summary>
        private void ColumnsIndexInitialization()
        {
            for (int j = 0; j < y; j++) // columns index initialization
            {
                Label l = new Label
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    Content = pc.ListRows()[j],
                    Foreground = Brushes.White
                };
                Grid.SetRow(l, 0);
                Grid.SetColumn(l, j);
                _ = grid.Children.Add(l); // ignore output
            }
        }

        /// <summary>
        /// Method to verify if the player win the game
        /// </summary>
        private void VictoryVerification()
        {
            int countCaseTrue = 0;
            int nbCaseTrue = ((x - 1) * (y - 1)) - nbCases;
            for (int i = 1; i < x; i++)
            {
                for (int j = 1; j < y; j++)
                {
                    if(UItable[i, j].Background == Brushes.Blue && pc.Table[i, j]) // if case true and user click on it
                    {
                        countCaseTrue++;
                    }
                }
            }

            if (countCaseTrue == nbCaseTrue)
            {
                _ = MessageBox.Show("Victory ! \nTime : " + timer.Content.ToString() + "\nErrors : " + errorCount);
                chrono.Stop();
                Close();
            }
        }
    }
}


