using AutoMapper;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.Token;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Domain.Security.Tokens.Refresh;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionBase;
using MyRecipeBook.Infrastructure.Security.Cryptography;

namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserUseCase(IUserWriteOnlyRepository writeOnlyRepository,
                                IUserReadOnlyRepository readOnlyRepository,
                                IMapper mapper,
                                IPasswordEncripter passwordEncrypter,
                                IUnitOfWork unitOfWork,
                                IAccessTokenGenerator accessTokenGenerator,
                                IRefreshTokenGenerator refreshTokenGenerator,
                                ITokenRepository tokenRepository) : IRegisterUserUseCase
{
    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var user = mapper.Map<Domain.Entities.User>(request);

        user.Password = passwordEncrypter.Encrypt(request.Password);

        await writeOnlyRepository.Add(user);

        await unitOfWork.Commit();

        var refreshToken = await CreateAndSaveRefreshToken(user);

        return new ResponseRegisteredUserJson()
        {
            Name = user.Name,
            Tokens = new ResponseTokensJson()
            {
                AccessToken = accessTokenGenerator.Generate(user.UserIdentifier),
                RefreshToken = refreshToken
            }
        };
    }

    private async Task<string> CreateAndSaveRefreshToken(Domain.Entities.User user)
    {
        var refreshToken = refreshTokenGenerator.Generate();

        await tokenRepository.SaveNewRefreshToken(new RefreshToken
        {
            Value = refreshToken,
            UserId = user.Id
        });

        await unitOfWork.Commit();

        return refreshToken;
    }
    private async Task Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        var exist = await readOnlyRepository.ExistActiveUserWithEmail(request.Email);
        if (exist)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_ALREADY_REGISTERED));
        }

        if (!result.IsValid)
        {
            var errorsMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages);
        }
    }
}
