using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Security.Claims;
using BasketAPI.services;
using EasyNetQ;
using StackExchange.Redis;
using Newtonsoft.Json;

namespace BasketAPI.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class BasketController : ControllerBase
    {
        ConnectionMultiplexer RedisConn = ConnectionMultiplexer.Connect("redis:6379,allowAdmin=true");
        IDatabase db;

        public BasketController()
        {
            db = RedisConn.GetDatabase();
            if (RedisConn == null)
            {
                ConfigurationOptions option = new ConfigurationOptions
                {
                    AbortOnConnectFail = false
                };
                RedisConn = ConnectionMultiplexer.Connect(option);
            }
        }
        //[HttpGet]
        //public ActionResult GetBasket()
        //{
        //    CustomerBasket basket = new CustomerBasket();
        //    return Ok(basket);
        //}
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetBasketByIdAsync(string id)
        {
            var data = await db.StringGetAsync(id);
            if (data.IsNullOrEmpty)
            {
                return null;
            }

            return Ok(JsonConvert.SerializeObject(data));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerBasket), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CustomerBasket>> UpdateBasketAsync([FromBody]CustomerBasket value)
        {
            var created = await db.StringSetAsync(value.BuyerId, JsonConvert.SerializeObject(value));
            return Ok(created);
        }
    }
}
