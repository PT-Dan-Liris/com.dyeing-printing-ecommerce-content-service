﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using com.bateeqshop.service.content.business;
using com.bateeqshop.service.content.data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace com.bateeqshop.service.content.api.Controllers
{
    [ApiController]
    [Route("webcontent")]
    public class WebContentController : ControllerBase
    {
        private readonly ILogger<WebContentController> _logger;
        private readonly IService<WebContent> _service;

        public WebContentController(IService<WebContent> service, ILogger<WebContentController> logger)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> FindAsync()
        {
            try
            {
                var result = await _service.FindAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] WebContent webContent)
        {
 

                await _service.Create(webContent);
                return CreatedAtRoute(
                "Get",
                new { Id = webContent.Id },
                webContent);
                //var result = new ResultFormatter(API_VERSION, General.CREATED_STATUS_CODE, General.OK_MESSAGE)
                //  .Ok();
                //return Created(string.Concat(Request.Path, "/", 0), result);
            
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult> Get(int id)
        {
                var webContent = await _service.GetSingleById(id);
                return Ok(webContent);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] WebContent webContent)
        {
            try
            {
                /*if (webContent == null)
                {
                    return BadRequest("record is null.");
                }*/
                WebContent webContent1 = await _service.GetSingleById(id);

            /*    if (webContent1 == null)
                {
                    return NotFound("The  record couldn't be found.");
                }*/
                await _service.Update(webContent1, webContent);

                return NoContent();
            }
            catch(Exception e) {
                return StatusCode(500);
            }
         
            /*WebContent webContentToUpdate = await _service.GetSingleById(id);
            await _service.Update(webContentToUpdate, webContent);
            return NoContent();*/



        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
               
                await _service.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                
                return StatusCode(500);
            }
        }
    }
}