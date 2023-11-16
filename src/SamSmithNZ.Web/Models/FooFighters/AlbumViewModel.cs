using SamSmithNZ.Service.Models.FooFighters;
using System.Collections.Generic;

namespace SamSmithNZ.Web.Models.FooFighters
{
    public class AlbumViewModel
    {
        public Album Album { get; set; }
        public List<Song> Songs { get; set; }
    }
}
