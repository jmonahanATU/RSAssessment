using MovieRatingsAnalyser.Models;

namespace MovieRatingsAnalyser.Services
{
    public class MovieService
    {
        public List<Movie> LoadMoviesFromCsv(string filePath)
        {
            var movies = new List<Movie>();
            
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File not found: {filePath}");
            
            var lines = File.ReadAllLines(filePath);
            if (lines.Length <= 1)
                throw new InvalidOperationException("CSV file is empty or contains only headers");
            
            // Skip header row
            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(',');
                if (parts.Length >= 6)
                {
                    var movie = new Movie
                    {
                        Title = parts[0].Trim(),
                        Genre = parts[1].Trim(),
                        Rating = double.TryParse(parts[2], out double rating) ? rating : 0,
                        Source = parts[3].Trim(),
                        Year = int.TryParse(parts[4], out int year) ? year : 0,
                        ReviewDate = parts[5].Trim()
                    };
                    movies.Add(movie);
                }
            }
            
            return movies;
        }

        public List<Movie> GetTopRatedMovies(List<Movie> movies, int count = 5)
        {
            return movies.OrderByDescending(m => m.Rating).Take(count).ToList();
        }

        public List<Movie> GetWorstRatedMovies(List<Movie> movies, int count = 5)
        {
            return movies.OrderBy(m => m.Rating).Take(count).ToList();
        }

        public List<Movie> FilterByGenre(List<Movie> movies, string genre)
        {
            return movies.Where(m => m.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public Dictionary<int, double> GetAverageRatingByYear(List<Movie> movies)
        {
            return movies.GroupBy(m => m.Year)
                        .ToDictionary(g => g.Key, g => Math.Round(g.Average(m => m.Rating), 2));
        }
    }
}