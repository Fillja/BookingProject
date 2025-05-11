using Infrastructure.Models;

namespace BookingBackOffice.Models;

public class SignInViewModel
{
    public string Title { get; set; } = "Sign in";
    public SignInModel SignInModel { get; set; } = new SignInModel();
    public string? DisplayMessage { get; set; }
    public string? ErrorMessage { get; set; }
}
