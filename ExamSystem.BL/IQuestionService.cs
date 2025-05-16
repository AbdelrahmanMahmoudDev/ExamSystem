using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamSystem.Domains;

namespace ExamSystem.BL
{
    public interface IQuestionService
    {
        public Task<TbQuestions> GetQuestionBasedOnId(int id);
        public Task<bool> RemoveQuestion(int id);
    }
}
