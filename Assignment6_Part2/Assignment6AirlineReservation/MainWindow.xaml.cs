using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// add passenger window
        /// </summary>
        private wndAddPassenger wndAddPass;
        /// <summary>
        /// db connections
        /// </summary>
        private Connection conn;
        /// <summary>
        /// list of flights
        /// </summary>
        private List<Flight> flights;
        /// <summary>
        /// modes for clicking on seats
        /// </summary>
        private Dictionary<int, Action> modes;
        /// <summary>
        /// inital mode is defaulted to 0
        /// </summary>
        private int mode;
        /// <summary>
        /// passed information
        /// </summary>
        private string[] passed;
        /// <summary>
        /// MainWindow Constructor
        /// </summary>
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                conn = new Connection();
                flights = conn.getFlights();
                cbChooseFlight.ItemsSource = flights;
                mode = 0;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// handles canceling add passenger
        /// </summary>
        /// <exception cref="Exception">Exception</exception>
        public void cancel()
        {
            try
            {
                enable();
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// handles data passing
        /// </summary>
        /// <param name="name">array contains the name</param>
        public void pPass(string[] name)
        {
            try
            {
                passed = name;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// enables the controls
        /// </summary>
        /// <exception cref="Exception">Exception</exception>
        private void enable()
        {
            try
            {
                cbChooseFlight.IsEnabled = true;
                cbChoosePassenger.IsEnabled = true;
                cmdAddPassenger.IsEnabled = true;
                cmdChangeSeat.IsEnabled = true;
                cmdDeletePassenger.IsEnabled = true;
                mode = 0;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// disables the controls
        /// </summary>
        /// <exception cref="Exception">Exception</exception>
        private void disable()
        {
            try
            {
                cbChooseFlight.IsEnabled = false;
                cbChoosePassenger.IsEnabled = false;
                cmdAddPassenger.IsEnabled = false;
                cmdChangeSeat.IsEnabled = false;
                cmdDeletePassenger.IsEnabled = false;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// changing flight code
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">event</param>
        private void cbChooseFlight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Flight selection = (Flight)cbChooseFlight.SelectedItem;  //This is wrong, if a list of flights was in the combo box, then could get the selected flight in an object
                cbChoosePassenger.SelectedIndex = -1;
                selection.addPassengers(conn.getPassenger(selection.FlightId));
                cbChoosePassenger.ItemsSource = selection.PassengerList;
                cbChoosePassenger.IsEnabled = true;
                gPassengerCommands.IsEnabled = true;
                Canvas canvas;
                
                //Should be using a flight object to get the flight ID here
                if (selection.FlightId == "2")
                {
                    CanvasA380.Visibility = Visibility.Hidden;
                    Canvas767.Visibility = Visibility.Visible;
                    canvas = this.c767_Seats;
                }
                else
                {
                    Canvas767.Visibility = Visibility.Hidden;
                    CanvasA380.Visibility = Visibility.Visible;

                    canvas = this.cA380_Seats;
                }

                fillSeats(canvas, selection);
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// addding passenger mode
        /// </summary>
        /// <param name="sender">object</param>
        /// <exception cref="Exception">Exception</exception>
        private void modeAdd(Object sender)
        {
            try
            {
                Label label = (Label)sender;

                if (label.Background == Brushes.Red || label.Background == Brushes.Green)
                    return;

                Flight flight = (Flight)cbChooseFlight.SelectedItem;
                Canvas canvas = this.FindName($"c{flight.AircraftType.Split()[1]}_Seats") as Canvas;

                string seat = (string)label.Content;
                string[] pArgs = { passed[0], passed[1] };
                conn.execSql("addPassenger", pArgs);
                conn.execSql("getPassengerId", pArgs);
                string pId = conn.Scalar;
                string[] lArgs = { flight.FlightId, pId, seat };

                conn.execSql("insertLink", lArgs);
                flight.addPassengers(conn.getPassenger(flight.FlightId));

                lblPassengersSeatNumber.Content = $"{seat}";
                bool found = false;
                int idx = 0;
                foreach (Passenger p in flight.PassengerList)
                {
                    if (p.SeatNumber == seat)
                    {
                        found = true;
                        break;
                    }
                    idx++;
                }
                cbChoosePassenger.SelectedIndex = idx;
                fillSeats(canvas, flight);
                selectedSeat(canvas, int.Parse(seat));
                enable();
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// updating passenger code
        /// </summary>
        /// <param name="sender">object label</param>
        /// <exception cref="Exception">Exception</exception>
        private void modeUpdate(Object sender)
        {
            try
            {
                Label label = (Label)sender;
                if (label.Background == Brushes.Red || label.Background == Brushes.Green)
                    return;

                Flight flight = (Flight)cbChooseFlight.SelectedItem;
                Passenger passenger = (Passenger)cbChoosePassenger.SelectedItem;
                Canvas canvas = this.FindName($"c{flight.AircraftType.Split()[1]}_Seats") as Canvas;

                string seat = (string)label.Content;
                string[] args = { seat, flight.FlightId, passenger.PassengerId };
                conn.execSql("updateSeat", args);
                flight.addPassengers(conn.getPassenger(flight.FlightId));


                lblPassengersSeatNumber.Content = $"{seat}";


                fillSeats(canvas, flight);
                selectedSeat(canvas, int.Parse(seat));

                enable();
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// regular mode code
        /// </summary>
        /// <param name="sender">object label</param>
        /// <exception cref="Exception">Exception</exception>
        private void modeRegular(Object sender)
        {
            try
            {
                bool found = false;
                Label label = (Label)sender;
                Flight flight = (Flight)cbChooseFlight.SelectedItem;
                Canvas canvas = this.FindName($"c{flight.AircraftType.Split()[1]}_Seats") as Canvas;
                string seat = (string)label.Content;

                int idx = 0;
                foreach (Passenger p in flight.PassengerList)
                {
                    if (p.SeatNumber == seat)
                    {
                        found = true;
                        break;
                    }
                    idx++;
                }
                if (!found)
                    return;
                cbChoosePassenger.SelectedIndex = idx;
                lblPassengersSeatNumber.Content = $"{seat}";

                selectedSeat(canvas, int.Parse(seat));

            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// what happens when the user clicks on a seat
        /// </summary>
        /// <param name="sender">object label</param>
        /// <param name="e">event</param>
        private void clickSeat(object sender, RoutedEventArgs e)
        {
            try
            {
                modes = new Dictionary<int, Action>()
                {
                    {0,()=>modeRegular(sender) },
                    {1,()=>modeUpdate(sender) },
                    {2,()=>modeAdd(sender) }
                };
                modes[mode]();
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// changing listbox passenger code
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">event</param>
        private void cbChoosePassenger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbChoosePassenger.SelectedIndex < 0)
                    return;
                Passenger selection = (Passenger)cbChoosePassenger.SelectedItem;
                Flight flight = (Flight)cbChooseFlight.SelectedItem;
                Canvas canvas = this.FindName($"c{flight.AircraftType.Split()[1]}_Seats") as Canvas;
                int seat = int.Parse(selection.SeatNumber);
                lblPassengersSeatNumber.Content = seat;
                selectedSeat(canvas, seat);
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// code that changes the color of seats
        /// </summary>
        /// <param name="canvas">selected flight canvas</param>
        /// <param name="Selected">selected seat</param>
        /// <exception cref="Exception">Exception</exception>
        private void selectedSeat(Canvas canvas, int Selected)
        {
            try
            {
                int labelCount = VisualTreeHelper.GetChildrenCount(canvas);
                for (int i = 0; i < labelCount; i++)
                {
                    Label label = VisualTreeHelper.GetChild(canvas, i) as Label;
                    int content = int.Parse((string)label.Content);
                    if (label.Background == Brushes.Green)
                        label.Background = Brushes.Red;
                    else if (label.Background != Brushes.Red)
                        label.Background = Brushes.Blue;

                    if (content == Selected)
                    {
                        label.Background = Brushes.Green;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// handles the add passenger button
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">event</param>
        private void cmdAddPassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                disable();
                mode = 2;
                wndAddPass = new wndAddPassenger();
                wndAddPass.ShowDialog();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// handles filling the seats
        /// </summary>
        /// <param name="canvas">flight seat canvas</param>
        /// <param name="selected">selected flight</param>
        /// <exception cref="Exception">Exception</exception>
        private void fillSeats(Canvas canvas, Flight selected)
        {
            try
            {
                int labelCount = VisualTreeHelper.GetChildrenCount(canvas);
                for (int i = 0; i < labelCount; i++)
                {
                    Label label = VisualTreeHelper.GetChild(canvas, i) as Label;
                    int content = int.Parse((string)label.Content);
                    label.Background = Brushes.Blue;


                    foreach (Passenger p in selected.PassengerList)
                    {
                        int seat = int.Parse(p.SeatNumber);
                        if (content == seat)
                            label.Background = Brushes.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// handles errors
        /// </summary>
        /// <param name="sClass">class that throws exception</param>
        /// <param name="sMethod">method that throws exception</param>
        /// <param name="sMessage">message</param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }

        /// <summary>
        /// handles delete passenger code
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">event</param>
        private void cmdDeletePassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbChoosePassenger.SelectedIndex < 0)
                    return;

                Passenger p = (Passenger)cbChoosePassenger.SelectedItem;
                Flight f = (Flight)cbChooseFlight.SelectedItem;
                string[] dPassenger = { p.PassengerId };
                string[] dLink = { f.FlightId, p.PassengerId };
                conn.execSql("deleteLink", dLink);
                conn.execSql("deletePassenger", dPassenger);
                string[] flightId = { f.FlightId };
                conn.execSql("getPassengers", flightId);
                f.addPassengers(conn.getPassenger(f.FlightId));
                Canvas canvas = this.FindName($"c{f.AircraftType.Split()[1]}_Seats") as Canvas;
                fillSeats(canvas, f);
                cbChoosePassenger.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// handles change seate code
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">event</param>
        private void cmdChangeSeat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbChoosePassenger.SelectedIndex < 0)
                    return;

                disable();
                mode = 1;
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

    }
}
