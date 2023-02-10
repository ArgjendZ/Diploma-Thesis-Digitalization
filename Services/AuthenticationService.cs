using DiplomaThesisDigitalization.Data.UnitOfWork;
using DiplomaThesisDigitalization.Helpers;
using DiplomaThesisDigitalization.Models.DTOs;
using DiplomaThesisDigitalization.Models.Entities;
using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaThesisDigitalization.Services
{
    public class AuthenticationService : IServices.IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly JwtHelper _jwtHelper;
        public AuthenticationService(IUnitOfWork unitOfWork, IUserService userService, JwtHelper jwtHelper)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _jwtHelper = jwtHelper;
        }

        public async Task AddAdmin([FromQuery] CreateAdminDTO adminDTO)
        {
            if (await _userService.GetUserFromEmail(adminDTO.Email) != null)
            {
                throw new ArgumentException("Ekziston nje user me kete email");
            }

            User user = new User
            {
                Name = adminDTO.Name,
                Surname = adminDTO.Surname,
                DOB = adminDTO.DOB,
                Email = adminDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(adminDTO.Password),
                Gender = adminDTO.Gender,
                Phone = adminDTO.Phone,
                Address = adminDTO.Address,
                Role = "Admin"
            };

            await _unitOfWork.Repository<User>().CreateAsync(user);
            await _unitOfWork.SaveAsync();

            Administrator admin = new Administrator
            {
                Id = user.Id,
                Type= adminDTO.Type
            };

            await _unitOfWork.Repository<Administrator>().CreateAsync(admin);
            await _unitOfWork.CompleteAsync();
        }

        public async Task AddMentor([FromQuery] CreateMentorDTO mentorDTO)
        {
            throw new NotImplementedException();
        }

        public async Task AddStudent([FromQuery] CreateStudentDTO studentDTO)
        {
            if (await _userService.GetUserFromEmail(studentDTO.Email) != null)
            {
                throw new ArgumentException("Ekziston nje user me kete email");
            }

            User user = new User
            {
                Name = studentDTO.Name,
                Surname = studentDTO.Surname,
                DOB = studentDTO.DOB,
                Email = studentDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(studentDTO.Password),
                Gender = studentDTO.Gender,
                Phone = studentDTO.Phone,
                Address = studentDTO.Address,
                Role = "Student"
            };

            await _unitOfWork.Repository<User>().CreateAsync(user);

            Student student = new Student
            {
                Id = user.Id,
                ECTS = studentDTO.ECTS,
                DegreeLevel = studentDTO.DegreeLevel,
                FieldId = studentDTO.FieldId,
                DepartmentId = studentDTO.DepartmentId
            };

            await _unitOfWork.Repository<Student>().CreateAsync(student);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<string> Login([FromQuery] LoginDTO loginDTO)
        {
            var user = await _userService.GetUserFromEmail(loginDTO.Email);
            if(user == null)
            {
                throw new ArgumentException("Nuk ka user me kete email");
            }
            if(!BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password))
            {
                throw new ArgumentException("Passwordi nuk eshte i sakte");
            }

            return _jwtHelper.Generate(user.Id);
        }

        public async Task<User> LoggedUser(string jwt)
        {
            var token = _jwtHelper.Verify(jwt);
            int userId = int.Parse(token.Issuer);
            return await _userService.GetUserFromId(userId);
        }
    }
}
