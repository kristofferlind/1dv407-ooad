using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Specialized;

namespace _1DV407Labb2.Model
{
    [XmlRoot(ElementName = "MemberRegister")]
    public class MemberRegister
    {
        public ObservableCollection<Member> Members { get; private set; }
        private string password;
        private string username;
        
        [XmlIgnoreAttribute]
        public bool IsLoggedIn { get; private set; }

        private string filePath;

        private int lastMemberId;

        public int LastMemberId {
            get { return lastMemberId; }
            set {
                //private lastMemberId is set in LoadMembers().
                //This is a workaround for getting Serializer to work.
                //Serializer can't read from private lastMemberId.
                //Setting shouldn't be allowed here, so do nothing.
            }
        }

        public MemberRegister()
        {
            Members = new ObservableCollection<Member>();

            filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "memberregister.xml");

            //hard coded for simplicity
            username = "Admin";
            password = "Password";
        }

        private void InitiateCollectionChangeEventHandler()
        {
            Members.CollectionChanged += Members_CollectionChanged;
        }

        public void Members_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e != null)
            {
                SaveMembers();
            }
        }

        private int getNewMemberId()
        {
            int highestMemberId = Members.Max(member => member.Id);
            highestMemberId += 1;
            return highestMemberId;
        }

        public Member AddMember(string name, string socialSecurityNumber)
        {
            var member = new Member(name, socialSecurityNumber, getNewMemberId());
            Members.Add(member);
            return member;
        }

        private Member GetMember(int memberId)
        {
            return Members.Where<Member>(member => member.Id == memberId).Single<Member>();
        }

        public Member GetMemberInfo(int memberId)
        {
            return (Member)GetMember(memberId).Clone();
        }

        public void DeleteMember(Member member)
        {
            Members.Remove(member);
        }

        public ReadOnlyCollection<Member> GetMemberList()
        {
            return Members.ToList().AsReadOnly();
        }

        public void LoadMembers()
        {
            List<Type> typeList = new List<Type>();
            typeList.Add(typeof(Member)); //_tror_ jag har en metod som gör detta automatiskt
            typeList.Add(typeof(MemberRegister)); //vi kan köra såhär o testa att det funkar först
            typeList.Add(typeof(BoatManager));
            typeList.Add(typeof(Boat));
            if (File.Exists(filePath))
            {
                var memReg = Utils.XmlDeserialize<MemberRegister>(filePath, typeList.ToArray());
                Members = memReg.Members;
                lastMemberId = memReg.LastMemberId;
            }
            InitiateCollectionChangeEventHandler();
        }

        public void SaveMembers()
        {
            List<Type> typeList = new List<Type>();
            typeList.Add(typeof(Member));
            typeList.Add(typeof(MemberRegister));
            typeList.Add(typeof(BoatManager));
            typeList.Add(typeof(Boat));
            Utils.XmlSerialize(filePath, this, typeList.ToArray());
        }

        public bool IsMember(int memberId)
        {
            return GetMember(memberId) != null;            
        }

        public void Authenticate(string username, string password)
        {
            if (this.username == username && this.password == password) {
                IsLoggedIn = true;
            }
            else
            {
                IsLoggedIn = false;
            }
        }

        public BoatManager GetBoatManager(Member member)
        {
            var member2 = GetMember(member.Id);
            return member2.BoatManager;
        }
    }
}