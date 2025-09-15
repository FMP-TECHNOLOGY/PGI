using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ValueGenerators
{
    public class StringGuidValueGenerator : ValueGenerator<string>
    {
        public override bool GeneratesTemporaryValues { get; }

        public override string Next(EntityEntry entry) => Guid.NewGuid().ToString();
    }
}
