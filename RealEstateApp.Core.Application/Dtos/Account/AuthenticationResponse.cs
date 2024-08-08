﻿using System.Text.Json.Serialization;

namespace RealEstateApp.Core.Application.Dtos.Account
{
    public class AuthenticationResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string Phone { get; set; }
        public bool IsVerified { get; set; }
        public bool HasError { get; set; }
        public string Error { get; set; }
        public string JwToken { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }
    }
}
