
using Microsoft.EntityFrameworkCore;
using DataAccess.Entities;
using DataAccess;

namespace PGI.DataAccess.Repositories.Auth
{
    public interface IUser : IGenericRepo<User>
    {
        //List<User> FindAllByCompany(int? CompaniaId);
        User? FindByUsername(string? username);
        User? FindValidByUsername(string? username);
        User? FindValidById(string? userId);

    }

    public class UserRepo : GenericRepo<User>, IUser
    {
        public UserRepo(PGIContext context) : base(context)
        {
        }

        public  User UpdateSaving(User entity)
        {
            var user = base.UpdateSaving(entity);

            return user;
        }

        public User? FindByUsername(string? username)
        {
           
                 
            return (from t0 in EntityDbSet
                    where t0.Username == username
                    select t0).FirstOrDefault();
        }

        public User? FindValidById(string userId)
            => EntityDbSet//.AsNoTrackingWithIdentityResolution()
                          //.Include(x => x.Roles)
                          //.Include(x => x.ModulePermissions)
            .Include(x => x.Companies)
            .FirstOrDefault(x => x.Id.ToString() == userId && x.Active && x.LockoutEnabled == false && x.LockoutDueDate == null);

        public User? FindValidByUsername(string? username)
            => (from t0 in EntityDbSet
                where t0.Username == username
                select t0)
            .Include(x => x.Companies)
            .FirstOrDefault(x => x.Active && x.LockoutEnabled == false && x.LockoutDueDate == null);

        //public override User? Find(Func<User, bool> predicate)
        //    => EntityDbSet//.AsNoTrackingWithIdentityResolution()
        //                  //.Include(x => x.Roles)
        //                  //.Include(x => x.ModulePermissions)
        //                  //.Include(x => x.Companies)
        //    .FirstOrDefault(predicate);

        /*public List<User> FindAllByCompany(int? CompaniaId)
            => (from userCompany in context.UserCompanies
                join user in context.Users on userCompany.UserId equals user.Id
                where userCompany.CompaniaId == CompaniaId
                select user)
            .Distinct()
            .AsNoTrackingWithIdentityResolution()
            .ToList();*/

        public override User? Get(params object?[] keys)
        {
            var id = keys.FirstOrDefault()?.ToString();
            return Find(x => x.Id.ToString() == id);
        }

        /* public void UpdateCompany(string? userId, int CompaniaId)
         {
             var user = FindValidById(userId) ?throw new BadRequestException("Invalid user");

             if (!user.Active || user.LockoutEnabled || user.LockoutDueDate != null)
                 throw new BadRequestException("Invalid user");

             var companiesRepo = Companies.Find(x => x.Id == CompaniaId && x.Active) ?throw new BadRequestException("Invalid Company");

             user.CompaniaId = CompaniaId;

             UpdateSaving(user);
         }*/

        /*public void UpdateCompanies(string? userId, List<Company> companies)
        {
            try
            {
                var user = Get(userId) ?throw new BadRequestException("Invalid user");
                var userCompanies = UserCompanies.FindAll(x => x.UserId == user.Id);

                // ADDING ROLES
                companies.ForEach(companiesRepo =>
                {
                    var mCompany = Companies.Get(companiesRepo.Id) ?throw new BadRequestException($"Invalid companiesRepo {companiesRepo.Id}");

                    var userHasCompany = userCompanies.Any(x => x.UserId == user.Id && x.CompaniaId == mCompany.Id);

                    if (!mCompany.Active && !userHasCompany) throw new BadRequestException($"Inactive companiesRepo {mCompany.Name}");

                    if (!userHasCompany)
                        UserCompanies.AddSaving(new()
                        {
                            CompaniaId = mCompany.Id,
                            UserId = user.Id
                        });
                });

                // REMOVING COMPANIES
                userCompanies.ForEach(userCompany =>
                {
                    if (!companies.Any(x => x.Id == userCompany.CompaniaId))
                        UserCompanies.RemoveSaving(userCompany);
                });

                Save();
            }
            catch
            {
                throw;
            }
    }*/
    }
}
