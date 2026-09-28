using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspire.Data.SeedData.DMS
{
    public static class SeedConfiguration
    {
        public static readonly Dictionary<string, string> EntityToSheet =
            new()
            {
                ["DMSProjectOwner"] = "DMSProjectOwner",
                ["Contractor"] = "Contractor",
                ["DMSProject"] = "DMSProject",
                ["DMSContract"] = "DMSContract"
            };
    }
}
