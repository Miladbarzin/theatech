using DataLayer.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface IJWTAuthManager
    {
        Task<string> GenerateJWT(AspNetUser user);
        Task RemoveToken(string userId);
    }
}
