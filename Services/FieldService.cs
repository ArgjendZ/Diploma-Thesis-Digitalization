using DiplomaThesisDigitalization.Data.UnitOfWork;
using DiplomaThesisDigitalization.Models.Entities;
using DiplomaThesisDigitalization.Services.IServices;

namespace DiplomaThesisDigitalization.Services
{
    public class FieldService : IFieldService
    {
        private readonly IUnitOfWork _unitOfWork;
        public FieldService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateField(string fieldName)
        {
            var repository = _unitOfWork.Repository<Field>();
            
            var existingField = repository.GetAll().Where(a => a.FieldName== fieldName).FirstOrDefault();
            if (existingField != null)
            {
                throw new ArgumentException("Fusha me kete emer ekziston!");
            }
            Field field = new Field() { FieldName = fieldName };
            await repository.CreateAsync(field);
            await _unitOfWork.CompleteAsync();
        }

        public Task DeleteField(string fieldName)
        {
            throw new NotImplementedException();
        }

        public Task<List<Field>> GetAllFields()
        {
            throw new NotImplementedException();
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
