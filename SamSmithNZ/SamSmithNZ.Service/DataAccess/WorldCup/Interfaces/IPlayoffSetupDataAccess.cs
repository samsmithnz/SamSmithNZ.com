using SamSmithNZ.Service.Models.WorldCup;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup.Interfaces
{
    public interface IPlayoffSetupDataAccess
    {
        Task<bool> SaveItem(PlayoffSetup setup);
    }
}