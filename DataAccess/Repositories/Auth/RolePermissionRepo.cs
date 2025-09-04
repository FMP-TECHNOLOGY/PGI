using PGI.Common.Exceptions;
using PGI.DataAccess.DbContenxts;
using PGI.DataAccess.Entities;

namespace PGI.DataAccess.Repositories.Auth
{
    public interface IRolePermission : IGenericRepo<RolePermission>
    {
        public void AddPermission(string? roleId, string? permissionId);
        public void RemovePermission(string? roleId, string? permissionId);
    }

    public class RolePermissionRepo : GenericRepo<RolePermission>, IRolePermission
    {
        private readonly IPermission permissionRepo;
        private readonly IRole roleRepo;
        public RolePermissionRepo(AppDBContext context,
                                        IRole roleRepo,
                                        IPermission permissionRepo) : base(context)
        {
            this.permissionRepo = permissionRepo;
            this.roleRepo = roleRepo;
        }

        public void AddPermission(string? roleId, string? permissionId)
        {
            if (Exists(x => x.PermissionId == permissionId && x.RoleId == roleId))
                throw new BadRequestException("Already assigned permission");

            var role = roleRepo.Find(x => x.Id == roleId && x.Active)
                ?? throw new BadRequestException("Invalid or locked role");

            var permission = permissionRepo.Find(x => x.Id == permissionId && x.Active)
                ?? throw new BadRequestException("Invalid or locked permission");

            AddSaving(new RolePermission()
            {
                PermissionId = permissionId,
                RoleId = roleId,
                Permission = permission,
                Role = role,
            });
        }

        public void RemovePermission(string? roleId, string? permissionId)
        {
            var rolePermission = Find(x => x.RoleId == roleId && x.PermissionId == permissionId)
                ?? throw new NotFoundException("Role-Permission not found");


            RemoveSaving(rolePermission);
        }
    }
}
