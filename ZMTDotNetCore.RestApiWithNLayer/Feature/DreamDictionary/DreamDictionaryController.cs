using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ZMTDotNetCore.RestApiWithNLayer.Feature.DreamDictionary
{
    [Route("api/[controller]")]
    [ApiController]
    public class DreamDictionaryController : ControllerBase
    {
       public DreamDictionaryController() { }
        private  async Task<DreamDictionaryModel> Data()
        {
            string data =await System.IO.File.ReadAllTextAsync("DreamDictionary.json");
            var json=JsonConvert.DeserializeObject<DreamDictionaryModel>(data);
            return json;
        }

        [HttpGet("GetAllDictionary")]
        public async Task<IActionResult> GetAllDictionary()
        {
            var model = await Data();
            return Ok(model.BlogDetail);
        }

        [HttpGet("GetAlphabet")]
        public async Task<IActionResult> GetAlphabet()
        {
            var model = await Data();
            return Ok(model.BlogHeader);
        }

        [HttpGet("{BlogId}")]
        public async Task<IActionResult> GetDictionaryByAlphabet(int BlogId)
        {
            var model = await Data();
            return Ok(model.BlogDetail.Where(x => x.BlogId == BlogId));
        }
    }
}
