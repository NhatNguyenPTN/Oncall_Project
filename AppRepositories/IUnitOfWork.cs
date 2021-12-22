using Infrastructure.Repositories;

namespace Infrastructure
{
    public interface IUnitOfWork
    {
        //public UserContext DbContext { get; }
        public IUserRepository UserRepository { get; }
        public int SaveChanges();
        //public void Dispose();
    }
}
