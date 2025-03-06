using UnicomTIC_LMS_BE.Entities;

namespace UnicomTIC_LMS_BE.IRepositories
{
    public interface ITokenRepository
    {
        string GenerateToken(User user);
    }
}
