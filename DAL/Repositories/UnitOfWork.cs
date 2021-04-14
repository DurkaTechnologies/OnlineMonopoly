using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
namespace DAL
{
    public class UnitOfWork : IDisposable
    {
        private MonopolyDbContext context = new MonopolyDbContext();

        private GenericRepository<Branch> branchRepository;
        private GenericRepository<BranchType> branchTypeRepository;
        private GenericRepository<RentSetting> rentRepository;
        private GenericRepository<User> userRepository;

		public UnitOfWork()
		{
            
		}

        public GenericRepository<Branch> BranchRepository
        {
            get
            {
                if (branchRepository == null)
                    branchRepository = new GenericRepository<Branch>(context);
                return branchRepository;
            }
        }
        public GenericRepository<BranchType> BranchTypeRepository
        {
            get
            {
                if (branchTypeRepository == null)
                    branchTypeRepository = new GenericRepository<BranchType>(context);
                return branchTypeRepository;
            }
        }
        public GenericRepository<RentSetting> RentRepository
        {
            get
            {
                if (rentRepository == null)
                    rentRepository = new GenericRepository<RentSetting>(context);
                return rentRepository;
            }
        }
        public GenericRepository<User> UserRepository
        {
            get
            {
                if (userRepository == null)
                    userRepository = new GenericRepository<User>(context);
                return userRepository;
            }
        }
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
