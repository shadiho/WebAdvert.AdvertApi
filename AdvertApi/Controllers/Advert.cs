using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertApi.Services;
using AdvertApt.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdvertApi.Controllers
{
    [ApiController]
    [Route("adverts/v1")]
    public class Advert : ControllerBase
    {
        private readonly IAdvertStorageService _advertStorageService;

        public Advert(IAdvertStorageService advertStorageService)
        {
            _advertStorageService = advertStorageService;
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(404)]
        [ProducesResponseType(201, Type=typeof(CreateAdvertResponse))]
        public async Task<IActionResult> Create(AdverModel model)
        {
            string recordID;
            try
            {
                recordID = await _advertStorageService.Add(model);
            }
            catch(KeyNotFoundException ex)
            {
                return new NotFoundResult();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return StatusCode(201, new CreateAdvertResponse{ Id = recordID });
        }

        [HttpPut]
        [Route("confirm")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Confirm(ConfirmAdvertModel model)
        {
            try
            {
                await _advertStorageService.Confirm(model);
            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return new OkResult();
        }
    }
}
