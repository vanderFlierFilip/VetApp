using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VDMJasminka.WebApi.Controllers.Inventory
{
    /// <summary>
    /// Base controller for all inventory-related controllers.
    /// </summary>
    /// <remarks>
    [ApiController, Authorize(Roles = "Admin")]
    [ApiExplorerSettings(GroupName = "Warehouse")]
    public class BaseInventoryController : ControllerBase
    {
    }
}