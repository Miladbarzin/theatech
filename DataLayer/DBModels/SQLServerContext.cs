using System;
using System.Collections.Generic;
using CoreLayer.Setting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataLayer.DBModels
{
    public partial class SQLServerContext : IdentityDbContext<AspNetUser>
    {
        public SQLServerContext()
        {
        }

        public SQLServerContext(DbContextOptions<SQLServerContext> options)
            : base(options)
        {
        }

        
        public virtual DbSet<UserJwtToken> UserJwtTokens { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<Stock> Stocks { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            
			#region SeedData

			modelBuilder.Entity<IdentityRole>().HasData(
				new IdentityRole()
				{
					Id = "efda6afd-76ac-4a74-b99a-713b1acd1a34",
					Name = "Admin",
					NormalizedName = "ADMIN"
				},
				new IdentityRole()
                {
                    Id = "4f78f8ad-4ef8-4636-9332-18dacb7dbbda",
                    Name = "User",
					NormalizedName = "USER"
				});

			modelBuilder.Entity<AspNetUser>().HasData(new AspNetUser()
			{
				Id = "330f8e81-fec4-4d48-8774-d0b372a77764",
				Firstname = "Mohammad",
				Lastname = "Barzin",
				PhoneNumber = "09111111111",
				UserName = "09111111111",
				NormalizedUserName = "09111111111",
				PhoneNumberConfirmed = true,
				CreateDateTime = DateTime.Now,
				PasswordHash = "AQAAAAEAACcQAAAAEIZxwSkcjcbJbekWNFQeICW8RmAaSIkONkdXF+WQRUDfQv3WRmcWnUERY9PFZ3L8NA=="//K7nD404d<6.R'y
			});

			modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>()
			{
				RoleId = "efda6afd-76ac-4a74-b99a-713b1acd1a34",
				UserId = "330f8e81-fec4-4d48-8774-d0b372a77764"
            });

			#endregion
			base.OnModelCreating(modelBuilder);
        }
       
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
