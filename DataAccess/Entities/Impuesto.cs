using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Impuesto : BaseSystemData
    {
        public override int ObjectType => 71;

        public decimal Value { get; set; }
    }
}
