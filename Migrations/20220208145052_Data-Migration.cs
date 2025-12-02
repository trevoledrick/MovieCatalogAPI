using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieCatalogAPI.Migrations
{
    public partial class DataMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.CharacterId);
                });

            migrationBuilder.CreateTable(
                name: "Franchises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Franchises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    Director = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trailer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FranchiseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieId);
                    table.ForeignKey(
                        name: "FK_Movies_Franchises_FranchiseId",
                        column: x => x.FranchiseId,
                        principalTable: "Franchises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "MovieAndCharacter",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    CharacterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieAndCharacter", x => new { x.MovieId, x.CharacterId });
                    table.ForeignKey(
                        name: "FK_MovieAndCharacter_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieAndCharacter_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "CharacterId", "Alias", "FullName", "Gender", "Picture" },
                values: new object[,]
                {
                    { 1, "Spider-Man", "Peter Parker", "Male", "https://images.hindustantimes.com/rf/image_size_960x540/HT/p2/2020/05/12/Pictures/_4c4e601e-9425-11ea-9070-932bbf5d90a5.jpg" },
                    { 2, "MJ", "Mary Jane Watson", "Female", "https://www.spidermancrawlspace.com/wp-content/uploads/2019/07/1d74ea8c597b21b1a7d23e03da298a86.jpg" },
                    { 3, null, "Harry Osborn", "Male", "https://static.wikia.nocookie.net/spiderman-films/images/a/ab/F_25672.jpg/revision/latest/scale-to-width-down/250?cb=20130708091735" },
                    { 4, "Mr. Incredible", "Bob Parr", "Male", "https://static.wikia.nocookie.net/disney/images/d/d2/Profile_-_Bob_Parr.jpeg/revision/latest?cb=20190313155821" },
                    { 5, "Elastigirl", "Helen Parr", "Female", "https://ficquotes.com/images/characters/helen-parr-the-incredible.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Franchises",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "A high school kid becomes Spider-Man", "Spider-Man, Tobey Maguire version" },
                    { 2, "A family of superheroes", "The Incredibles" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "MovieId", "Director", "FranchiseId", "Genre", "Picture", "ReleaseYear", "Title", "Trailer" },
                values: new object[,]
                {
                    { 1, "Sam Raimi", 1, "Action", "https://m.media-amazon.com/images/M/MV5BZDEyN2NhMjgtMjdhNi00MmNlLWE5YTgtZGE4MzNjMTRlMGEwXkEyXkFqcGdeQXVyNDUyOTg3Njg@._V1_FMjpg_UX1000_.jpg", 2002, "Spider-Man", "https://www.youtube.com/watch?v=_yhFofFZGcc" },
                    { 2, "Sam Raimi", 1, "Action", "https://m.media-amazon.com/images/M/MV5BMzY2ODk4NmUtOTVmNi00ZTdkLTlmOWYtMmE2OWVhNTU2OTVkXkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_.jpg", 2004, "Spider-Man 2", "https://www.youtube.com/watch?v=1s9Yln0YwCw" },
                    { 3, "Sam Raimi", 1, "Action", "https://m.media-amazon.com/images/M/MV5BYTk3MDljOWQtNGI2My00OTEzLTlhYjQtOTQ4ODM2MzUwY2IwXkEyXkFqcGdeQXVyNTIzOTk5ODM@._V1_.jpg", 2007, "Spider-Man 3", "https://www.youtube.com/watch?v=zjdtiQx7RIw" },
                    { 4, "Brad Bird", 2, "Animation", "https://m.media-amazon.com/images/M/MV5BMTY5OTU0OTc2NV5BMl5BanBnXkFtZTcwMzU4MDcyMQ@@._V1_FMjpg_UX1000_.jpg", 2004, "The Incredibles", "https://www.youtube.com/watch?v=SOKY7XyOHTA" },
                    { 5, "Brad Bird", 2, "Animation", "https://m.media-amazon.com/images/M/MV5BMTEzNzY0OTg0NTdeQTJeQWpwZ15BbWU4MDU3OTg3MjUz._V1_FMjpg_UX1000_.jpg", 2018, "Incredibles 2", "https://www.youtube.com/watch?v=i5qOzqD9Rms" }
                });

            migrationBuilder.InsertData(
                table: "MovieAndCharacter",
                columns: new[] { "CharacterId", "MovieId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 3, 2 },
                    { 1, 3 },
                    { 2, 3 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 4 },
                    { 4, 5 },
                    { 5, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieAndCharacter_CharacterId",
                table: "MovieAndCharacter",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_FranchiseId",
                table: "Movies",
                column: "FranchiseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieAndCharacter");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Franchises");
        }
    }
}