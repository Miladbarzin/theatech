using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DBModels
{
    public partial class UserJwtToken
    {
        public int Id { get; set; }
        public string TokenHash { get; set; }
        public DateTime? TokenExp { get; set; }
        public string UserId { get; set; }
    }
}
