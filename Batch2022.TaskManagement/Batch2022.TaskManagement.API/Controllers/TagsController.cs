using Models = Batch2022.TaskManagement.Domain.Models.Tasks;
using Batch2022.TaskManagement.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Batch2022.TaskManagement.API.Controllers
{
    [Route("api/tags")]
    [ApiController]
    public class TagsController : Controller
    {
        private ITagRepository tagRepository;

        public TagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Models.Tag>> GetTags()
        {
            var tags = tagRepository.FindAll();

            return Ok(tags);
        }
    }
}
