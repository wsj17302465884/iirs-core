using IIRS.IRepository.Base;
using IIRS.IServices;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Controllers
{
    /// <summary>
    /// 协同办公
    /// </summary>
    [ApiController]
    [CustomRoute(ApiVersions.V2)]
    [Produces("application/json")]
    public class CoordinationControllers : ControllerBase
    {
        private readonly IDBTransManagement _dbTransManagement;
        private readonly ILogger<CoordinationControllers> _logger;
        private readonly ICoordinationServices _coordinationServices;

        public CoordinationControllers(IDBTransManagement dbTransManagement, ILogger<CoordinationControllers> logger, ICoordinationServices coordinationServices)
        {
            _dbTransManagement = dbTransManagement;
            _logger = logger;
            _coordinationServices = coordinationServices;
        }
    }
}
