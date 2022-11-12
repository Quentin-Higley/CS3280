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
                //sql command object
                conn = new Connection();
                //changes the combobox itemsource to be flights so that they can be used
                flights = conn.getFlights();
                cbChooseFlight.ItemsSource = flights;
                // this is the click mode
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
                //enable conrols
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
                //anables the controls
                cbChooseFlight.IsEnabled = true;
                cbChoosePassenger.IsEnabled = true;
                cmdAddPassenger.IsEnabled = true;
                cmdChangeSeat.IsEnabled = true;
                cmdDeletePassenger.IsEnabled = true;
                //changes the click mode to be the default click mode
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
                //disables window controls
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
                // get the currently selected flight
                Flight selection = (Flight)cbChooseFlight.SelectedItem;  //This is wrong, if a list of flights was in the combo box, then could get the selected flight in an object
                //set the cb to no selected item
                cbChoosePassenger.SelectedIndex = -1;
                //adds the passengers from the flight to the flights person list
                selection.addPassengers(conn.getPassenger(selection.FlightId));

                //change item source
                cbChoosePassenger.ItemsSource = selection.PassengerList;
                //Enable passenger cb
                cbChoosePassenger.IsEnabled = true;
                //enable buttons
                gPassengerCommands.IsEnabled = true;
                //seat canvases
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

                //color the labels correctly
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
                //label that was clicked
                Label label = (Label)sender;

                // if the seat is taken or selected return
                if (label.Background == Brushes.Red || label.Background == Brushes.Green)
                    return;
                //get selected flight

                Flight flight = (Flight)cbChooseFlight.SelectedItem;
                //get the canvas that is to the selected flight
                Canvas canvas = this.FindName($"c{flight.AircraftType.Split()[1]}_Seats") as Canvas;

                //get the seat number
                string seat = (string)label.Content;
                // the arguments for the sql command First Name Last Name
                string[] pArgs = { passed[0], passed[1] };
                //call the function to add a passenger
                conn.execSql("addPassenger", pArgs);
                //get the added passenger ID
                conn.execSql("getPassengerId", pArgs);
                //store the passengerId
                string pId = conn.Scalar;
                // add the link
                string[] lArgs = { flight.FlightId, pId, seat };

                //call the function to add the seat
                conn.execSql("insertLink", lArgs);
                //Add the passenger to the list
                flight.addPassengers(conn.getPassenger(flight.FlightId));

                //set the UI seat number of the clicked seat
                lblPassengersSeatNumber.Content = $"{seat}";
                //get the index of the passenger
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
                //set the cb selected item to the the newelly added passenger index
                cbChoosePassenger.SelectedIndex = idx;
                //reset the seat colors
                fillSeats(canvas, flight);
                //change the selected seat
                selectedSeat(canvas, int.Parse(seat));
                //enable UI
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
                //the clicked seat
                Label label = (Label)sender;
                // if the background is red or green skip
                if (label.Background == Brushes.Red || label.Background == Brushes.Green)
                    return;

                //get the selected flight
                Flight flight = (Flight)cbChooseFlight.SelectedItem;
                //get the selected passenger
                Passenger passenger = (Passenger)cbChoosePassenger.SelectedItem;
                //get the correct seat canvas
                Canvas canvas = this.FindName($"c{flight.AircraftType.Split()[1]}_Seats") as Canvas;

                //get the seat number
                string seat = (string)label.Content;
                //arguments for sql seat number flightID and passengerID
                string[] args = { seat, flight.FlightId, passenger.PassengerId };
                // call the function that calls to execute the sql command
                conn.execSql("updateSeat", args);
                //refresh passenger list
                flight.addPassengers(conn.getPassenger(flight.FlightId));

                //update the ui
                lblPassengersSeatNumber.Content = $"{seat}";


                //update seat map
                fillSeats(canvas, flight);
                //select the new seat
                selectedSeat(canvas, int.Parse(seat));

                //enabel ui
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
                //label that was clicked
                Label label = (Label)sender;
                //current selected flight
                Flight flight = (Flight)cbChooseFlight.SelectedItem;
                // flight canvas
                Canvas canvas = this.FindName($"c{flight.AircraftType.Split()[1]}_Seats") as Canvas;
                //seat number
                string seat = (string)label.Content;

                //get passenger index
                int idx = 0;
                //get the passenger index
                foreach (Passenger p in flight.PassengerList)
                {
                    if (p.SeatNumber == seat)
                    {
                        found = true;
                        break;
                    }
                    idx++;
                }
                //if it is not found then it is not a valid selection
                if (!found)
                    return;
                //set passenger to the correct index
                cbChoosePassenger.SelectedIndex = idx;
                //update the seat
                lblPassengersSeatNumber.Content = $"{seat}";

                // set the selected seat to green
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
                    // default seat click
                    {0,()=>modeRegular(sender) },
                    // if updating seat
                    {1,()=>modeUpdate(sender) },
                    // if adding seats
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
                // if their is no selected passenger do nothing
                if (cbChoosePassenger.SelectedIndex < 0)
                    return;
                // get current selected passenger
                Passenger selection = (Passenger)cbChoosePassenger.SelectedItem;
                //get the current flight
                Flight flight = (Flight)cbChooseFlight.SelectedItem;
                //get correct canvas
                Canvas canvas = this.FindName($"c{flight.AircraftType.Split()[1]}_Seats") as Canvas;
                //get the seat number from the passenger
                int seat = int.Parse(selection.SeatNumber);
                //change the ui
                lblPassengersSeatNumber.Content = seat;
                //update selected seat
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
                //get the amount of labels on the canvas
                int labelCount = VisualTreeHelper.GetChildrenCount(canvas);
                for (int i = 0; i < labelCount; i++)
                {
                    //get the ith label
                    Label label = VisualTreeHelper.GetChild(canvas, i) as Label;
                    //get the content of it
                    int content = int.Parse((string)label.Content);
                    //if it is currently green change to red
                    if (label.Background == Brushes.Green)
                        label.Background = Brushes.Red;
                    //if it is not red change to blue
                    else if (label.Background != Brushes.Red)
                        label.Background = Brushes.Blue;

                    //otherwise if it is the correct seat change to green
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
                // disable ui
                disable();
                //change to add passenger mode
                mode = 2;
                //change the window
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
                //get the seats to red and blue if taken or not
                int labelCount = VisualTreeHelper.GetChildrenCount(canvas);
                for (int i = 0; i < labelCount; i++)
                {
                    // ith label
                    Label label = VisualTreeHelper.GetChild(canvas, i) as Label;
                    //get the seat number of the label
                    int content = int.Parse((string)label.Content);
                    // change it to blue
                    label.Background = Brushes.Blue;


                    foreach (Passenger p in selected.PassengerList)
                    {
                        //if the passenger is on the flight the seat is red
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
                // if there is no selected passenger skip
                if (cbChoosePassenger.SelectedIndex < 0)
                    return;

                //get selected passenger
                Passenger p = (Passenger)cbChoosePassenger.SelectedItem;
                //get selected flight
                Flight f = (Flight)cbChooseFlight.SelectedItem;
                //get passenger id for sql arguments
                string[] dPassenger = { p.PassengerId };
                //get flightId and passengerId for sql arguments
                string[] dLink = { f.FlightId, p.PassengerId };
                //call function to execute the sql commands
                conn.execSql("deleteLink", dLink);
                conn.execSql("deletePassenger", dPassenger);
                //refresh passenger list
                string[] flightId = { f.FlightId };
                conn.execSql("getPassengers", flightId);
                f.addPassengers(conn.getPassenger(f.FlightId));
                //update the ui
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
                //if no seleced skip
                if (cbChoosePassenger.SelectedIndex < 0)
                    return;
                //disable ui
                disable();
                //change ui mode
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
