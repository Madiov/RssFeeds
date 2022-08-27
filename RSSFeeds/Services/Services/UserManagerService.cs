using Fanap.MarginParkingPlus.Services.Shared;
using Microsoft.IdentityModel.Tokens;
using RSSFeeds.Database.Models;
using RSSFeeds.Database.Shared;
using RSSFeeds.Services.DTO;
using RSSFeeds.Services.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RSSFeeds.Services.Services
{
    public class UserManagerService : BaseService, IUserManagerService
    {
        public UserManagerService(ILoggerManager logger, IUnitOfWork unitOfWork, IConfiguration configuration) : base(unitOfWork, logger, configuration)
        {

        }
        public LoginResponseDTO LoginUser(UserDTO request)
        {
            if(request.Username.IsNullOrEmpty() || request.Password.IsNullOrEmpty())
                return new LoginResponseDTO { Result = false, Message = "Invalid Inputs" };
            var user = unitOfWork.User.Get(u => u.UserName == request.Username);
            if (user == null)
                return new LoginResponseDTO { Result = false, Message = "User Does Not Exist" };
            if (user.Password != GetMd5Hash(request.Password))
                return new LoginResponseDTO { Result = false, Message = "Password Is Wrong" };
            var JWT = CreateToken(user);
            return new LoginResponseDTO { Result = true, Message = "Welcome To Your Dashboard " + user.UserName, Jwtoken = JWT };
        }
        public async Task<BaseResponseDTO> RegisterUser(UserDTO request)
        {
            try
            {
                if (request.Username.IsNullOrEmpty() || request.Password.IsNullOrEmpty())
                    return new LoginResponseDTO { Result = false, Message = "Invalid Inputs" };
                var user = unitOfWork.User.Get(u => u.UserName == request.Username);
                if (user != null)
                    return new BaseResponseDTO { Result = false, Message =user.UserName + " Already Exists" };
                var newAccount = new User();
                newAccount.UserName = request.Username;
                newAccount.Password = GetMd5Hash(request.Password);
                unitOfWork.User.Add(newAccount);
                await unitOfWork.SaveAsync();
                return new BaseResponseDTO { Result=true , Message = "Your Account Created Succesfully " + newAccount.UserName};
            }
            catch(Exception ex)
            {
                return new BaseResponseDTO { Result = false, Message = ex.ToString()};
            }
        }
  
        private string CreateToken(User user)
        {
            
            var secretkey = configuration.GetSection("JwtSetting").GetValue<string>("SecretKey");
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(secretkey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString())
                    }
                    ),
                Expires = DateTime.Now.AddHours(int.Parse(configuration.GetSection("JwtSetting").GetValue<string>("ExpirationTimeHRS"))),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)

            };
            var Token = tokenHandler.CreateToken(tokenDescriptor);
            var JWT = new JwtSecurityTokenHandler().WriteToken(Token);
            return JWT;
        }
        private string GetMd5Hash(string password)
        {
            var md5Hasher = MD5.Create();
            var hashBytes = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(password));

            return Convert.ToHexString(hashBytes);
        }
    }
}
