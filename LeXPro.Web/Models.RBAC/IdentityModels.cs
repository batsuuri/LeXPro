using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using LeXPro.DatabaseInitializer;

namespace LeXPro.Models
{
    public class ApplicationUserLogin : IdentityUserLogin<int> { }
    public class ApplicationUserClaim : IdentityUserClaim<int> { }

    public class ApplicationUserRole : IdentityUserRole<int>
    {
        public ApplicationUserRole()
            : base()
        { }

        public ApplicationRole Role { get; set; }

        public bool IsPermissionInRole(string _permission)
        {
            bool _retVal = false;
            try
            {          
                _retVal = this.Role.IsPermissionInRole(_permission);             
            }
            catch (Exception)
            {
            }
            return _retVal;
        }

        public bool IsSysAdmin { get { return this.Role.IsSysAdmin; } }
    }
    
    public class ApplicationUser : IdentityUser<int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public DateTime LastModified { get; set; }

        public bool Inactive { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }
        public ApplicationUser()
        {
            LastModified = DateTime.Now;
            Inactive = false;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }


        //public virtual List<ApplicationRole> UserRoles { get; set; }


        public bool IsPermissionInUserRoles(string _permission)
        {
            bool _retVal = false;
            try
            {
                foreach (ApplicationUserRole _role in this.Roles)
                {
                    if (_role.IsPermissionInRole(_permission))
                    {
                        _retVal = true;
                        break;
                    }
                }
            }
            catch (Exception)
            {
            }
            return _retVal;
        }

        public bool IsSysAdmin()
        {
            bool _retVal = false;
            try
            {
                foreach (ApplicationUserRole _role in this.Roles)
                {
                    if (_role.IsSysAdmin)                    
                    {
                        _retVal = true;
                        break;
                    }
                }
            }
            catch (Exception)
            {
            }
            return _retVal;
        }
    }

    public class ApplicationRole : IdentityRole<int, ApplicationUserRole>
    {
        public ApplicationRole()
        {
            //this.Id = Guid.NewGuid().ToString();
        }
        public ApplicationRole(string name)
            : this()
        {
            this.Name = name;
        }

        public ApplicationRole(string name, string description)
            : this(name)
        {
            this.RoleDescription = description;
        }

        public DateTime LastModified { get; set; }
        public bool IsSysAdmin { get; set; }
        public string RoleDescription { get; set; }

        public virtual ICollection<PERMISSION> PERMISSIONS { get; set; }    

        public bool IsPermissionInRole(string _permission)
        {
            bool _retVal = false;
            try
            {
                foreach (PERMISSION _perm in this.PERMISSIONS)
                {
                    if (_perm.PermissionDescription == _permission)
                    {
                        _retVal = true;
                        break;
                    }
                }
            }
            catch (Exception)
            {
            }
            return _retVal;
        }
    }

    public class RBACDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {        
        public DbSet<PERMISSION> PERMISSIONS { get; set; }     

        public RBACDbContext() : base("DefaultConnection")
        {
            Database.SetInitializer<RBACDbContext>(new RBACDatabaseInitializer());

        }

        public static RBACDbContext Create()
        {
            return new RBACDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("USERS").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<ApplicationRole>().ToTable("ROLES").Property(p => p.Id).HasColumnName("RoleId");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("LNK_USER_ROLE");           

            modelBuilder.Entity<ApplicationRole>().
            HasMany(c => c.PERMISSIONS).
            WithMany(p => p.ROLES).
            Map(
                m =>
                {
                    m.MapLeftKey("RoleId");
                    m.MapRightKey("PermissionId");
                    m.ToTable("LNK_ROLE_PERMISSION");
                });            
        }
    }
}