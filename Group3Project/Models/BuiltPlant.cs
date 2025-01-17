﻿using System;
using System.Collections.Generic;

namespace Group3Project.Models
{
    public partial class Builtplant
    {
        public Builtplant()
        {
            Usertables = new HashSet<Usertable>();
        }

        public int? FuelId { get; set; }
        public int? NameplateCapacity { get; set; }
        public bool? PowState { get; set; }
        public int? Npc { get; set; }
        public int? Ac { get; set; }
        public int Id { get; set; }

        public virtual Plantprop? Fuel { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<Usertable> Usertables { get; set; }
    }
}
