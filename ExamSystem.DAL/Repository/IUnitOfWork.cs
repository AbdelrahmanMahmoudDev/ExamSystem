using ExamSystem.Domains;

namespace ExamSystem.DAL.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TbExams> Exams { get; }
        IRepository<TbQuestions> Questions { get; }
        IRepository<TbUserExams> UserExams { get; }

        Task<int> SaveAsync();

        // Transactions won't work using an in-memory database
        // So these are useful, if the Sql Server db is toggled
        // and will crash otherwise
        Task BeginTransactionAsync(); 
        Task CommitTransactionAsync(); 
        Task RollbackTransactionAsync();
    }
}
