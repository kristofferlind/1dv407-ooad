using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace _1DV407Labb2.Model
{
    public class BoatManager : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public BindingList<Boat> Boats { get; private set; }

        public BoatManager()
        {
            Boats = new BindingList<Boat>();
            InitiateListChangedEventHandler();
        }

        public void InitiateListChangedEventHandler()
        {
            Boats.ListChanged += Boats_ListChanged;
        }

        void Boats_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e != null)
            {
                OnPropertyChanged("Boats");
            }
        }

        public void RemoveBoat(Boat boat)
        {
            Boats.Remove(boat);
        }

        public int AddBoat(double length, BoatType type)
        {
            var boat = new Boat(length, type);
            Boats.Add(boat);
            return Boats.Count - 1;
        }

        public ReadOnlyCollection<Boat> GetBoatList()
        {
            return Boats.ToList().AsReadOnly();
        }

        public bool HasBoat(int boatIndex)
        {
            return boatIndex < Boats.Count;
        }

        public Boat GetBoat(int boatIndex)
        {
            return Boats.ElementAt(boatIndex);
        }

        public void Update(Boat boatInfo, double length, BoatType type)
        {
            var boat = Boats.FirstOrDefault<Boat>(b => b == boatInfo);
            boat.Update(length, type);
        }
    }
}
