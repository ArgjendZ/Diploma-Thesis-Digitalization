using DiplomaThesisDigitalization.Models.Entities;

namespace DiplomaThesisDigitalization.Services.IServices
{
    public interface IFieldService
    {
        Task<Field> GetField(int fieldId);
        Task DeleteField(int fieldId);
        Task<List<Mentor>> GetMentorsFromField(string fieldName);

    }
}
