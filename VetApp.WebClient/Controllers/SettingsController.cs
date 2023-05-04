using Microsoft.AspNetCore.Mvc;
using VDMJasminka.Application.Database;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.WebClient.Models;

namespace VDMJasminka.WebClient.Controllers
{
    public class SettingsController : Controller
    {
        private readonly IOwnersManagementService _ownersService;
        private readonly IMigrationsService _migrationsService;

        public SettingsController(IOwnersManagementService ownersService, IMigrationsService migrationsService)
        {
            _ownersService = ownersService;
            _migrationsService = migrationsService;
        }

        public IActionResult Migrations()
        {
            var unappliedMigrations = _migrationsService.GetPendingMigrations();
            var applied = _migrationsService.GetAppliedMigrations();
            var vm = new SettingsViewModel
            {
                AppliedMigrations = applied,
                PendingMigrations = unappliedMigrations
            };
            return View(vm);
        }
    }
}
