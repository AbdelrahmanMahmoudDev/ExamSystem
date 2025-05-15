using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamSystem.DAL.Repository;
using ExamSystem.Domains;

namespace ExamSystem.BL
{
    public class ExamService : IExamService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExamService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> SaveNew(TbExams newExam)
        {
            if (newExam is null)
            {
                throw new ArgumentException(nameof(newExam));
            }
            try
            {
                newExam.CreatedDate = DateTime.Now;

                await _unitOfWork.Exams.AddAsync(newExam);
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SaveUpdate(TbExams exam)
        {
            if (exam == null)
            {
                throw new ArgumentNullException(nameof(exam));
            }

            try
            {
                _unitOfWork.Exams.Update(exam);
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<TbExams>> GetAllExams()
        {
            return await _unitOfWork.Exams.GetAllAsync();
        }

        public async Task<TbExams> GetExamBasedOnId(int id)
        {
            TbExams targetExam = await _unitOfWork.Exams.GetByIdAsync(id);

            if(targetExam == null)
            {
                return new TbExams();
            }

            return targetExam;
        }

        public async Task<bool> DeleteExam(TbExams exam)
        {
            if (exam == null)
            {
                throw new ArgumentNullException(nameof(exam));
            }

            try
            {
                _unitOfWork.Exams.Delete(exam);
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
