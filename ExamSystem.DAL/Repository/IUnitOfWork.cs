using ExamSystem.Domains;

namespace ExamSystem.DAL.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TbExams> Exams { get; }
        IRepository<TbQuestions> Questions { get; }
        IRepository<TbUserAnswers> UserAnswers { get; }
        IRepository<TbUserExams> UserExams { get; }

        Task<int> SaveAsync();
        Task BeginTransactionAsync(); 
        Task CommitTransactionAsync(); 
        Task RollbackTransactionAsync();
    }
}
