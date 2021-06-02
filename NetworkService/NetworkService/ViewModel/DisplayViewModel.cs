using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NetworkService.ViewModel
{
    public class DisplayViewModel : BindableBase
    {
        public static ObservableCollection<ParkingModel> DisplayParking { get; set; }

        public MyICommand<Canvas> Drop_Command { get; set; }
        public MyICommand<Canvas> DragOver_Command { get; set; }
        public MyICommand<Canvas> MouseUp_Command { get; set; }
        public MyICommand<ListView> ListView_SelectionChanged { get; set; }
        public MyICommand<Canvas> Remove_Command { get; set; }

        private ParkingModel currentParking = new ParkingModel();
        private ParkingModel selectedParking = new ParkingModel();

        private bool dragging = false;

        public ParkingModel CurrentParking
        {
            get { return currentParking; }
            set { currentParking = value; OnPropertyChanged("CurrentParking"); }
        }

        public ParkingModel SelectedParking
        {
            get { return selectedParking; }
            set { selectedParking = value; OnPropertyChanged("SelectedParking"); }
        }

        public DisplayViewModel()
        {
            Drop_Command = new MyICommand<Canvas>(OnDrop);
            DragOver_Command = new MyICommand<Canvas>(OnDragOver);
            MouseUp_Command = new MyICommand<Canvas>(OnMouseUp);
            ListView_SelectionChanged = new MyICommand<ListView>(OnListViewChange);
            Remove_Command = new MyICommand<Canvas>(OnRemove);
        }

        private void OnRemove(Canvas obj)
        {
            if(obj.Resources["taken"] != null)
            {
                string[] podeli = ((TextBlock)((obj).Children[0])).Text.Split(',');
                string name = podeli[1];
                int value = Int32.Parse(((TextBlock)((obj).Children[1])).Text);
                int id = Int32.Parse(((TextBlock)((obj).Children[2])).Text);
                Model.Type t = new Model.Type();
                t.Name = ((TextBlock)((obj).Children[3])).Text;
                t.ImgSrc = ((TextBlock)((obj).Children[4])).Text;
                obj.Background = Brushes.Transparent;
                DisplayParking.Add(new ParkingModel() { Name = name, Value = value, Id = id, TypeP = t }) ;
                ((TextBlock)((obj).Children[5])).Text = string.Empty;
                ((TextBlock)((obj).Children[0])).Text = string.Empty;
                ((TextBlock)((obj).Children[1])).Text = string.Empty;
                ((TextBlock)((obj).Children[2])).Text = string.Empty;
                ((TextBlock)((obj).Children[3])).Text = string.Empty;
                ((TextBlock)((obj).Children[4])).Text = string.Empty;
                obj.Resources.Remove("taken");
            }
        }

        private void OnDrop(Canvas obj)
        {
            if(SelectedParking != null)
            {
                if((obj).Resources["taken"] == null)
                {
                    BitmapImage slika = new BitmapImage();
                    slika.BeginInit();
                    slika.UriSource = new Uri(SelectedParking.TypeP.ImgSrc);
                    slika.EndInit();
                    (obj).Background = new ImageBrush(slika);
                    if(SelectedParking.Value > 90)
                        ((TextBlock)((obj).Children[5])).Text = "!";
                    ((TextBlock)((obj).Children[0])).Text = SelectedParking.Id + "," + SelectedParking.Name;
                    ((TextBlock)((obj).Children[1])).Text = SelectedParking.Value.ToString();
                    ((TextBlock)((obj).Children[2])).Text = SelectedParking.Id.ToString();
                    ((TextBlock)((obj).Children[3])).Text = SelectedParking.TypeP.Name;
                    ((TextBlock)((obj).Children[4])).Text = SelectedParking.TypeP.ImgSrc;
                    (obj).Resources.Add("taken", true);
                }
                SelectedParking = null;
                dragging = false;
            }
        }

        private void OnDragOver(Canvas obj)
        {
            if (obj.Resources["taken"] != null)
                obj.AllowDrop = false;
            else
                obj.AllowDrop = true;
        }

        private void OnMouseUp(Canvas obj)
        {
            CurrentParking = null;
            SelectedParking = null;
            dragging = false;
        }

        private void OnListViewChange(ListView obj)
        {
            if(!dragging)
            {
                dragging = true;
                CurrentParking = SelectedParking;
                DragDrop.DoDragDrop(obj, CurrentParking, DragDropEffects.Move);
                DisplayParking.Remove(CurrentParking);
            }
        }
    }
}
