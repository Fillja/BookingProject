using Infrastructure.Models;

namespace BookingBackOffice.Models;

public class SignUpViewModel
{
    public string Title { get; set; } = "Sign up";
    public SignUpModel SignUpModel { get; set; } = new SignUpModel();
    public string? DisplayMessage { get; set; }
    public string? ErrorMessage { get; set; }
}
