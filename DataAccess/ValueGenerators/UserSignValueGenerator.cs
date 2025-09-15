using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using PGI.DataAccess.Repositories.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ValueGenerators
{
    public class UserSignValueGenerator : ValueGenerator<string?>
    {
        public override bool GeneratesTemporaryValues { get; }

        public override string? Next(EntityEntry entry)
            => entry.Context.GetService<IAuth>()?.CurrentUser?.Id.ToString();
    }
}
