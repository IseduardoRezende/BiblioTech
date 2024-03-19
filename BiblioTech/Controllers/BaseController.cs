using BiblioTechData.Interfaces;
using BiblioTechDomain.Interfaces;
using BiblioTechDomain.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace BiblioTech.Controllers
{
    public abstract class BaseController<CreateModel, UpdateModel, ReadModel, Model> : BaseReadOnlyController<ReadModel, Model>
        where CreateModel : ICreateModel
        where UpdateModel : IUpdateModel
        where ReadModel : IReadModel
        where Model : class, IBaseModel
    {
        protected IBaseService<CreateModel, UpdateModel, ReadModel, Model> _baseService;

        protected BaseController(IBaseService<CreateModel, UpdateModel, ReadModel, Model> baseService)
            : base(baseService)
        {
            _baseService = baseService;
        }
        
        [HttpPost]
        public virtual async Task<IActionResult> CreateAsync([FromBody] CreateModel createModel)
        {
            var createdModel = await _baseService.CreateAsync(createModel);

            if (createdModel.Validation != null)
                return UnprocessableEntity(createdModel.Validation);

            return Created($"/[controller]/{createdModel.Id}", createdModel);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> UpdateAsync([FromRoute(Name = "id")] long id,
                                                             [FromBody] UpdateModel updateModel)
        {
            if (id != updateModel.Id)
                return BadRequest("Route Id is different from Body Id");

            var updatedModel = await _baseService.UpdateAsync(updateModel);           
            
            if (updatedModel.Validation != null)
                return UnprocessableEntity(updatedModel.Validation);

            return Ok(updateModel);
        }

        [HttpDelete("Delete/{id}")]
        public virtual async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
        {
            var result = await _baseService.DeleteAsync(id);

            return result ? Ok() : ValidationProblem("Entity is already deleted");
        }

        [HttpDelete("DeletePermanent/{id}")]
        public virtual async Task<IActionResult> DeletePermanentAsync([FromRoute(Name = "id")] long id)
        {
            var result = await _baseService.DeletePermanentAsync(id);

            return result ? Ok() : NotFound();
        }

        [HttpPut("Active/{id}")]
        public virtual async Task<IActionResult> ActiveAsync([FromRoute(Name = "id")] long id)
        {
            var result = await _baseService.ActiveAsync(id);

            return result ? Ok() : ValidationProblem("Entity is already activated");
        }
    }
}
