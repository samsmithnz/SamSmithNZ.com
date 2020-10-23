using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.Steam
{
    public class TextStats
    {

        public Dictionary<string, int> TextStatistics = new Dictionary<string, int>();
        private List<string> _blackList = new List<string>();
        private bool _filterNumbers;

        public TextStats(List<string> blackList, bool filterNumbers)
        {
            this.TextStatistics = new Dictionary<string, int>();
            this._blackList = blackList;
            this._filterNumbers = filterNumbers;
        }

        public bool AddItem(string text, char splitChar)
        {

            text = text.Replace(".", "").Replace(",", "").ToLower();
            string[] splitItems = text.Split(splitChar);
            foreach (string splititem in splitItems)
            {
                int intresult;
                //Filter out "ACHIEVEMENT" and numbers
                if (this._blackList.Contains(splititem.ToLower()) == true || (this._filterNumbers == true && int.TryParse(splititem, out intresult) == true))
                {
                    //do nothing
                }
                else
                {
                    if (this.TextStatistics.ContainsKey(splititem))
                    {
                        this.TextStatistics[splititem] = this.TextStatistics[splititem] + 1;
                    }
                    else if (string.IsNullOrEmpty(splititem) == false && splititem.Length >= 3)
                    {
                        this.TextStatistics.Add(splititem, 1);
                    }
                }
            }
            return true;
        }

        public List<KeyValuePair<string, int>> SortList(bool sortAsc)
        {
            List<KeyValuePair<string, int>> myList = this.TextStatistics.ToList();
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
