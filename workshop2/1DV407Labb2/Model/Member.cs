using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace _1DV407Labb2.Model
{
    public class Member  : ICloneable , INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        // TODO: Better checks when setting values for:
        // name
        // social security number
        // memberID
        public BoatManager BoatManager;
        private string name;
        private string socialSecurityNumber;
        private int id;
        public int Id
        {
            get
            {
                return id;
            }
            //TODO: Should be private, but that doesn't work with serializer
            set
            {
                id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            //TODO: Should be private, but that doesn't work with serializer
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
                else
                {
                    throw new FormatException("Name cannot be empty");
                }
            }
        }

        public string SocialSecurityNumber
        {
            get
            {
                return socialSecurityNumber;
            }
            //TODO: Should be private, but that doesn't work with serializer
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    socialSecurityNumber = value;
                    OnPropertyChanged("SocialSecurityNumber");
                }
                else
                {
                    throw new FormatException("Social security number must be entered.");
                }

            }
        }

        public Member() 
            : this("<missing>", "0000000000", 0)
        {
        }
        public Member(string name, string socialSecurityNumber, int memberId)
        {
            BoatManager = new BoatManager();
            Id = memberId;
            Name = name;
            SocialSecurityNumber = socialSecurityNumber;
        }

        public void Update(string name, string socialSecurityNumber)
        {
            Name = name;
            SocialSecurityNumber = socialSecurityNumber;
        }

        public object Clone()
        {
            //TODO tror det behöve en "DEEP clone" här...
            return this.MemberwiseClone();
        }
    }
}
