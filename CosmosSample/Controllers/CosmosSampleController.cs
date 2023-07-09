using CosmosSample.Interfaces;
using CosmosSample.Models;
using Microsoft.AspNetCore.Mvc;

namespace CosmosSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CosmosSampleController : ControllerBase
    {      

        private readonly ILogger<CosmosSampleController> _logger;
        private readonly ICosmosSampleService _cosmosSampleService;

        public CosmosSampleController(ILogger<CosmosSampleController> logger, ICosmosSampleService cosmosSampleService)
        {
            _logger = logger;
            _cosmosSampleService = cosmosSampleService;
        }

        [HttpGet]
        public async Task<IEnumerable<CosmosSampleModel>> Get()
        {
            return await _cosmosSampleService.GetAsync();
        }

        [HttpGet("{partitionkey}/{id}")]
        public async Task<CosmosSampleModel> Get(string id, string partitionkey)
        {
            return await _cosmosSampleService.GetByIdAsync(id, partitionkey);
        }

        [HttpPost]
        public async Task Post(CosmosSampleModel model)
        {
            await _cosmosSampleService.AddAsync(model);
        }
    }
}