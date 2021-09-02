using System;

namespace HyperaiShell.App.Models
{
    public class BlockedUser
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public string Reason { get; set; }
        public DateTime Enrollment { get; set; }
        public bool IsBanned { get; set; }
    }
}
