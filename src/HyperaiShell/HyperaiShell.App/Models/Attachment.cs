namespace HyperaiShell.App.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        public string Target { get; set; }
        public object Object { get; set; }
        public string TypeName { get; set; }
    }
}
