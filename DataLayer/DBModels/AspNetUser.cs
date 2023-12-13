
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.DBModels
{
    public partial class AspNetUser : IdentityUser
    {
        public AspNetUser()
        {

        }

        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public bool IsActive { get; set; }
        public string? ProfilePicture { get; set; } = null!;
        public DateTime MembershipDateTime { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string? AuthorizationCode { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime AuthorizationExpireDate { get; set; }

        public int ActiveSubscriptionId { get;set; }


        public virtual ICollection<UserJwtToken> UserJwtTokens { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }

    }
}
