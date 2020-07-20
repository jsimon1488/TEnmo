using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.Models
{
    public class Transfer
    {
        public int Id { get; set; }
        public int Type { get; set;  }
        public string PrintType
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
        [Required]
        public int AccountFrom { get; set; }
        [Required]
        public int AccountTo { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string UserTo { get; set; }
        public string UserFrom { get; set; }
        public override string ToString()
        {
            return $"{Id,-5}{PrintType,-15}{PrintStatus,-15}{(Type == 1 ? AccountFrom : AccountTo),-20}{Amount:C,-10}";
        }
    }
}
