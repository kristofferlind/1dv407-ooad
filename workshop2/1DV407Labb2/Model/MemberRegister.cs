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
using System.Text.RegularExpressions;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;


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

        public int LastMemberId
        {
            get { return lastMemberId; }
            set
            {
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

        private List<String> SplitExpression(string expression)
        {
            Regex re = new Regex(@"\s*([()]|AND|X?OR|\w+=[\w\s]+?(?=\s*(?:[()]|AND|X?OR)))\s*"); //tillåter inte mellanslag just nu
            var matches = re.Matches(expression).OfType<Match>().Select(m => m.Groups[1].Value).ToList();

            return matches;
        }
        private bool IsNextACriteria(string partOfExpression)
        {
            Regex re = new Regex(@"^\w+=[^\s]+$"); //tillåter inte mellanslag just nu
            return re.IsMatch(partOfExpression);
        }
        private Search.ICriteria GetNextCriteria(string partOfExpression)
        {
            Regex re = new Regex(@"^(\w+=[^\s]+)$"); //tillåter inte mellanslag just nu
            string match = re.Match(partOfExpression).Groups[1].Value;

            Regex reCommand = new Regex(@"^(\w+)=");
            Regex reValue = new Regex(@"=(\w+)$");
            string matchCommand = reCommand.Match(match).Groups[1].Value;
            string matchValue = reValue.Match(match).Groups[1].Value;

            Search.ICriteria criteria = null;

            switch (matchCommand)
            {
                case "ageover":
                    criteria = new Search.AgeOverCriteria(int.Parse(matchValue)); //kan kasta undantag
                    break;
                case "ageunder":
                    criteria = new Search.AgeUnderCriteria(int.Parse(matchValue)); //kan kasta undantag
                    break;
                case "namestartswith":
                    criteria = new Search.NameStartsWithCriteria(matchValue);
                    break;
                case "namelike":
                    criteria = new Search.NameLikeCriteria(matchValue);
                    break;
                case "nameendswith":
                    criteria = new Search.NameEndsWithCriteria(matchValue);
                    break;
                case "birthmonth":
                    criteria = new Search.BirthMonthCriteria(matchValue);
                    break;
                default:
                    throw new FormatException("Illegal command found in expression");
            }
            
            return criteria;
        }

        private bool IsNextAFilter(string partOfExpression)
        {
            Regex re = new Regex(@"^(?:AND|X?OR)$");
            return re.IsMatch(partOfExpression);
        }

        private Search.IFilter GetNextFilter(string partOfExpression)
        {
            Regex re = new Regex(@"^(AND|X?OR)$");
            string match = re.Match(partOfExpression).Groups[1].Value;

            Search.IFilter filter = null;

            switch (match)
            {
                case "AND":
                    filter = new Search.AndFilter();
                    break;
                case "OR":
                    filter = new Search.OrFilter();
                    break;
                case "XOR":
                    filter = new Search.XorFilter();
                    break;
                default:
                    throw new Exception("Incorrect expression. Guess again :P");
            }

            return filter;
        }

        public ReadOnlyCollection<Member> FilterMembers(string expression)
        {
            //expression = "ageover=20 OR ageunder=40 AND (namestartswith=Kri OR namestartswith=Han)";        //prickar mer än hannes och kristoffer (även anton och nisse, AND failar?
            //expression = "ageover=20 AND birthmonth=jan OR (namestartswith=Han) /";

            //exempelsökningen
            //(månad == Jan || (namn == “ni*” && ålder > 18))
            expression = "birthmonth=jan OR (namestartswith=ni AND ageover=18)";        //borde pricka nisse
            
            List<String> splitExpression = SplitExpression(expression);
            return EvaluatorRunner(splitExpression).AsReadOnly();
        }

        private bool IsNextAStartParenthesis(string partOfExpression)
        {
            return (partOfExpression.Equals("("));
        }

        private bool IsNextAnEndParenthesis(string partOfExpression) //borde nog bara skicka med en enda sträng))
        {
            return (partOfExpression.Equals(")"));
        }

        private List<Member> RecursiveEvaluator(List<String> splitExpression, List<Member> previousMemberList)
        {
            var memberList = GetMemberList().ToList<Member>();
            var tempList = new List<Member>();

            //criteria?
            //all we want to do here is filter by criteria and return list
            if (IsNextACriteria(splitExpression[0]))
            {
                var criteria = GetNextCriteria(splitExpression[0]);
                splitExpression.RemoveAt(0);
                tempList = criteria.Filter(memberList).ToList<Member>();
                return tempList;
            }
            //filter?
            //we need to get the result of the next "statement" and evaluate with previous, then return
            else if (IsNextAFilter(splitExpression[0]))      //Fail med enbart ett uttryck
            {
                var filter = GetNextFilter(splitExpression[0]);
                splitExpression.RemoveAt(0);
                
                //get result of next statement
                tempList = RecursiveEvaluator(splitExpression, null);

                //run filter for next an previous results
                var members = filter.CompareList(previousMemberList, tempList);
                tempList = members.ToList<Member>();

                //return results
                return tempList;
            }
            // ( ?
            //we want to evaluate everything up to the ending parenthesis
            else if (IsNextAStartParenthesis(splitExpression[0]))
            {
                splitExpression.RemoveAt(0);

                //Get value from entire parenthesis
                while (!IsNextAnEndParenthesis(splitExpression[0]))
                {
                    tempList = RecursiveEvaluator(splitExpression, previousMemberList);
                    previousMemberList = tempList;
                }
                return tempList;
            }
            // ) ?
            //This is the end of parenthesis, now we want the entire result..
            else if (IsNextAnEndParenthesis(splitExpression[0]))
            {
                //what to do?
                splitExpression.RemoveAt(0);
                return previousMemberList;
            }
            //nothing else to do? (shouldn't have run? throw exception?
            else
            {
                throw new Exception("Something weird happened");
            }
        }

        private List<Member> EvaluatorRunner(List<string> splitExpression)
        {
            List<Member> oldList = GetMemberList().ToList<Member>();
            List<Member> newList = null;

            while (splitExpression.Count > 0)
            {
                newList = RecursiveEvaluator(splitExpression, oldList);
                oldList = newList;
            }

            return newList;
        }
    }
}