using LibraryWebApi.Entities.Interfaces;
using LibraryWebApi.Models.RequestModels;
using LibraryWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LibraryWebApi.Controllers;

[ApiController]
public abstract class BaseController<T> : ControllerBase where T : class, IEntityId
{
    protected readonly IBaseService<T> EntityService;
    protected readonly ILogger<BaseController<T>> Logger;   
    protected BaseController(IBaseService<T> entityService, ILogger<BaseController<T>> logger)
    {
        EntityService = entityService;
        Logger = logger;
    }

    [HttpGet]
    [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 120)]
    public IActionResult Get()
    {
        return Ok(EntityService.GetEntities());
    }

    [HttpGet("{id:int}")]
    [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 60)]
    public IActionResult Get(int id)
    {
        return Ok(EntityService.Get(id));
    }

    [Authorize]
    [HttpPost]
    public IActionResult Create([FromBody] T entity)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }


        entity.Id = EntityService.Create(entity);

        return CreatedAtAction($"Get", new { id = entity.Id }, entity);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public IActionResult Edit([FromRoute] int id, [FromBody] T entity)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        EntityService.Update(id, entity);

        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        EntityService.Delete(id);

        return NoContent();

    }

    [Authorize]
    [HttpDelete]
    public IActionResult Delete([FromBody] DeleteModel model)
    {
        if (ModelState.IsValid)
        {
            return BadRequest();
        }

        foreach (var item in model.Ids)
        {
            EntityService.Delete(item);
        }

        return Ok();
    }

    [Authorize]
    [HttpPost("Find")]
    public IActionResult Find([FromHeader(Name = "TimesOnOffset")] int offset, [FromBody] SearchModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        model.TimesOnOffset = offset;
        return Ok(EntityService.Find(model));
    }

    [HttpGet("Export")]
    public IActionResult Export([FromQuery] string exportType)
    {
        Logger.LogInformation("Request for export, export type " + exportType);
        if (exportType.ToLower() == "excel")
        {
            Logger.LogTrace("Calling service for exporting excel");
            return new FileStreamResult(EntityService.ExportDataToExcel(),
                "application/xlsx")
            {
                FileDownloadName = "exported.xlsx"
            };
        }

        Logger.LogTrace("Calling service for exporting json");

        return new FileStreamResult(EntityService.ExportDataToJson(),
            "application/json")
        {
            FileDownloadName = "exported.json"
        };

    }

    [Authorize]
    [HttpPost("Import")]
    public IActionResult ImportExcel([FromQuery] string importType, IFormFile file)
    {
        Logger.LogInformation("POST request for import, type " + importType);

        if (importType.ToLower() == "excel")
        {
            Logger.LogTrace("Calling service for importing excel");
            EntityService.ImportDataFromExcel(file);
        }
        else
        {
            EntityService.ImportDataFromJson(file);
        }

        return NoContent();
    }
}