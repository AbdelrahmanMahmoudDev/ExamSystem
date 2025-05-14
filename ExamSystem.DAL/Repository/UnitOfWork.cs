using ExamSystem.DAL.Context;
using ExamSystem.Domains;

namespace ExamSystem.DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MainContext _context;
        public IRepository<TbExams> Exams { get; private set; }
        public IRepository<TbQuestions> Questions { get; private set; }
        public IRepository<TbUserAnswers> UserAnswers { get; private set; }
        public IRepository<TbUserExams> UserExams { get; private set; }

        public UnitOfWork(MainContext context)
        {
            _context = context;
            Exams = new GenericRepository<TbExams>(context);
            Questions = new GenericRepository<TbQuestions>(context);
            UserAnswers = new GenericRepository<TbUserAnswers>(context);
            UserExams = new GenericRepository<TbUserExams>(context);
        }

        public void Dispose() => _context.Dispose();
        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
    }
}
