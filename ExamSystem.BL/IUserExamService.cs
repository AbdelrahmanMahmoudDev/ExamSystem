using ExamSystem.Domains.DTOs;

namespace ExamSystem.BL
{
    public interface IUserExamService
    {
        public Task<Tuple<double, int, int, bool>> AddAttemptToDb(string userId, ExamAttemptDTO examAttempt);
    }
}
