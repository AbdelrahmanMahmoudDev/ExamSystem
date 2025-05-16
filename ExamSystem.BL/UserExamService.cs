using System.Diagnostics;
using ExamSystem.DAL.Repository;
using ExamSystem.Domains;
using ExamSystem.Domains.DTOs;

namespace ExamSystem.BL
{
    public class UserExamService : IUserExamService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserExamService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private Tuple<double, int, int, bool> AssesExam(ExamAttemptDTO examAttempt)
        {
            int correctAnswers = 0;
            foreach (var question in examAttempt.Questions)
            {
                if (question.CorrectChoice == question.UserChoice)
                {
                    correctAnswers++;
                }
            }

            int incorrectAnswers = examAttempt.Questions.Count - correctAnswers;

            double score = ((double)correctAnswers / examAttempt.Questions.Count) * 100;

            bool passStatus = score >= 60 ? true : false;

            return new Tuple<double, int, int, bool>(score, correctAnswers, incorrectAnswers, passStatus);
        }

        public async Task<Tuple<double, int, int, bool>> AddAttemptToDb(string userId, ExamAttemptDTO examAttempt)
        {
            if(string.IsNullOrEmpty(userId) || examAttempt == null)
            {
                throw new ArgumentNullException("User ID or Exam Attempt cannot be null.");
            }

            var assessmentResult = AssesExam(examAttempt);
            if (assessmentResult == null)
            {
                throw new InvalidOperationException("Assessment result cannot be null.");
            }

            double score = assessmentResult.Item1;
            int correctAnswers = assessmentResult.Item2;
            int incorrectAnswers = assessmentResult.Item3;
            bool passStatus = assessmentResult.Item4;

            TbUserExams userExamAttempt = new TbUserExams()
            {
                CreatedDate = DateTime.Now,
                PassStatus = passStatus,
                TotalQuestions = examAttempt.Questions.Count,
                CorrectAnswers = correctAnswers,
                Score = score,
                UserId = userId,
                ExamId = examAttempt.ExamId
            };

            try
            {
               await _unitOfWork.BeginTransactionAsync();
               await _unitOfWork.UserExams.AddAsync(userExamAttempt);
               await _unitOfWork.SaveAsync();
               await _unitOfWork.CommitTransactionAsync();
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                await _unitOfWork.RollbackTransactionAsync();
            }

            return new Tuple<double, int, int, bool>(score, correctAnswers, incorrectAnswers, passStatus);
        }
    }
}
