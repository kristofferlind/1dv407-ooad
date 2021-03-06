﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV407Labb2.Model
{
    public class Boat : ICloneable, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }


        private double length;

        /// <summary>
        /// Allowed between 1 and 100 meters.
        /// </summary>
        public double Length
        {
            get { return length; }
            set
            {
                if (value >= 1.0 || value <= 100.0)
                {
                    length = value;
                    OnPropertyChanged("Length");
                }
                else
                {
                    length = 0.0; //<missing>
                }
            }
        }

        public Boat() : this(1, BoatType.Other)
        {
        }

        public Boat(double length, BoatType type)
        {
            Length = length;
            BoatType = type;
        }

        public void Update(double length, BoatType type)
        {
            Length = length;
            BoatType = type;
        }

        public BoatType BoatType { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
