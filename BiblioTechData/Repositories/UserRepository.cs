using BiblioTechData.Models;
using BiblioTechData.Repositories.IRepository;

namespace BiblioTechData.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(BiblioTechContext context) : base(context) { }                      
    }
}
