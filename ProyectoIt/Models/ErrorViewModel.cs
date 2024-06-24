namespace ProyectoIt.Models
{
    public class ErrorViewModel
    {
        public string? Nombre { get; set; }

        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
