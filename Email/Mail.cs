namespace BlogApi.Email
{
    public class Mail
    {
        public string? To { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
    }

    public record MailDto(string? To, string? Subject, string? Body);
}
