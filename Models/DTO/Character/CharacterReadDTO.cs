namespace MovieholicAPI.Models.DTO.Character
{
    public class CharacterReadDTO
    {
        // Primary Key
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Alias { get; set; }
        public string Gender { get; set; }
        public string Picture { get; set; }
    }
}