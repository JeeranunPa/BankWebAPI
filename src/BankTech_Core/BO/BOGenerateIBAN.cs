using AngleSharp;
using BankTech_Model;
using BankTech_Model.Models.System;
using PuppeteerSharp;
using System;
using System.Threading.Tasks;

namespace BankTech_Core.BO
{
    public class BOGenerateIBAN
    {
        public async Task<string> GenerateIBAN()
        {
            try
            {
                using var browserFetcher = new BrowserFetcher();
                await browserFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
                var browser = await Puppeteer.LaunchAsync(new LaunchOptions
                {
                    Headless = true
                });
                BOConfigMaster boconfig = new BOConfigMaster();
                ResultDataModels resultGetConfig = boconfig.GetConfigMaster("001");
                if (!resultGetConfig.success)
                {
                    throw new Exception(resultGetConfig.msg);
                }
                ConfigMasterModels config = (ConfigMasterModels)resultGetConfig.data;

                var page = await browser.NewPageAsync();
                await page.GoToAsync(config.configUrl);

                var content = await page.GetContentAsync();

                var context = BrowsingContext.New(Configuration.Default);
                var document = await context.OpenAsync(req => req.Content(content));

                string selectedValue = string.Empty;
                BOAccount boAcc = new BOAccount();
                do
                {
                    var selectElement = document.QuerySelector(config.configValue1);
                    selectedValue = selectElement?.TextContent;
                    ResultDataModels resultGetAcc = boAcc.GetAccount(selectedValue);
                    AccountModels account = (AccountModels)resultGetAcc.data;
                    if (!string.IsNullOrWhiteSpace(account?.accountNo))
                    {
                        selectedValue = string.Empty;
                    }

                }
                while (string.IsNullOrWhiteSpace(selectedValue));

                return selectedValue;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());

            }
        }
    }
}
