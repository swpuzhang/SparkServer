using Account.Application.Services;
using Account.Application.ViewModels;
using Commons.Domain.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Account.Test
{
    public class LoginTest
    {

        [Fact]
        public async System.Threading.Tasks.Task LoginTestAsync()
        {
            HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:8080") };
            
            var response = await client.PostAsync("/api/Account/Login", new StringContent(JsonConvert.SerializeObject(new AccountVM("facebookzhangyang", "zhangyang", 0,
                "www.baidu.com", 0)),Encoding.UTF8, mediaType:"application/json"));
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var accountInfo = JsonConvert.DeserializeObject<BodyResponse<AccountResponseVM>>(await response.Content.ReadAsStringAsync());
            Assert.True(accountInfo.Body != null);
            Assert.Equal("facebookzhangyang", accountInfo.Body.PlatformAccount);
        }
    }
}
