using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MobyLabWebProgramming.Infrastructure.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Responses;
using System.Net;
using System.Net.Http.Json;
using MobyLabWebProgramming.Core.Entities;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class LoginService : ILoginService
{
    private readonly JwtConfiguration _jwtConfiguration;

    /// <summary>
    /// Inject the required service configuration from the application.json or environment variables.
    /// </summary>
    public LoginService(IOptions<JwtConfiguration> jwtConfiguration) => _jwtConfiguration = jwtConfiguration.Value;

    public async Task<ServiceResponse<LoginResponseDTO>> Login(LoginDTO login, CancellationToken cancellationToken = default)
    {
        //  result from here is the user from the database.

        User? result = new User(); // Assuming User is the class representing the user model.

        using (HttpClient client = new HttpClient())
        {
            try
            {
                var link = "http://localhost:5000/api/User/GetByEmail/mail/" + login.Email;
                var response = await client.GetAsync(link, cancellationToken); // Make a request to the user service to get the user by email.
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON into an object
                    var jsonObject = JsonConvert.DeserializeObject<dynamic>(responseBody);

                    // Access the response field
                    var responseData = jsonObject.response;

                    // map data from responseData to result variable
                    if (responseData != null)
                    {
                        result.Id = responseData.id;
                        result.Email = responseData.email;
                        result.Password = responseData.password;
                        result.Name = responseData.name;
                        result.PhoneNumber = responseData.phoneNumber;
                        if (responseData.role == "Admin")
                            result.Role = UserRoleEnum.Admin;
                        else
                            result.Role = UserRoleEnum.User;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                return ServiceResponse<LoginResponseDTO>.FromError(new(HttpStatusCode.NotFound, ex.Message, ErrorCodes.CannotSee)); // Pack the proper error as the response.
            }
        }

        if (result == null) // Verify if the user is found in the database.
        {
            return ServiceResponse<LoginResponseDTO>.FromError(CommonErrors.UserNotFound); // Pack the proper error as the response.
        }

        if (result.Password != login.Password) // Verify if the password hash of the request is the same as the one in the database.
        {
            return ServiceResponse<LoginResponseDTO>.FromError(new(HttpStatusCode.BadRequest, "Wrong password!", ErrorCodes.WrongPassword));
        }

        var user = new UserDTO
        {
            Id = result.Id,
            Email = result.Email,
            Name = result.Name,
            PhoneNumber = result.PhoneNumber,
            Role = result.Role
        };

        return ServiceResponse<LoginResponseDTO>.ForSuccess(new()
        {
            User = user,
            Token = this.GetToken(user, DateTime.UtcNow, new(7, 0, 0, 0)) // Get a JWT for the user issued now and that expires in 7 days.
        });
    }


    public string GetToken(UserDTO user, DateTime issuedAt, TimeSpan expiresIn)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtConfiguration.Key); // Use the configured key as the encryption key to sing the JWT.
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new(new[] { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) }), // Set the user ID as the "nameid" claim in the JWT.
            Claims = new Dictionary<string, object> // Add any other claims in the JWT, you can even add custom claims if you want.
            {
                { ClaimTypes.Name, user.Name },
                { ClaimTypes.Email, user.Email },
                { ClaimTypes.Role, user.Role },
                { ClaimTypes.MobilePhone, user.PhoneNumber },
            },
            IssuedAt = issuedAt, // This sets the "iat" claim to indicate then the JWT was emitted.
            Expires = issuedAt.Add(expiresIn), // This sets the "exp" claim to indicate when the JWT expires and cannot be used.
            Issuer = _jwtConfiguration.Issuer, // This sets the "iss" claim to indicate the authority that issued the JWT.
            Audience = _jwtConfiguration.Audience, // This sets the "aud" claim to indicate to which client the JWT is intended to.
            SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) // Sign the JWT, it will set the algorithm in the JWT header to "HS256" for HMAC with SHA256.
        };

        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)); // Create the token.
    }
}
