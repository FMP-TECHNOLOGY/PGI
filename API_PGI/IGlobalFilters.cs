using DataAccess;

namespace API_PGI
{
    public class GlobalFilters : IGlobalFilters
    {
        public bool Expand { get; set; } = false;
    }
}
