using BiblioTechData.Models;
using BiblioTechData.Repositories.IRepository;

namespace BiblioTechData.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(BiblioTechContext context) : base(context) { }
    }
}
