using DiplomaThesisDigitalization.Data.UnitOfWork;
using DiplomaThesisDigitalization.Models.DTOs;
using DiplomaThesisDigitalization.Models.Entities;
using DiplomaThesisDigitalization.Services.IServices;

namespace DiplomaThesisDigitalization.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateDepartment(CreateDepartmentDTO depDTO)
        {
            var repository = _unitOfWork.Repository<Department>();

            var existingDepartment = repository.GetAll().Where(a => a.Name == depDTO.Name).FirstOrDefault();
            if (existingDepartment != null)
            {
                throw new ArgumentException("Departamenti me kete emer ekziston!");
            }
            Department dep = new Department() 
            { 
                Name = depDTO.Name, 
                FacultyId = depDTO.FacultyId,
                Location = depDTO.Location,
                Number = depDTO.Number
            };
            await repository.CreateAsync(dep);
            await _unitOfWork.CompleteAsync();
        }

        public Task DeleteDepartment(int departmentId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Department>> GetAllDepartments()
        {
            throw new NotImplementedException();
        }

        public Task<List<Department>> GetDepartmentsByFaculty(int facultyId)
        {
            throw new NotImplementedException();
        }
    }
}
