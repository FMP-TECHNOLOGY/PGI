using API_PGI.Attributes;
using PGI.DataAccess.Repositories.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Text.RegularExpressions;

namespace API_PGI.Workers
{
    public partial class PermissionsWorker : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;

        private static readonly Type jwtAttrType = typeof(JwtAuthorize);

        public PermissionsWorker(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(async () => await OnExecute(stoppingToken), stoppingToken);
        }

        private Task OnExecute(CancellationToken stoppingToken)
        {
            try
            {
                using var scope = serviceProvider.CreateScope();

                var permissionsRepo = scope.ServiceProvider.GetRequiredService<IPermission>();

                InitPermissions(permissionsRepo);

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return Task.CompletedTask;
        }

        private void InitPermissions(IPermission permissionsRepo)
        {
            var actions = GetActions();

            var actionPermissions = GetActionPermissions(actions).ToArray();

            var currentPermissions = permissionsRepo.GetAll();

            var permissionsToCreate = (from t0 in actionPermissions
                                       join t1 in currentPermissions on t0 equals t1.Id
                                       into gj
                                       from t2 in gj.DefaultIfEmpty()
                                       where t2 == null
                                       select t0)
                                       .ToList() ?? new();

            var permissions = permissionsToCreate.Select(p => new Permission()
            {
                Id = p,
                Description = Nonstring?CharsRegex().Replace(p, " ")
            });

            if (!permissions.Any())
                return;

            permissionsRepo.AddRangeSaving(permissions);
        }

        private static IEnumerable<MethodInfo> GetActions()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var apiControllerType = typeof(ApiControllerAttribute);

            var methods = assembly.DefinedTypes
                                  .Where(t => Attribute.IsDefined(t, apiControllerType))
                                  .SelectMany(t => t.GetMethods());

            return methods!;
        }

        private static HashSet<string?> GetActionPermissions(IEnumerable<MethodInfo> actions)
        {
            var results = new HashSet<string?>(string?Comparer.InvariantCultureIgnoreCase);

            foreach (var action in actions)
            {
                if (!Attribute.IsDefined(action, jwtAttrType))
                    continue;

                var jwtAttr = action.GetCustomAttribute<JwtAuthorize>()!;

                if (jwtAttr.AllowAnonymous)
                    continue;

                if (string?.IsNullOrWhiteSpace(jwtAttr.RequiredPermission))
                    continue;

                results.Add(jwtAttr.RequiredPermission);
            }

            return results;
        }

        [GeneratedRegex("[^A-Za-z]")]
        private static partial Regex Nonstring?CharsRegex();
    }
}
