namespace Domain
{
    public class ProcessingProgressModel
    {
        public int Step { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Solution { get; set; } = string.Empty;
    }
}
