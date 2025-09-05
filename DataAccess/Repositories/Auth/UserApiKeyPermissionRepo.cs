//using PGI.Common.Exceptions;
//using PGI.DataAccess.DbContenxts;
//using PGI.DataAccess.Entities;

//namespace PGI.DataAccess.Repositories.Auth
//{
//    public interface IUserApiKeyPermission : IGenericRepo<UserApiKeyPermission>
//    {
//        public void AddPermission(string? apiKeyId, string? permissionId);
//        public void RemovePermission(string? apiKeyId, string? permissionId);
//    }

//    public class UserApiKeyPermissionRepo : GenericRepo<UserApiKeyPermission>, IUserApiKeyPermission
//    {
//        private readonly IUserApiKey apiKeyRepo;
//        private readonly IPermission permissionRepo;

//        public UserApiKeyPermissionRepo(AppDBContext context,
//                                        IUserApiKey apiKeyRepo,
//                                        IPermission permissionRepo) : base(context)
//        {
//            this.apiKeyRepo = apiKeyRepo;
//            this.permissionRepo = permissionRepo;
//        }

//        public void AddPermission(string? apiKeyId, string? permissionId)
//        {
//            if (Exists(x => x.PermissionId == permissionId && x.ApiKeyId == apiKeyId))
//                throw new BadRequestException("Already assigned permission");

//            var apikey = apiKeyRepo.Find(x => x.Id == apiKeyId && x.Active)
//                ?throw new BadRequestException("Invalid or locked Api-Key");

//            var permission = permissionRepo.Find(x => x.Id == permissionId && x.Active)
//                ?throw new BadRequestException("Invalid or locked permission");

//            AddSaving(new UserApiKeyPermission()
//            {
//                PermissionId = permissionId,
//                ApiKeyId = apiKeyId,
//                Permission = permission,
//                UserApiKey = apikey,
//            });
//        }

//        public void RemovePermission(string? apiKeyId, string? permissionId)
//        {
//            var apiKeyPermission = Find(x => x.ApiKeyId == apiKeyId && x.PermissionId == permissionId)
//                ?throw new NotFoundException("ApiKey-Permission not found");


//            RemoveSaving(apiKeyPermission);
//        }
//    }
//}
