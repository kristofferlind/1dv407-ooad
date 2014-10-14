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
using System.ComponentModel;


namespace _1DV407Labb2.Model
{
    [XmlRoot(ElementName = "MemberRegister")]
    public class MemberRegister
    {
        public BindingList<Member> Members { get; private set; }
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
            Members = new BindingList<Member>();

            filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "memberregister.xml");

            //hard coded credentials for simplicity
            username = "Admin";
            password = "Password";
        }

        /// <summary>
        /// Initiating event listeners for the entire collection
        /// of deserialized objects all the way down to BoatManager
        /// for automatic save when changes are made.
        /// </summary>
        private void InitiateCollectionChangedEventHandler()
        {
            for (int i = 0; i < Members.Count; i++)
            {
                Members[i].InitiatePropertyChangedEventHandler();
            }
            Members.ListChanged += Members_ListChanged;
        }

        void Members_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e != null)
            {
                SaveMembers();
            }
        }

        private int getNewMemberId()
        {
            int highestMemberId = 0;
            
            if (Members.Count != 0)
            {
                highestMemberId = Members.Max(member => member.Id);
            }
            
            highestMemberId++;

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
            return Members.FirstOrDefault<Member>(member => member.Id == memberId);
        }

        public Member GetMemberInfo(int memberId)
        {
            return (Member)GetMember(memberId).Clone();
        }

        public void DeleteMember(Member memberInfo)
        {
            var member = GetMember(memberInfo.Id);
            Members.Remove(member);
        }

        public ReadOnlyCollection<Member> GetMemberList()
        {
            return Members.ToList().AsReadOnly();
        }

        public void LoadMembers()
        {
            List<Type> typeList = new List<Type>();
            typeList.AddRange(new Type[] { 
                typeof(Member), 
                typeof(MemberRegister),
                typeof(BoatManager),
                typeof(Boat)}); 
            
            if (File.Exists(filePath))
            {
                var memberRegister = Utils.XmlDeserialize<MemberRegister>(filePath, typeList.ToArray());
                Members = memberRegister.Members;
                lastMemberId = memberRegister.LastMemberId;
            }

            InitiateCollectionChangedEventHandler();
        }

        /// <summary>
        /// All Members and boats saved automatically on any change made.
        /// </summary>
        public void SaveMembers()
        {
            List<Type> typeList = new List<Type>();
            typeList.AddRange(new Type[] { 
                typeof(Member), 
                typeof(MemberRegister),
                typeof(BoatManager),
                typeof(Boat)});
            Utils.XmlSerialize(filePath, this, typeList.ToArray());
        }

        public bool IsMember(int memberId)
        {
            return GetMember(memberId) != null;
        }

        public void Authenticate(string username, string password)
        {
            if (this.username == username && this.password == password)
            {
                IsLoggedIn = true;
            }
            else
            {
                IsLoggedIn = false;
            }
        }

        public BoatManager GetBoatManager(Member memberInfo)
        {
            var member = GetMember(memberInfo.Id);
            return member.BoatManager;
        }

        public void UpdateMember(Member memberInfo, string name, string ssn)
        {
            var member = GetMember(memberInfo.Id);
            member.Update(name, ssn);
        }
    }
}