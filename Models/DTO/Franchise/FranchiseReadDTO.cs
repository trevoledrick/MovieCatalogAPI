using System.ComponentModel.DataAnnotations;

namespace MovieholicAPI.Models.DTO.Franchise
{
    public class FranchiseReadDTO
    {
        // Primary Key
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}