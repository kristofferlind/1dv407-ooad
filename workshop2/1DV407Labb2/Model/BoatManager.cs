using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV407Labb2.Model
{
    //TODO: Changing boats doesn't trigger save
    public class BoatManager : INotifyPropertyChanged
    {
        //Observablecollection som anropar propertychanged? ^o)
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        //private List<Boat> boats;
        public List<Boat> Boats { get; private set; }

        public BoatManager()
        {
            Boats = new List<Boat>();
        }
        public void RemoveBoat(Boat boat)
        {
            Boats.Remove(boat);
            //boats.RemoveAt(index);
            OnPropertyChanged("Boat");
        }

        public int AddBoat(double length, BoatType type)
        {
            var boat = new Boat(length, type);
            Boats.Add(boat);
            OnPropertyChanged("Boat");
            return Boats.Count - 1;
        }

        public ReadOnlyCollection<Boat> GetBoatList()
        {
            return Boats.AsReadOnly();
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

            var boat = Boats.Find(b => b == boatInfo);
            boat.Update(length, type);
            OnPropertyChanged("Boat");
        }
    }
}
