using Microsoft.EntityFrameworkCore.Query;
using System.ComponentModel.DataAnnotations;

namespace RamsTrackerAPI.Models.Domain
{
    public class ContractorFlow
    {
        
        public Guid ContractorId { get; set; }
        public virtual Contractor Contractor { get; set; }
        [Key]
        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public DateTime StartDate { get; set; }
        public int DurationOnSite { get; set; }
    }
}
