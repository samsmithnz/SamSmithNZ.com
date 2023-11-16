using SamSmithNZ.Service.Models.FooFighters;
using System.Collections.Generic;

namespace SamSmithNZ.Web.Models.FooFighters
{
    public class SongViewModel
    {
        public Song Song { get; set; }
        public List<Show> Shows { get; set; }
    }
}
