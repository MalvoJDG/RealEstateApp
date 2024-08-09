using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.ViewModels.Users;

namespace RealEstateApp.Core.Application.Services
{
    public interface IUserService
    {
        Task<string> ConfirmEmailAsyncs(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string token);
        Task<AuthenticationResponse> LoginAsyncs(LoginViewModel vm);
        Task<RegisterResponse> RegisterAsyncs(SaveUserViewModel vm, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsyncs(ResetPasswordViewModel vm);
        Task SingoutAsyncs();
        Task<string> GetUserFullNameById(string userId);
        Task UpdateUser(SaveUserViewModel model);

        Task<AuthenticationResponse> LoginAsyncsweb(LoginViewModel vm);


    }
}