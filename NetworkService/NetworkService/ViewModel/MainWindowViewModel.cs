using NetworkService.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkService.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {
        public MyICommand<string> NavCommand { get; private set; }
        private ParkingViewModel parkingViewModel = new ParkingViewModel();
        private DisplayViewModel displayViewModel = new DisplayViewModel();
        private GraphViewModel graphViewModel = new GraphViewModel();
        private HelpViewModel helpViewModel = new HelpViewModel();
        private BindableBase currentViewModel;
        public BindableBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }
        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "Parking Data":
                    CurrentViewModel = parkingViewModel;
                    break;
                case "Display Data":
                    CurrentViewModel = displayViewModel;
                    break;
                case "Graph Data":
                    CurrentViewModel = graphViewModel;
                    break;
                case "Help":
                    CurrentViewModel = helpViewModel;
                    break;
            }
        }
        public MainWindowViewModel()
        {
            NavCommand = new MyICommand<String>(OnNav);
            CurrentViewModel = parkingViewModel;
        }
    }
}
