using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SamSmithNZ2017.Models.GameDev
{
    public class LevelCreationModel
    {
        public LevelCreationModel(string content)
        {
            this.Content = content;
        }

        public string Content { get; private set; }
    }
}