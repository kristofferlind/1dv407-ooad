using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace _1DV407Labb2.Model
{
    public class Member : ICloneable, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

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
            //Supposed be private, but that doesn't work with serializer
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
            //Supposed be private, but that doesn't work with serializer
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
                else
                {
                    name = "<missing>";
                }
            }
        }

        public string SocialSecurityNumber
        {
            get
            {
                return socialSecurityNumber;
            }
            //Supposed be private, but that doesn't work with serializer
            set
            {  
                var re = new Regex(@"^(?:\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[1-2]\d|3[0-1])\-?\d{4}|\d{3}\-?\d{2}\-?\d{4})$");

                if (!String.IsNullOrWhiteSpace(value) || re.IsMatch(value))
                {
                    socialSecurityNumber = value;
                    OnPropertyChanged("SocialSecurityNumber");
                }
                else
                {
                    socialSecurityNumber = "<missing>"; 
                }

            }
        }

        public Member() 
            : this("<missing>", "", 0)
        {
        }
        public Member(string name, string socialSecurityNumber, int memberId)
        {
            BoatManager = new BoatManager();
            Id = memberId;
            Name = name;
            SocialSecurityNumber = socialSecurityNumber;

            BoatManager.PropertyChanged += BoatManager_PropertyChanged;
        }

        void BoatManager_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged("BoatManager");
        }

        public void Update(string name, string socialSecurityNumber)
        {
            Name = name;
            SocialSecurityNumber = socialSecurityNumber;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void InitiatePropertyChangedEventHandler()
        {
            BoatManager.InitiateListChangedEventHandler();
            BoatManager.PropertyChanged += BoatManager_PropertyChanged;
        }
    }
}
