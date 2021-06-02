using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public class ParkingModel : ValidationBase
    {
        private int id;
        private string name;
        private Type typeP;
        private int value;

        public ParkingModel() { }
        public ParkingModel(int id) { this.id = id; }
        public ParkingModel(ParkingModel p) { Id = p.Id; Name = p.Name; Value = p.Value; TypeP = p.TypeP; }
        public int Id
        {
            get { return id; }
            set { if(this.id != value) { this.id = value;  RaisePropertyChanged("Id"); } }
        }
        public string Name
        {
            get { return name; }
            set { if (this.name != value) { this.name = value; RaisePropertyChanged("Name"); } }
        }
        public int Value
        {
            get { return value; }
            set { if (this.value != value) { this.value = value; RaisePropertyChanged("Value"); } }
        }
        public Type TypeP
        {
            get { return typeP; }
            set { typeP = value; RaisePropertyChanged("TypeP"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return "[" + id + "]" + "_" + "[" + name + "]";
        }

        protected override void ValidateSelf()
        {
            foreach (ParkingModel p in ViewModel.ParkingViewModel.Parkings)
            {
                if (this.id == p.Id)
                {
                    this.ValidationErrors["Id"] = "ERROR -> exists!";
                }
            }
            if (this.id < 0 || string.IsNullOrWhiteSpace(this.id.ToString()))
            {
                this.ValidationErrors["Id"] = "ERROR -> invalid!";
            }
            if (string.IsNullOrWhiteSpace(this.name))
            {
                this.ValidationErrors["Name"] = "ERROR -> empty!";
            }
        }

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

    }
}
