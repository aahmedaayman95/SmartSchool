using SmartSchool.BL.Models;
using SmartSchool.BL.ViewModel;
using System.Threading.Tasks;
//using JWT.Models;

namespace SmartSchool.BL.Services
{
    public interface IAuthService
    {
        //add endpoints such as register or login , .....
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> LoginAsync(LoginModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
		Task<AuthModel> ConfirmEmailAsync(string userId, string token);
        Task<AuthModel> ForgotPasswordAsync(string email);
        Task<AuthModel> ResetPasswordAsync(ResetPasswordVM resetPasswordVM);
		Task<AuthModel> ChangePasswordAsync(ChangePasswordVM changePasswordVM);

	}
}
