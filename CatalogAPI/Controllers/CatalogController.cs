using CatalogAPI.Context;
using CatalogAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CatalogAPI.Controllers
{

    [Route("{controller}")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogContext dbContext;
        public CatalogController(CatalogContext dbContext2)
        {
            dbContext = dbContext2;
        }

        [HttpGet]
        public ActionResult<List<CatalogItem>> GetItemsAsync()
        {
            var result = dbContext.CatalogItems.ToList();
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<CatalogItem>> UpdateBasketAsync([FromBody]CatalogItem value)
        {
            dbContext.CatalogItems.Add(value);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
       

      
