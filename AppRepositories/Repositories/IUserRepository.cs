using EFCore.Model;

namespace Infrastructure.Repositories
{
    public interface IUserRepository: IGenericRepository<User>
    {
        public int a();
    }
}
