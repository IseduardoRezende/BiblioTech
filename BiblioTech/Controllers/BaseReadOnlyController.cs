using BiblioTechData.Interfaces;
using BiblioTechDomain.Interfaces;
using BiblioTechDomain.Services.IService;
using BiblioTechDomain.Bases;
using Microsoft.AspNetCore.Mvc;
using BiblioTechData.Enums;

namespace BiblioTech.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseReadOnlyController<ReadModel, Model> : ControllerBase
        where ReadModel : IReadModel
        where Model : class, IBaseModel
    {
        protected IBaseReadOnlyService<ReadModel, Model> _baseReadOnlyService;

        protected BaseReadOnlyController(IBaseReadOnlyService<ReadModel, Model> baseReadOnlyService)
        {
            _baseReadOnlyService = baseReadOnlyService;
        }

        [HttpGet]
        public virtual async Task<IActionResult> ListAsync(
            [FromQuery(Name = "filter")] string filter = default!,
            [FromQuery(Name = "orderField")] string orderField = "createdAt",
            [FromQuery(Name = "orderType")] OrderType orderType = OrderType.Asc,
            [FromQuery(Name = "offSet")] int offSet = default,
            [FromQuery(Name = "itemsPerPage")] short itemsPerPage = 15)
        {
            var isValid = TryBuildBaseFilter(filter, out IEnumerable<BaseFilter> buildFilter);

            if (!isValid)
                return Problem($"Error to build filter");

            var result = await _baseReadOnlyService.ListAsync(buildFilter, orderField, orderType, offSet, itemsPerPage);
            return Ok(result);
        }

        protected async Task<IActionResult> ListAsync(
            string filter,
            string orderField,
            OrderType orderType,
            int offSet,
            short itemsPerPage,
            params string[] includes)
        {
            var isValid = TryBuildBaseFilter(filter, out IEnumerable<BaseFilter> buildFilter);

            if (!isValid)
                return Problem($"Error to build filter");

            var result = await _baseReadOnlyService.ListAsync(buildFilter, orderField, orderType, offSet, itemsPerPage, includes);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> FindByIdAsync([FromRoute(Name = "id")] long id)
        {
            var result = await _baseReadOnlyService.FindByAsync(c => c.Id == id);

            return result == null ? NotFound() : Ok(result);
        }

        protected async Task<IActionResult> FindByIdAsync(long id, params string[] includes)
        {
            var result = await _baseReadOnlyService.FindByAsync(c => c.Id == id, includes);

            return result == null ? NotFound() : Ok(result);
        }

        private static bool TryBuildBaseFilter(string filter, out IEnumerable<BaseFilter> buildFilter)
        {
            try
            {
                buildFilter = BaseFilter.ConvertToBaseFilter(filter);
                return true;
            }
            catch
            {
                buildFilter = null!;
                return false;
            }
        }
    }
}
