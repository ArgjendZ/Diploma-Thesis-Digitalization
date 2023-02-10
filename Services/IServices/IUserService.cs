using DiplomaThesisDigitalization.Models.Entities;

namespace DiplomaThesisDigitalization.Services.IServices
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserFromId(int userId);
        Task<User> GetUserFromEmail(string userEmail);
    }
}
