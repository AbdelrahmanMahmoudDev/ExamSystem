using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamSystem.DAL.Repository;
using ExamSystem.Domains;

namespace ExamSystem.BL
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuestionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TbQuestions> GetQuestionBasedOnId(int id)
        {
            return await _unitOfWork.Questions.GetByIdAsync(id);
        }

        public async Task<bool> RemoveQuestion(int id)
        {
            TbQuestions question = await GetQuestionBasedOnId(id);
            if (question is null)
            {
                return false;
            }

            try
            {
                _unitOfWork.Questions.Delete(question);
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
