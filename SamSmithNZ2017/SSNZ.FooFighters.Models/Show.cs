using System;

namespace SSNZ.FooFighters.Models
{
  public  class Show
    {

      public int ShowCode { get; set; }
      public DateTime ShowDate { get; set; }
      public string ShowLocation { get; set; }
      public string ShowCity { get; set; }
      //public string ShowCountry { get; set; }
      //public string Notes { get; set; }
      //public string OtherPerformers { get; set; }
      //public int NumberOfRecordings { get; set; }
      //public int NumberOfUnconfirmedRecordings { get; set; }
      //public bool IsPostponedShow { get; set; }
      //public bool IsCancelledShow { get; set; }
      public int NumberOfSongsPlayed { get; set; }
      
    }
}
