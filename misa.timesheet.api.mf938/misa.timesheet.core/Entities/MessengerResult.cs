using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace misa.timesheet.core.Entities
{
    public class MessengerResult
    {
        public MessengerResult(string userMsg, string devMsg)
        {
            this.userMsg = userMsg;
            this.devMsg = devMsg;
        }

        // thông báo cho người dùng
        public string userMsg { get; set; } = "";

        //thông báo cho dev
        public string devMsg { get; set; } = "";
    }
}
