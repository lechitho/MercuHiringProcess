namespace HiringProcessService.API.Models
{
    public class CandidateModel
    {
        public required string Name { get; set; }
        public required string Stage { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public Guid Id { get; set; }
    }
}
