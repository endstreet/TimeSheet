using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trm.MaLogger.Data.Views
{
    public class UserProjectView
    {
        public string UserId { get; set; } = null!;
        public string ProjectId { get; set; } = null!;
        public string ClientName { get; set; } = null!;
        public string ProjectName { get; set; } = null!;
        public bool IsProjectManager { get; set; }
        public bool IsProjectMember { get; set; }
    }
}
