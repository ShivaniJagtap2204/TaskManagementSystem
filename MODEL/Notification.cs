using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public int? TaskId { get; set; }
        public string NotificationType { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
