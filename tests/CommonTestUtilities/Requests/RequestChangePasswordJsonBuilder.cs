using Bogus;
using MyRecipeBook.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class RequestChangePasswordJsonBuilder
{
    public static RequestChangePasswordJson Build(int passwordLenght = 10)
    {
        return new Faker<RequestChangePasswordJson>()
            .RuleFor(user => user.NewPassword, (f) => f.Internet.Password(passwordLenght))
            .RuleFor(user => user.Password, (f) => f.Internet.Password());
    }
}