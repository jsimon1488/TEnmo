using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Models
{
    public class Transfer
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string PrintTypeRequestSend
        {
            get
            {
                if (Type == 1)
                {
                    return "Request";
                }
                else
                {
                    return "Send";
                }
            }
        }
        public string PrintTypeFromTo
        {
            get
            {
                if (Type == 1)
                {
                    return "From:";
                }
                else
                {
                    return "To:";
                }
            }
        }
        public int Status { get; set; }
        public string PrintStatus
        {
            get
            {
                if (Status == 1)
                {
                    return "Pending";
                }
                else if (Status == 2)
                {
                    return "Approved";
                }
                else
                {
                    return "Rejected";
                }
            }
        }
        public int AccountFrom { get; set; }
        public int AccountTo { get; set; }
        public decimal Amount { get; set; }
        public string UserTo { get; set; }
        public string UserFrom { get; set; }
        public override string ToString()
        {
            return $"{Id,-5}{PrintTypeRequestSend,-15}{PrintStatus,-15}{(Type == 1 ? AccountFrom : AccountTo),-20}{Amount,-10:C}";
        }
    }
}
