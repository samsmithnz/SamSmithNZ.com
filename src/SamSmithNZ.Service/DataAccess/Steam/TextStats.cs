using System.Collections.Generic;
using System.Linq;

namespace SamSmithNZ.Service.DataAccess.Steam
{
    public class TextStats
    {

        public Dictionary<string, int> TextStatistics = new();
        private readonly List<string> _blackList = new();
        private readonly bool _filterNumbers;

        public TextStats(List<string> blackList, bool filterNumbers)
        {
            TextStatistics = new();
            _blackList = blackList;
            _filterNumbers = filterNumbers;
        }

        public bool AddItem(string text, char splitChar)
        {

            text = text.Replace(".", "").Replace(",", "").ToLower();
            string[] splitItems = text.Split(splitChar);
            foreach (string splititem in splitItems)
            {
                int intresult;
                //Filter out "ACHIEVEMENT" and numbers
                if (_blackList.Contains(splititem.ToLower()) == true || (_filterNumbers == true && int.TryParse(splititem, out intresult) == true))
                {
                    //do nothing
                }
                else
                {
                    if (TextStatistics.ContainsKey(splititem))
                    {
                        TextStatistics[splititem] = TextStatistics[splititem] + 1;
                    }
                    else if (string.IsNullOrEmpty(splititem) == false && splititem.Length >= 3)
                    {
                        TextStatistics.Add(splititem, 1);
                    }
                }
            }
            return true;
        }

        public List<KeyValuePair<string, int>> SortList(bool sortAsc)
        {
            List<KeyValuePair<string, int>> myList = TextStatistics.ToList();
            //if (sortAsc == true)
            //{
            //    myList.Sort((x, y) => x.Value.CompareTo(y.Value));
            //}
            //else
            //{
            myList.Sort((x, y) => y.Value.CompareTo(x.Value));
            //}
            return myList;
        }
    }
}
