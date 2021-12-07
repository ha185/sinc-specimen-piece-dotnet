namespace Server.Web.Controllers
{
    using DataContracts;
    using global::AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Server.Core.Interfaces;
    using Swashbuckle.AspNetCore.Annotations;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class UpdateController : ControllerBase
    {
        private readonly ILogger<UpdateController> _logger;

        private readonly IMapper _mapper;

        private readonly IUpdateStorage _updateStorage;

        public UpdateController(ILogger<UpdateController> logger, IMapper mapper, IUpdateStorage updateStorage)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._updateStorage = updateStorage;
        }

        [HttpGet]
        [SwaggerResponse(200, "New version available", typeof(AppVm))]
        [SwaggerResponse(204, "The application is up-to-date")]
        [SwaggerResponse(400, "Invalid version format")]
        public async Task<IActionResult> Get([FromQuery] string version)
        {
            if (!Version.TryParse(version, out var v))
            {
                this.ModelState.AddModelError("version", "Invalid version format");

                return this.BadRequest(this.ModelState);
            }

            if (!await this._updateStorage.IsNewVersionAvailableAsync(v))
            {
                return this.NoContent();
            }

            var latestApp = await this._updateStorage.GetLatestVersionAsync();

            return this.Ok(this._mapper.Map<AppVm>(latestApp));
        }

        [HttpGet("list")]
        [SwaggerResponse(200, "List of application versions", typeof(List<AppBaseVm>))]
        public async Task<IActionResult> GetList()
        {
            var list = await this._updateStorage.GetVersionListAsync();

            return this.Ok(this._mapper.Map<List<AppBaseVm>>(list));
        }

        [HttpGet("{version}/details")]
        [SwaggerResponse(200, "Get details of the version", typeof(AppDetailsVm))]
        public async Task<IActionResult> GetDetails([FromRoute] string version)
        {
            await Task.CompletedTask;

            return this.Ok(new AppDetailsVm
            {
                Version = version,
                Description = $"Lorem ipsum (v{version}) dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus."
            });
        }
    }
}