using System.Collections.Generic;
using MovieholicAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace MovieholicAPI.Models
{
    public class MoviesContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Franchise> Franchises { get; set; }

        public MoviesContext(DbContextOptions<MoviesContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data
            modelBuilder.Entity<Franchise>()
                .HasData(new Franchise()
                {
                    Id = 1,
                    Name = "Spider-Man, Tobey Maguire version",
                    Description = "A high school kid becomes Spider-Man"
                });
            modelBuilder.Entity<Franchise>()
                .HasData(new Franchise()
                {
                    Id = 2,
                    Name = "The Incredibles",
                    Description = "A family of superheroes"
                });

            modelBuilder.Entity<Movie>()
                .HasData(new Movie()
                {
                    MovieId = 1,
                    Title = "Spider-Man",
                    Picture = "https://m.media-amazon.com/images/M/MV5BZDEyN2NhMjgtMjdhNi00MmNlLWE5YTgtZGE4MzNjMTRlMGEwXkEyXkFqcGdeQXVyNDUyOTg3Njg@._V1_FMjpg_UX1000_.jpg",
                    Trailer = "https://www.youtube.com/watch?v=_yhFofFZGcc",
                    ReleaseYear = 2002,
                    Director = "Sam Raimi",
                    FranchiseId = 1,
                    Genre = "Action"
                });
            modelBuilder.Entity<Movie>()
                .HasData(new Movie()
                {
                    MovieId = 2,
                    Title = "Spider-Man 2",
                    Picture = "https://m.media-amazon.com/images/M/MV5BMzY2ODk4NmUtOTVmNi00ZTdkLTlmOWYtMmE2OWVhNTU2OTVkXkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_.jpg",
                    Trailer = "https://www.youtube.com/watch?v=1s9Yln0YwCw",
                    ReleaseYear = 2004,
                    Director = "Sam Raimi",
                    FranchiseId = 1,
                    Genre = "Action"
                });
            modelBuilder.Entity<Movie>()
                .HasData(new Movie()
                {
                    MovieId = 3,
                    Title = "Spider-Man 3",
                    Picture = "https://m.media-amazon.com/images/M/MV5BYTk3MDljOWQtNGI2My00OTEzLTlhYjQtOTQ4ODM2MzUwY2IwXkEyXkFqcGdeQXVyNTIzOTk5ODM@._V1_.jpg",
                    Trailer = "https://www.youtube.com/watch?v=zjdtiQx7RIw",
                    ReleaseYear = 2007,
                    Director = "Sam Raimi",
                    FranchiseId = 1,
                    Genre = "Action"
                });
            modelBuilder.Entity<Movie>()
                .HasData(new Movie()
                {
                    MovieId = 4,
                    Title = "The Incredibles",
                    Picture = "https://m.media-amazon.com/images/M/MV5BMTY5OTU0OTc2NV5BMl5BanBnXkFtZTcwMzU4MDcyMQ@@._V1_FMjpg_UX1000_.jpg",
                    Trailer = "https://www.youtube.com/watch?v=SOKY7XyOHTA",
                    ReleaseYear = 2004,
                    Director = "Brad Bird",
                    FranchiseId = 2,
                    Genre = "Animation"
                });
            modelBuilder.Entity<Movie>()
                .HasData(new Movie()
                {
                    MovieId = 5,
                    Title = "Incredibles 2",
                    Picture = "https://m.media-amazon.com/images/M/MV5BMTEzNzY0OTg0NTdeQTJeQWpwZ15BbWU4MDU3OTg3MjUz._V1_FMjpg_UX1000_.jpg",
                    Trailer = "https://www.youtube.com/watch?v=i5qOzqD9Rms",
                    ReleaseYear = 2018,
                    Director = "Brad Bird",
                    FranchiseId = 2,
                    Genre = "Animation"
                });

            modelBuilder.Entity<Character>()
                .HasData(new Character()
                {
                    CharacterId = 1,
                    FullName = "Peter Parker",
                    Gender = "Male",
                    Picture = "https://images.hindustantimes.com/rf/image_size_960x540/HT/p2/2020/05/12/Pictures/_4c4e601e-9425-11ea-9070-932bbf5d90a5.jpg",
                    Alias = "Spider-Man"
                });
            modelBuilder.Entity<Character>()
                .HasData(new Character()
                {
                    CharacterId = 2,
                    FullName = "Mary Jane Watson",
                    Gender = "Female",
                    Picture = "https://www.spidermancrawlspace.com/wp-content/uploads/2019/07/1d74ea8c597b21b1a7d23e03da298a86.jpg",
                    Alias = "MJ"
                });
            modelBuilder.Entity<Character>()
                .HasData(new Character()
                {
                    CharacterId = 3,
                    FullName = "Harry Osborn",
                    Gender = "Male",
                    Picture = "https://static.wikia.nocookie.net/spiderman-films/images/a/ab/F_25672.jpg/revision/latest/scale-to-width-down/250?cb=20130708091735"
                });
            modelBuilder.Entity<Character>()
                .HasData(new Character()
                {
                    CharacterId = 4,
                    FullName = "Bob Parr",
                    Gender = "Male",
                    Picture = "https://static.wikia.nocookie.net/disney/images/d/d2/Profile_-_Bob_Parr.jpeg/revision/latest?cb=20190313155821",
                    Alias = "Mr. Incredible"
                });
            modelBuilder.Entity<Character>()
                .HasData(new Character()
                {
                    CharacterId = 5,
                    FullName = "Helen Parr",
                    Gender = "Female",
                    Picture = "https://ficquotes.com/images/characters/helen-parr-the-incredible.jpg",
                    Alias = "Elastigirl"
                });

            // Many-to-Many
            modelBuilder.Entity<Movie>()
                .HasMany(p => p.Characters)
                .WithMany(m => m.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieAndCharacter",
                    r => r.HasOne<Character>().WithMany().HasForeignKey("CharacterId"),
                    l => l.HasOne<Movie>().WithMany().HasForeignKey("MovieId"),
                    je =>
                    {
                        je.HasKey("MovieId", "CharacterId");
                        je.HasData(
                            new { MovieId = 1, CharacterId = 1 },
                            new { MovieId = 1, CharacterId = 2 },
                            new { MovieId = 1, CharacterId = 3 },
                            new { MovieId = 2, CharacterId = 1 },
                            new { MovieId = 2, CharacterId = 2 },
                            new { MovieId = 2, CharacterId = 3 },
                            new { MovieId = 3, CharacterId = 1 },
                            new { MovieId = 3, CharacterId = 2 },
                            new { MovieId = 3, CharacterId = 3 },
                            new { MovieId = 4, CharacterId = 4 },
                            new { MovieId = 4, CharacterId = 5 },
                            new { MovieId = 5, CharacterId = 4 },
                            new { MovieId = 5, CharacterId = 5 }
                        );
                    });

            // On deleting Franchise
            modelBuilder.Entity<Franchise>()
                .HasMany(p => p.Movies)
                .WithOne(m => m.Franchise)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}