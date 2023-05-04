using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VDMJasminka.WebApi.Controllers.Ambulance
{
    /// <summary>
    /// Base controller for all ambulance-related controllers.
    /// </summary>
    [ApiController, Authorize(Roles = "Admin, User")]
    [ApiExplorerSettings(GroupName = "Ambulance")]
    public class BaseAmbulanceController : ControllerBase
    {
    }
}