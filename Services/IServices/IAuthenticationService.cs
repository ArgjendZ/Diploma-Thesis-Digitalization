using DiplomaThesisDigitalization.Models.DTOs;
using DiplomaThesisDigitalization.Models.Entities;

namespace DiplomaThesisDigitalization.Services.IServices
{
    public interface IAuthenticationService
    {
        Task AddStudent(CreateStudentDTO studentDTO);
        Task AddMentor(CreateMentorDTO mentorDTO);
        Task AddAdmin(CreateAdminDTO adminDTO);
        Task<string> Login(LoginDTO loginDTO);
        Task<User> LoggedUser(string jwt);
    }
}
