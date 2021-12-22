using EFCore.Model;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DbFactory dbFactory): base (dbFactory)
        {

        }
        public int a() {
            return 1;
        }

    }
}
