using MyRecipeBook.Domain.Dtos;
using MyRecipeBook.Domain.Services.OpenAI;

namespace MyRecipeBook.Infrastructure.Services.OpenAI;

public class ChatGptService : IGenerateRecipeAI
{
    private const string CHAT_MODEL = "gpt-4o";
    public async Task<GeneratedRecipeDto> Generate(IList<string> ingredients)
    {
        throw new NotImplementedException();
    }
}
