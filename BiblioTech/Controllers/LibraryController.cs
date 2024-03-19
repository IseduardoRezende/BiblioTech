using BiblioTechData.Enums;
using BiblioTechData.Models;
using BiblioTechDomain.Services.IService;
using BiblioTechDomain.ViewModels.Libraries;
using Microsoft.AspNetCore.Mvc;

namespace BiblioTech.Controllers
{
    public class LibraryController : BaseController<CreateLibraryViewModel, UpdateLibraryViewModel, ReadLibraryViewModel, Library>
    {
        private static readonly string[] includes = new[] { "User" };

        public LibraryController(ILibraryService libraryService) : base(libraryService)
        { }

        public override Task<IActionResult> ListAsync(
            [FromQuery(Name = "filter")] string filter = default!,
            [FromQuery(Name = "orderField")] string orderField = "createdAt",
            [FromQuery(Name = "orderType")] OrderType orderType = OrderType.Asc,
            [FromQuery(Name = "offSet")] int offSet = 0,
            [FromQuery(Name = "itemsPerPage")] short itemsPerPage = 15)
        {
            return base.ListAsync(filter, orderField, orderType, offSet, itemsPerPage, includes);
        }

        public override Task<IActionResult> FindByIdAsync([FromRoute(Name = "id")] long id)
        {
            return base.FindByIdAsync(id, includes);
        }        
    }
}
