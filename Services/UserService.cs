using DiplomaThesisDigitalization.Data.UnitOfWork;
using DiplomaThesisDigitalization.Models.Entities;
using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace DiplomaThesisDigitalization.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var repository = _unitOfWork.Repository<User>();

            return await repository.GetAll().ToListAsync();
        }

        public async Task<User> GetUserFromEmail(string userEmail)
        {
            var repository = _unitOfWork.Repository<User>();

            return await repository.GetByCondition(a => a.Email == userEmail).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserFromId(int userId)
        {
            var repository = _unitOfWork.Repository<User>();

            return await repository.GetByCondition(a => a.Id == userId).FirstOrDefaultAsync();
        }
    }
}
