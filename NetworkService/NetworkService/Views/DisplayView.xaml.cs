using NetworkService.Model;
using NetworkService.ViewModel;
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

namespace NetworkService.Views
{
    public partial class DisplayView : UserControl
    {
        public DisplayView()
        {
            DisplayViewModel.DisplayParking = new System.Collections.ObjectModel.ObservableCollection<Model.ParkingModel>();
            foreach (ParkingModel p in ParkingViewModel.ParkingsSearched)
            {
                DisplayViewModel.DisplayParking.Add(p);
            }

            InitializeComponent();
        }
    }
}
