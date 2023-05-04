using System.Collections.Generic;

namespace VDMJasminka.WebClient.Models
{
    public class SettingsViewModel
    {
        public IEnumerable<string> AppliedMigrations { get; set; }
        public IEnumerable<string> PendingMigrations { get; set; }

    }
}
