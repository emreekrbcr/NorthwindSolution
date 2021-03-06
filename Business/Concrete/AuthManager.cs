using System.Threading.Tasks;
using Business.Abstract;
using Business.Utilities.Constants;
using Core.Entities.Concrete;
using Core.Entities.Concrete.Dtos;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public async Task<IDataResult<User>> Register(UserForRegisterDto userForRegisterDto)
        {
            IResult result = BusinessRules.Run(await CheckIfUserAlreadyExists(userForRegisterDto.Email));

            if (!result.Success)
            {
                return new ErrorDataResult<User>(result.Messages);
            }

            //Sistemde aynı mail adresine sahip kullanıcı yoksa devam ediyoruz
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
            User user = new User()
            {
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Email = userForRegisterDto.Email,
                Status = true
            };
            await _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.AuthMessages.SuccessMessages.UserRegistered);
        }

        public async Task<IDataResult<User>> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = await _userService.GetByEmail(userForLoginDto.Email);

            if (userToCheck.Data == null)
            {
                return new ErrorDataResult<User>(Messages.AuthMessages.ErrorMessages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Data.PasswordHash,
                userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.AuthMessages.ErrorMessages.InvalidPassword);
            }

            return new SuccessDataResult<User>(userToCheck.Data, Messages.AuthMessages.SuccessMessages.SuccessfulLogin);
        }

        public async Task<IDataResult<AccessToken>> CreateAccessToken(User user)
        {
            var claims = await _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims.Data);
            return new SuccessDataResult<AccessToken>(accessToken,
                Messages.AuthMessages.SuccessMessages.AccessTokenCreated);
        }

        #region BusinessRules

        private async Task<IResult> CheckIfUserAlreadyExists(string email)
        {
            var result = await _userService.GetByEmail(email);

            if (result.Data != null)
            {
                return new ErrorResult(Messages.AuthMessages.ErrorMessages.UserAlreadyExists);
            }

            return new SuccessResult();
        }

        #endregion
    }
}