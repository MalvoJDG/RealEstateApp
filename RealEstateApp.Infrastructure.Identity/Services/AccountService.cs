using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Domain.Settings;
using RealEstateApp.Infraestructure.Identity.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using RealEstateApp.Core.Application.ViewModels.Users;
using System.Security.Policy;
using MiniProyectoBanking.Core.Application.Dtos.Account;

namespace RealEstateApp.Infraestructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly JwtSettings _jwtSettings;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IEmailService emailservice, IOptions<JwtSettings> jwtsettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailservice;
            _jwtSettings = jwtsettings.Value;
        }

        public async Task<string> GetUserFullNameById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            return $"{user.FirstName} {user.LastName}";
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleclaim = new List<Claim>();

            foreach (var role in roles)
            {
                roleclaim.Add(new Claim("roles", role));
            }

            var claim = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleclaim);

            var SymetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var SinginCredential = new SigningCredentials(SymetricKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken
                (
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claim,
                    expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                    signingCredentials: SinginCredential
                );
           
               
            return jwtSecurityToken;
        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,

            };
        }

        private string RandomTokenString()
        {
            using var rngCryptoProvider = new RNGCryptoServiceProvider();
            var randombyte = new byte[40];
            rngCryptoProvider.GetBytes(randombyte);

            return BitConverter.ToString(randombyte).Replace("-", "");
        }

        public async Task<AuthenticationResponse> ApiAuthenticationAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No accounts registered with {request.UserName}";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid credentials for {request.UserName}";
                return response;
            }

            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"The email is not confirmed for {request.UserName}";
                return response;
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("ADMIN") && !roles.Contains("DESARROLLADOR"))
            {
                response.HasError = true;
                response.Error = "You do not have permission to access this web API.";
                return response;
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;
            response.ProfilePictureUrl = user.ProfilePictureUrl;
            response.JwToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var refreshToken = GenerateRefreshToken();
            response.RefreshToken = refreshToken.Token;

            response.Roles = roles.ToList();
            response.IsVerified = user.EmailConfirmed;

            return response;
        }

        public async Task<AuthenticationResponse> AutheticationAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts register with {request.UserName}";
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid Credentials for {request.UserName}";
            }

            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"The email is not confirmed for {request.UserName}";
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);
            

            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;
            response.ProfilePictureUrl = user.ProfilePictureUrl;
            response.JwToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var refreshtoken = GenerateRefreshToken();
            response.RefreshToken = refreshtoken.Token;

            var roleList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = roleList.ToList();
            response.IsVerified = user.EmailConfirmed;

            return response;
        }

        public async Task SingoutAsyncs()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin)
        {
            RegisterResponse response = new()
            {
                HasError = false
            };

            var UserSameName = await _userManager.FindByNameAsync(request.UserName);
            if (UserSameName != null)
            {
                response.HasError = true;
                response.Error = $"The UserName '{request.UserName}' already exists";
                return response;
            }

            var UserSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (UserSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"The email '{request.Email}' is already register";
                return response;
            }

            var user = new ApplicationUser()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.Phone,
                ProfilePictureUrl = request.ProfilePictureUrl
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded && request.Rol == Roles.CLIENTE)
            {
                await _userManager.AddToRoleAsync(user, Roles.CLIENTE.ToString());
                var verificationurl = await SendVerificationEmailurlAsync(user, origin);
                await _emailService.SendAsync(new Core.Application.Dtos.Email.EmailRequest()
                {
                    To = user.Email,
                    Body = $"Confirme Your Account visiting this URL {verificationurl}",
                    Subject = "Confirm Registration"
                });
            }
            else if(result.Succeeded && request.Rol == Roles.AGENTE)
            {
                await _userManager.AddToRoleAsync(user, Roles.AGENTE.ToString());
            }
            else
            {
                response.HasError = true;
                response.Error = $"A error has ocurred in the register";
                return response;
            }

            return response;
        }


        public async Task<AuthenticationResponse> AutheticationAsyncWeb(AuthenticationRequest request)
        {
            AuthenticationResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts register with {request.UserName}";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid Credentials for {request.UserName}";
                return response;
            }

            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"The email is not confirmed for {request.UserName}";
                return response;
            }

            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;
            response.ProfilePictureUrl = user.ProfilePictureUrl;

            var roleList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = roleList.ToList();
            response.IsVerified = user.EmailConfirmed;

            return response;
        }


        public async Task<RegisterResponse> RegisterAdminUserAsync(RegisterRequest request, string origin)
        {
            RegisterResponse response = new()
            {
                HasError = false
            };

            var UserSameName = await _userManager.FindByNameAsync(request.UserName);
            if (UserSameName != null)
            {
                response.HasError = true;
                response.Error = $"The UserName '{request.UserName}' already exists";
                return response;
            }

            var UserSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (UserSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"The email '{request.Email}' is already register";
                return response;
            }

            var user = new ApplicationUser()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.Phone,
                ProfilePictureUrl = request.ProfilePictureUrl,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.ADMIN.ToString());
            }
            else
            {
                response.HasError = true;
                response.Error = $"An error occurred trying to register the user.";
                return response;
            }

            return response;
        }

        public async Task<RegisterResponse> RegisterDevUserAsync(RegisterRequest request, string origin)
        {
            RegisterResponse response = new()
            {
                HasError = false
            };

            var UserSameName = await _userManager.FindByNameAsync(request.UserName);
            if (UserSameName != null)
            {
                response.HasError = true;
                response.Error = $"The UserName '{request.UserName}' already exists";
                return response;
            }

            var UserSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (UserSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"The email '{request.Email}' is already register";
                return response;
            }

            var user = new ApplicationUser()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.Phone,
                ProfilePictureUrl = request.ProfilePictureUrl,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.DESARROLLADOR.ToString());
            }
            else
            {
                response.HasError = true;
                response.Error = $"An error occurred trying to register the user.";
                return response;
            }

            return response;
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
        {
            ForgotPasswordResponse response = new()
            {
                HasError = false
            };


            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"Account {request.Email} not found";
                return response;
            }

            var verificationurl = await SendForgotPasswordUriAsync(user, origin);
            await _emailService.SendAsync(new Core.Application.Dtos.Email.EmailRequest()
            {
                To = user.Email,
                Body = $"Reset your password visiting this URL {verificationurl}",
                Subject = "Reset your password"
            });


            return response;
        }

        private async Task<string> SendForgotPasswordUriAsync(ApplicationUser user, string origin)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            var route = "User/ResetPassword";

            var uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUrl = QueryHelpers.AddQueryString(uri.ToString(), "token", code);


            return verificationUrl;

        }

        private async Task<string> SendVerificationEmailurlAsync(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            var route = "User/ConfirmEmail";

            var uri = new Uri(string.Concat($"{origin}/", route));

            var verificationUrl = QueryHelpers.AddQueryString(uri.ToString(), "userId", user.Id);
            verificationUrl = QueryHelpers.AddQueryString(verificationUrl, "token", code);


            return verificationUrl;

        }

        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return $"{user.Id} is not register";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Account confirmed for{user.Email}, now you can use de app";
            }
            else
            {
                return $"An error ocurred confirmin the email:{user.Email}";
            }
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsyncs(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"Account {request.Email} not found";
                return response;
            }
            request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error ocurred while reset password for:{user.Email}";
            }

            return response;
        }

        public async Task<AuthenticationResponse> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return null;

            return new AuthenticationResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfilePictureUrl = user.ProfilePictureUrl,
                UserName = user.UserName,
                Phone = user.PhoneNumber,
                Email = user.Email
            };
        }

        public async Task<List<AuthenticationResponse>> GetUsersByIdsAsync(List<string> userIds)
        {
            var users = await _userManager.Users.Where(u => userIds.Contains(u.Id)).ToListAsync();
            return users.Select(user => new AuthenticationResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                ProfilePictureUrl = user.ProfilePictureUrl
            }).ToList();
        }

        public async Task<string> GetUserIdByUsernameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user.Id;
        }

        public async Task<List<AuthenticationResponse>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var userList = new List<AuthenticationResponse>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userList.Add(new AuthenticationResponse
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Roles = roles.ToList(),
                    Phone = user.PhoneNumber,
                    ProfilePictureUrl = user.ProfilePictureUrl
                });
            }

            return userList;
        }

        public async Task UpdateUser (SaveUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            // Update it with the values from the view model

            user!.UserName = model.Username;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.ProfilePictureUrl = model.ProfilePictureUrl;
            user.PhoneNumber = model.Phone;


            // Apply the changes if any to the db
            await _userManager.UpdateAsync(user);
        }

        public async Task<UserDto> GetByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            var userDto = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Nombre = user.FirstName,
                Apellido = user.LastName,
                EmailConfirmed = user.EmailConfirmed,
                Tipo = await GetUserTypeAsync(user.Id)
            };

            return userDto;
        }

        public async Task<string> GetUserTypeAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            if (await _userManager.IsInRoleAsync(user, Roles.ADMIN.ToString()))
            {
                return "Admin";
            }
            else if (await _userManager.IsInRoleAsync(user, Roles.AGENTE.ToString()))
            {
                return "Agente";
            }
            else
            {
                // Definir un comportamiento por defecto o manejar otros roles según sea necesario
                return "Otro";
            }
        }

        public async Task<ConfirmEmailResponse> ConfirmUserEmailAsync(EditUsuarioViewModel vm)
        {
            ConfirmEmailResponse response = new ConfirmEmailResponse
            {
                HasError = false
            };

            var user = await _userManager.FindByIdAsync(vm.Id);
            if (user == null)
            {
                response.HasError = true;
                response.Error = "User not found.";
                return response;
            }

            user.EmailConfirmed = !user.EmailConfirmed;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = "Error confirming email.";
                return response;
            }

            return response;
        }

    }
}
