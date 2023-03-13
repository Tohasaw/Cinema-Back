using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Cinema.Data.Services.Emails;

public sealed class EmailService
{
    private readonly IOptions<EmailOptions> _options;

    public EmailService(IOptions<EmailOptions> options)
    {
        _options = options;
    }

    public async Task SendAsync(
        string to,
        string subject,
        string html,
        CancellationToken cancellationToken)
    {
        // create message
        var email = CreateMessage(to, subject, html);

        // send email
        await SendEmail(email, cancellationToken);
    }

    private MimeMessage CreateMessage(
        in string to,
        in string subject,
        in string html)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_options.Value.From));
        email.To.Add(MailboxAddress.Parse(to));
        email.Date = DateTimeOffset.Now.LocalDateTime;
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html)
        {
            Text = html
        };
        return email;
    }
    public async Task SendWithPdfAsync(
        string to,
        string subject,
        string html,
        byte[] file,
        CancellationToken cancellationToken)
    {
        // create message
        var email = CreateMessageWithPdf(to, subject, html, file);

        // send email
        await SendEmail(email, cancellationToken);
    }
    private MimeMessage CreateMessageWithPdf(
        in string to,
        in string subject,
        in string html,
        in byte[] file)
    {
        var body = new TextPart(TextFormat.Html)
        {
            Text = html
        };
        var attachment = new MimePart("application", "pdf")
        {
            Content = new MimeContent(new MemoryStream(file)),
            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
            ContentTransferEncoding = ContentEncoding.Base64,
            FileName = Path.GetFileName("tickets")
        };
        var multipart = new Multipart("mixed");
        multipart.Add(body);
        multipart.Add(attachment);

        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_options.Value.From));
        email.To.Add(MailboxAddress.Parse(to));
        email.Date = DateTimeOffset.Now.LocalDateTime;
        email.Subject = subject;
        email.Body = multipart;

        return email;
    }

    private async Task SendEmail(
        MimeMessage email,
        CancellationToken cancellationToken)
    {
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(
            _options.Value.Host, 
            _options.Value.Port, 
            true,
            cancellationToken);

        await smtp.AuthenticateAsync(
            _options.Value.From, 
            _options.Value.Password,
            cancellationToken);

        await smtp.SendAsync(email, cancellationToken);
        await smtp.DisconnectAsync(true, cancellationToken);
        smtp.Dispose();
    }
}