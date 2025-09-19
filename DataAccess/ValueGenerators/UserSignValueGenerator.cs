using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using PGI.DataAccess.Repositories.Auth;

namespace DataAccess.ValueGenerators
{
    public class UserSignValueGenerator : ValueGenerator<string?>
    {
        public override bool GeneratesTemporaryValues { get; }

        public override string? Next(EntityEntry entry)
            => entry.Context.GetService<IAuth>()?.CurrentUser?.Id.ToString();
    }
    public class CompaniaSignValueGenerator : ValueGenerator<string?>
    {
        public override bool GeneratesTemporaryValues { get; }

        public override string? Next(EntityEntry entry)
            => entry.Context.GetService<IAuth>()?.CurrentCompany?.Id.ToString();
    }
    public class DireccionInstitucionalSignValueGenerator : ValueGenerator<string?>
    {
        public override bool GeneratesTemporaryValues { get; }

        public override string? Next(EntityEntry entry)
            => entry.Context.GetService<IAuth>()?.CurrentDireccionIntitucional?.Id.ToString();
    }
    
    public class SucursalSignValueGenerator : ValueGenerator<string?>
    {
        public override bool GeneratesTemporaryValues { get; }

        public override string? Next(EntityEntry entry)
            => entry.Context.GetService<IAuth>()?.CurrentSucursal?.Id.ToString();
    }
}
