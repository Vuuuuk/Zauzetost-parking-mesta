using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents.DocumentStructures;

namespace NetworkService.ViewModel
{
    public class ParkingViewModel : BindableBase
    {
        public static List<string> ParkingTypes { get; set; } = new List<string> { "OPEN Parking", "CLOSED Parking" };
        public static ObservableCollection<ParkingModel> Parkings { get; set; } = new ObservableCollection<ParkingModel>();
        public static ObservableCollection<ParkingModel> ParkingsSearched { get; set; } = new ObservableCollection<ParkingModel>();

        private ParkingModel currentParking = new ParkingModel();

        private string id;
        private string name;

        private ParkingModel selectedParking;

        private string selectedTypeParking = string.Empty;

        private string pathFirst = "pack://application:,,,/Images/";
        private string path;

        private bool isNameChecked = true;
        private bool isTypeChecked = false;
        private string searchText;

        public MyICommand DeleteCommand { get; set; }
        public MyICommand AddCommand { get; set; }
        public MyICommand SearchCommand { get; set; }

        public ParkingViewModel()
        {
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            AddCommand = new MyICommand(OnAdd, CanAdd);
            SearchCommand = new MyICommand(OnSearch);
        }



        public ParkingModel CurrentParking
        {
            get { return currentParking; }
            set
            {
                currentParking = value;
                OnPropertyChanged("CurrentParking");
            }
        }

        //Property
        public string Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); AddCommand.RaiseCanExecuteChanged(); }
        }
        public bool IsNameChecked
        {
            get { return isNameChecked; }
            set { isNameChecked = value; OnPropertyChanged("IsNameChecked"); }
        }

        public bool IsTypeChecked
        {
            get { return isTypeChecked; }
            set { isTypeChecked = value; OnPropertyChanged("IsTypeChecked"); }
        }

        public string SearchText
        {
            get { return searchText; }
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    OnPropertyChanged("SearchText");
                }
            }
        }

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); AddCommand.RaiseCanExecuteChanged(); }
        }

        public string Path
        {
            get { return path; }
            set { path = value; OnPropertyChanged("Path"); }
        }

        public string SelectedTypeParking
        {
            get { return selectedTypeParking; }
            set
            {
                if (selectedTypeParking != value)
                {
                    selectedTypeParking = value;
                    Path = pathFirst + value.ToString() + ".png";
                    OnPropertyChanged("Path");
                    OnPropertyChanged("SelectedTypeParking");
                    AddCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public ParkingModel SelectedParking
        {
            get { return selectedParking; }
            set { selectedParking = value; DeleteCommand.RaiseCanExecuteChanged(); }
        }


        private void OnSearch()
        {
            if (IsNameChecked)
            {
                ParkingsSearched.Clear();
                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    for (int i = 0; i < Parkings.Count(); i++)
                        ParkingsSearched.Add(Parkings[i]);
                }
                else
                {
                    for (int i = 0; i < Parkings.Count(); i++)
                    {
                        if (Parkings[i].Name.Contains(SearchText))
                            ParkingsSearched.Add(Parkings[i]);
                    }
                }
            }
            else 
            {
                ParkingsSearched.Clear();
                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    for (int i = 0; i < Parkings.Count(); i++)
                        ParkingsSearched.Add(Parkings[i]);
                }
                else
                {
                    for (int i = 0; i < Parkings.Count(); i++)
                    {
                        if (Parkings[i].TypeP.Name.Contains(SearchText))
                            ParkingsSearched.Add(Parkings[i]);
                    }
                }
            }
        }

        /*private bool CanSearch()
        {
            if (IsNameChecked == true || IsTypeChecked == true)
                return true;
            else
                return false;
        }*/
        private bool CanDelete()
        {
            return SelectedParking != null; 
        }

        private void OnDelete()
        {
            Parkings.Remove(SelectedParking);
            ParkingsSearched.Remove(SelectedParking);
        }

        private bool CanAdd()
        {
            if (SelectedTypeParking != null && CurrentParking.Id != 0 && CurrentParking.Name != null)
                return true;
            else
                return false;
        }

        private void OnAdd()
        {
            CurrentParking.Validate();
            if (CurrentParking.IsValid)
            {
                Model.Type tmp = new Model.Type(selectedTypeParking, pathFirst + SelectedTypeParking + ".png");
                Parkings.Add(new ParkingModel { Id = CurrentParking.Id, Name = CurrentParking.Name, Value = CurrentParking.Value, TypeP = tmp });
                ParkingsSearched.Add(new ParkingModel { Id = CurrentParking.Id, Name = CurrentParking.Name, Value = CurrentParking.Value, TypeP = tmp });
            }
            else
            {
                int pom = 0;
                try { pom = Int32.Parse(Id); }
                catch
                {
                    MessageBox.Show("ERROR -> ID must must not be empty or something beside a number!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                string pom1 = Name;
                if (string.IsNullOrWhiteSpace(pom1))
                {
                    System.Windows.MessageBox.Show("ERROR -> NAME must not be empty!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                bool postoji = false;
                foreach (ParkingModel p in Parkings)
                {
                    if (p.Id == pom)
                        postoji = true;
                }
                if (postoji)
                {
                    System.Windows.MessageBox.Show("ERROR -> ID already exists!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

    }
}
