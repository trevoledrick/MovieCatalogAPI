namespace MovieCatalogAPI.Models.DTO.Character
{
    public class CharacterEditDTO
    {
        // Primary Key
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Alias { get; set; }
        public string Gender { get; set; }
        public string Picture { get; set; }
    }
}