using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using BankTech_Model.Models.System;

namespace BankTech.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankController : ControllerBase
    {
        private readonly IServiceProvider _sp;
        private readonly ILogger<BankController> _logger;
        private readonly IConfiguration _configuration;


        public BankController(ILogger<BankController> logger, IServiceProvider sp, IConfiguration configuration)
        {
            _sp = sp;
            _logger = logger;
            _configuration = configuration;

        }
        [HttpPost("CreateAccount")]
        public async Task<ResultDataModels> CreateAccount([FromBody] BankTech_Model.AccountModels account)
        {

            ResultDataModels result = new ResultDataModels();
            try
            {
                BankTech_Core.BO.BOAccountInfo boAcc = new BankTech_Core.BO.BOAccountInfo();
                result = await boAcc.AddAccountInfo(account);
            }
            catch (Exception ex)
            {
                result.error = ex.Message.ToString();
                result.msg = ex.Message.ToString();
            }
            return result;
        }
        [HttpPost("CreateAccountTransection")]
        public ResultDataModels CreateAccountTransection([FromBody] BankTech_Model.Model.ViewModels.Filter.AccountTransectionFilter filter)
        {

            ResultDataModels result = new ResultDataModels();
            try
            {
                BankTech_Core.BO.BOAccountTransactionsInfo boAcctran = new BankTech_Core.BO.BOAccountTransactionsInfo();
                result = boAcctran.AddAccountTransectionInfo(filter);
            }
            catch (Exception ex)
            {
                result.error = ex.Message.ToString();
                result.msg = ex.Message.ToString();
            }
            return result;
        }
    }
}
