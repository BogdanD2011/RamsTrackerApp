namespace RamsTrackerAPI.Models.Domain
{
    public class MS
    {
        public Guid Id { get; set; }
        public string MS_Title { get; set; }
        public int revision { get; set; }
        public Guid  SubcontractorId { get; set; }
        public Guid RAid { get; set; }


        // Navigation properties

        public Subcontractor Subcontractor { get; set; }
        public RA RA { get; set; }
    }
}
