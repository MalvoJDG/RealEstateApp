using RealEstateApp.Core.Application.Dtos.Account;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AutheticationAsync(AuthenticationRequest request);
        Task<AuthenticationResponse> ApiAuthenticationAsync(AuthenticationRequest request);
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin);
        Task<RegisterResponse> RegisterAdminUserAsync(RegisterRequest request, string origin);
        Task<RegisterResponse> RegisterDevUserAsync(RegisterRequest request, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsyncs(ResetPasswordRequest request);
        Task<AuthenticationResponse> GetUserByIdAsync(string userId);
        Task<List<AuthenticationResponse>> GetUsersByIdsAsync(List<string> userIds);
        Task<string> GetUserIdByUsernameAsync(string username);
        Task SingoutAsyncs();
        Task<string> GetUserFullNameById(string userId);
    }
}