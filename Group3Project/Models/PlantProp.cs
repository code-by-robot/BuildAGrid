using System;
using System.Collections.Generic;

namespace Group3Project.Models
{
    public partial class Plantprop
    {
        public Plantprop()
        {
            Builtplants = new HashSet<Builtplant>();
        }

        public int Id { get; set; }
        public string? FuelType { get; set; }
        public int? MinCapacity { get; set; }
        public int? MaxCapacity { get; set; }
        public ulong? RampRate { get; set; }
        public string? FuelTypeCode { get; set; }
        public string? AltCode { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<Builtplant> Builtplants { get; set; }
    }
}
