using AutoMapper;
using FileTypeChecker.Extensions;
using FileTypeChecker.Types;
using MyRecipeBook.Application.Extensions;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Domain.Services.Storage;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionBase;

namespace MyRecipeBook.Application.UseCases.Recipe.Register;

public class RegisterRecipeUseCase(IRecipeWriteOnlyRepository repository, ILoggedUser loggedUser, IUnitOfWork unitOfWork, IMapper mapper, IBlobStorageService blobStorageService) : IRegisterRecipeUseCase
{
    private readonly ILoggedUser _loggedUser = loggedUser;

    public async Task<ResponseRegisteredRecipeJson> Execute(RequestRegisterRecipeFormData request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.User();

        var recipe = mapper.Map<Domain.Entities.Recipe>(request);
        recipe.UserId = loggedUser.Id;

        var instructions = request.Instructions.OrderBy(i => i.Step).ToList();
        for (var index = 0; index < instructions.Count; index++)
        {
            instructions[index].Step = index + 1;
        }

        recipe.Instructions = mapper.Map<IList<Domain.Entities.Instruction>>(instructions);

        if(request.Image is not null)
        {
            var fileStream  = request.Image.OpenReadStream();

            (var isValidImage, var extension) = fileStream.ValidateAndGetImageExtension();

            if (isValidImage.IsFalse())
            {
                throw new ErrorOnValidationException([ResourceMessagesException.ONLY_IMAGES_ACCEPTED]);
            }

            recipe.ImageIdentifier = $"{Guid.NewGuid()}{Path.GetExtension(request.Image.FileName)}";

            await blobStorageService.Upload(loggedUser, fileStream, recipe.ImageIdentifier);
        }

        await repository.Add(recipe);

        await unitOfWork.Commit();

        return mapper.Map<ResponseRegisteredRecipeJson>(recipe);
    }

    private static void Validate(RequestRecipeJson request)
    {
        var result = new RecipeValidator().Validate(request);

        if (result.IsValid.IsFalse())
            throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToArray());
    }
}
