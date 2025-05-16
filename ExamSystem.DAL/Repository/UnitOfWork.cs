using ExamSystem.DAL.Context;
using ExamSystem.Domains;
using Microsoft.EntityFrameworkCore.Storage;

namespace ExamSystem.DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MainContext _context;
        private IDbContextTransaction _transaction;
        public IRepository<TbExams> Exams { get; private set; }
        public IRepository<TbQuestions> Questions { get; private set; }
        public IRepository<TbUserExams> UserExams { get; private set; }

        public UnitOfWork(MainContext context)
        {
            _context = context;
            Exams = new GenericRepository<TbExams>(context);
            Questions = new GenericRepository<TbQuestions>(context);
            UserExams = new GenericRepository<TbUserExams>(context);
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        } 
        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

        public async Task BeginTransactionAsync() 
        {
            if(_transaction != null)
            {
                throw new InvalidOperationException("A transaction is already in progress.");
            }

            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if(_transaction == null)
            {
                throw new InvalidOperationException("No transaction in progress");
            }

            try
            {
                await _transaction.CommitAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("No transaction in progress");
            }

            try
            {
                await _transaction.RollbackAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }
}
