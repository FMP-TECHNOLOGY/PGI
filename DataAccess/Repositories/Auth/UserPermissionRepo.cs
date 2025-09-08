
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using DataAccess.Entities;
using DataAccess;
using Microsoft.IdentityModel.SecurityTokenService;

namespace PGI.DataAccess.Repositories.Auth
{
    public interface IUserPermission : IGenericRepo<UserPermission>
    {
        public void AddPermission(string? username, string? permissionId);
        public void RemovePermission(string? username, string? permissionId);
    }

    public class UserPermissionRepo : GenericRepo<UserPermission>, IUserPermission
    {
        private readonly IUser usersRepo;
        private readonly IPermission permissionsRepo;

        public UserPermissionRepo(PGIContext context,
                                  IUser usersRepo,
                                  IPermission permissionsRepo
            ) : base(context)
        {
            this.usersRepo = usersRepo;
            this.permissionsRepo = permissionsRepo;
        }

        public void AddPermission(string? username, string? permissionId)
        {
            var user = usersRepo.FindValidByUsername(username)
                ??throw new BadRequestException("Invalid or locked user");

            if (Find(x => x.PermissionId == permissionId && x.UserId == user.Id)==null)
                throw new BadRequestException("Already assigned permission");

            var permission = permissionsRepo.Find(x => x.Id == permissionId && x.Active)
                ??throw new BadRequestException("Invalid or locked permission");

            AddSaving(new UserPermission()
            {
                PermissionId = permissionId,
                UserId = user.Id,
                //Permission = permission,
                
                //User = user,
            });
        }

        public void RemovePermission(string? username, string? permissionId)
        {
            var user = usersRepo.FindValidByUsername(username)
                ?? throw new BadRequestException("Invalid or locked user");

            var userPermission = Find(x => x.UserId == user.Id && x.PermissionId == permissionId)
                ??throw new Exception("User-Permission not found");

            RemoveSaving(userPermission);

            //var apiKeyPermissions = GetUserApiKeyPermissions(user.Id);

            //if (!apiKeyPermissions.Any())
            //    return;

            //var apiKeyPermissionsRepo = context.GetService<IUserApiKeyPermission>()
            //      ?throw new InvalidOperationException($"Cannot get {nameof(IUserApiKeyPermission)} repo");

            //apiKeyPermissions.ForEach(apiKeyPermission => apiKeyPermissionsRepo.RemovePermission(apiKeyPermission.ApiKeyId, apiKeyPermission.PermissionId));
        }

        //private List<UserApiKeyPermission> GetUserApiKeyPermissions(string? userId)
        //{
        //    return (from t0 in context.Set<UserApiKey>()
        //            join t1 in context.Set<UserApiKeyPermission>()
        //                 on new { ApiKeyId = t0.Id, t0.UserId } equals new { t1.ApiKeyId, UserId = userId }
        //            select t1)
        //            .IgnoreAutoIncludes()
        //            .AsNoTracking()
        //            .ToList() ?new List<UserApiKeyPermission>();
        //}
    }
}
