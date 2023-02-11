using DiplomaThesisDigitalization.Data.UnitOfWork;
using DiplomaThesisDigitalization.Models.Entities;
using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace DiplomaThesisDigitalization.Services
{
    public class FieldService : IFieldService
    {
        private readonly IUnitOfWork _unitOfWork;
        public FieldService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateField(string fieldName, int departmentId)
        {
            var repository = _unitOfWork.Repository<Field>();
            
            var existingField = repository.GetAll().Where(a => a.FieldName== fieldName).FirstOrDefault();
            if (existingField != null)
            {
                throw new ArgumentException("Fusha me kete emer ekziston!");
            }
            Field field = new Field() { FieldName = fieldName, DepartmentId = departmentId };
            await repository.CreateAsync(field);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteField(string fieldName)
        {
            var repository = _unitOfWork.Repository<Field>();

            var existingField = repository.GetAll().Where(a => a.FieldName == fieldName).FirstOrDefault();
            if (existingField == null)
            {
                throw new ArgumentException("Fusha me kete emer nuk ekziston!");
            }
            repository.Delete(existingField);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<Field>> GetAllFields()
        {
            var repository = _unitOfWork.Repository<Field>();

            return await repository.GetAll().ToListAsync();
        }

        public Task<List<User>> GetMentorsFromField(string fieldName)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetStudentsFromField(string fieldName)
        { 
            throw new NotImplementedException();
        }
    }
}
 
