using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private AccountSqlDAO accountSqlDAO;
        private IUserDAO userSqlDAO;
        private TransferSqlDAO transferSqlDAO;
        protected string UserName
        {
            get
            {
                return User?.Identity?.Name;
            }
        }
        public AccountsController(AccountSqlDAO accountSqlDAO, IUserDAO userSqlDAO, TransferSqlDAO transferSqlDAO)
        {
            this.accountSqlDAO = accountSqlDAO;
            this.userSqlDAO = userSqlDAO;
            this.transferSqlDAO = transferSqlDAO;
        }
        [HttpGet]
        public ActionResult<decimal> GetBalance()
        {
            return accountSqlDAO.GetBalance(UserName);
        }
        [HttpGet("list")]
        public ActionResult<List<User>> GetUsers()
        {
            return Ok(userSqlDAO.GetUsers());
        }
        [HttpGet("transfers")]
        public ActionResult<List<Transfer>> GetTransfers()
        {
            List<Transfer> transfers = transferSqlDAO.GetSentTransfers(UserName);
            if (transfers.Count == 0)
            {
                return NotFound("You have not made any transfers");
            }
            else
            {
                return Ok(transfers);
            }
        }
        [HttpPost("transfers")]
        public ActionResult<Transfer> AddTransfer(Transfer transfer)
        {
            if (transfer.UserTo == User.Identity.Name)
            {
                return NotFound("Cannot transfer money to yourself.");
            }
            if (transfer.Amount < 0)
            {
                return NotFound("Cannot transfer a negative amount.");
            }
            if (transfer.Amount > accountSqlDAO.GetBalance(UserName))
            {
                return NotFound("Not enough money in your account.");
            }
            return Created($"/accounts/transfers/{transfer.Id}", transferSqlDAO.AddTransfer(transfer));
        }
    }
}