using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionBase;

namespace MyRecipeBook.Application.UseCases.User.Update;

public class UpdateUserUseCase(ILoggedUser loggedUser,
                                IUserUpdateOnlyRepository repository,
                                IUserReadOnlyRepository userReadonlyRepository,
                                IUnitOfWork unitOfWork) : IUpdateUserUseCase
{
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IUserUpdateOnlyRepository _repository = repository;
    private readonly IUserReadOnlyRepository _userReadonlyRepository = userReadonlyRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute(RequestUpdateUserJson request)
    {
        var loggedUser = await _loggedUser.User();

        await Validate(request, loggedUser.Email);

        var user = await _repository.GetById(loggedUser.Id);

        user.Name = request.Name;
        user.Email = request.Email;

        _repository.Update(user);
        await _unitOfWork.Commit();
    }


    private async Task Validate(RequestUpdateUserJson request, string currentEmail)
    {
        var validator = new UpdateUserValidator();

        var result = validator.Validate(request);

        if (currentEmail.Equals(request.Email).IsFalse())
        {
            var userExist = await _userReadonlyRepository.ExistActiveUserWithEmail(request.Email);
            if (userExist)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceMessagesException.EMAIL_ALREADY_REGISTERED));
            }
        }

        if (result.IsValid.IsFalse())
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }

}