using System;
using System.Collections.Generic;

namespace Group3Project.Models
{
    public partial class Usertable
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int? BpId { get; set; }

        public virtual Builtplant? Bp { get; set; }
    }
}
