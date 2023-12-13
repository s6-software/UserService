﻿using API.Models.User;
using FirebaseAdmin.Auth;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace API.Services
{
    public interface IJwtProvider
    {
        Task<string> Login(LoginDTO userDTO);
    }

    public class JwtProvider : IJwtProvider
    {
        private readonly HttpClient _httpClient;

        public JwtProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> Login(LoginDTO userDTO)
        {
            var request = new
            {
                userDTO.Email,
                userDTO.Password,
                returnSecureToken = true
            };
            var response = await _httpClient.PostAsJsonAsync("", request);
            
            var authToken = await response.Content.ReadFromJsonAsync<AuthToken>();

            return authToken.IdToken;
        }

        public class AuthToken
        {
            [JsonPropertyName("kind")]
            public string Kind { get; set; }
            [JsonPropertyName("localId")]
            public string LocalId { get; set; }
            [JsonPropertyName("email")]
            public string Email { get; set; }

            [JsonPropertyName("displayName")]
            public string DisplayName{ get; set; }

            [JsonPropertyName("idToken")]
            public string IdToken { get; set; }

            [JsonPropertyName("registered")]
            public bool Registered { get; set; }
            [JsonPropertyName("refreshToken")]
            public string RefreshToken { get; set; }

            [JsonPropertyName("expiresin")]
            public long ExpiresIn { get; set; }


        }

    }
}
