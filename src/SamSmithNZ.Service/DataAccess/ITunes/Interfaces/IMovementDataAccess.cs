using Dapper;
using SamSmithNZ.Service.DataAccess.Base;
using SamSmithNZ.Service.DataAccess.ITunes.Interfaces;
using SamSmithNZ.Service.Models.ITunes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.ITunes.Interfaces
{
    public interface IMovementDataAccess
    {
        Task<List<Movement>> GetList(int playlistCode, bool showJustSummary);
        Task<List<Movement>> GetList(bool showJustSummary);
    }
}