namespace Cinema.Data.Services.Emails;

public sealed class EmailDto
{
    public string To { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string Html { get; set; } = null!;
}