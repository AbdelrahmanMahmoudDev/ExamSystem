using ExamSystem.Domains;

namespace ExamSystem.BL
{
    public interface IExamService
    {
        public Task<bool> SaveNew(TbExams newExam);
        public Task<bool> SaveUpdate(TbExams exam);
        public Task<IEnumerable<TbExams>> GetAllExams();
        public Task<TbExams> GetExamBasedOnId(int id);
        public Task<bool> DeleteExam(TbExams exam);
    }
}
