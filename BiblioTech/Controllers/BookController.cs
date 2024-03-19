using BiblioTechData.Enums;
using BiblioTechData.Models;
using BiblioTechDomain.Services.IService;
using BiblioTechDomain.ViewModels.Books;
using Microsoft.AspNetCore.Mvc;

namespace BiblioTech.Controllers
{
    public class BookController : BaseController<CreateBookViewModel, UpdateBookViewModel, ReadBookViewModel, Book>
    {
        private static readonly string[] includes = new[] { "Genre", "Library" };

        public BookController(IBookService bookService) : base(bookService) { }

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
