using SamSmithNZ.Service.Models.FooFighters;
using System.Collections.Generic;

namespace SamSmithNZ.Web.Models.FooFighters
{
    public class ShowViewModel
    {
        public Show Show { get; set; }
        public List<Song> Songs { get; set; }
    }
}
