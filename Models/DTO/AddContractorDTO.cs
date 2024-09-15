namespace RamsTrackerAPI.Models.DTO
{
    public class AddContractorDTO
    {
        //public Guid Id { get; set; }
        public string Name { get; set; }
        public string Activity { get; set; }

        public string HsPersonFirstName { get; set; }
        public string HsPersonLastName { get; set; }
        public string HsPersonEmail { get; set; }
        public string HsPersonPhone { get; set; }   
    
    }
}
