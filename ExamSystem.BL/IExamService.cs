using ExamSystem.Domains;

namespace ExamSystem.BL
{
    public interface IExamService
    {
        public Task<bool> SaveNew(TbExams newExam);
    }
}
