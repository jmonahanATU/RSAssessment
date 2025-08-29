using MovieRatingsAnalyser.Models;
using MovieRatingsAnalyser.Services;

namespace MovieRatingsAnalyser
{
    class Program
    {
        private static MovieService _movieService = new MovieService();
        private static List<Movie> _movies = new List<Movie>();

        static void Main(string[] args)
        {
            Console.WriteLine("Movie Ratings Analyser");
            Console.WriteLine("========================");

            LoadMovieData();
            ShowMainMenu();
        }

        private static void LoadMovieData()
        {
            try
            {
                string filePath = Path.Combine("Data", "movies.csv");
                _movies = _movieService.LoadMoviesFromCsv(filePath);
                Console.WriteLine($"Loaded {_movies.Count} movies from dataset\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading movie data: {ex.Message}");
                Console.WriteLine("Please ensure 'Data/movies.csv' exists and is properly formatted.\n");
            }
        }

        private static void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine("Choose an analysis option:");
                Console.WriteLine("1. Top 5 Highest Rated Movies");
                Console.WriteLine("2. Worst 5 Movies");
                Console.WriteLine("3. Best Years for Movies");
                Console.WriteLine("4. Filter by Genre");
                Console.WriteLine("5. Exit");
                Console.Write("\nEnter your choice (1-5): ");

                var choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ShowTopRatedMovies();
                        break;
                    case "2":
                        ShowWorstMovies();
                        break;
                    case "3":
                        ShowBestYears();
                        break;
                    case "4":
                        FilterByGenre();
                        break;
                    case "5":
                        Console.WriteLine(" Thanks for using Movie Ratings Analyser!");
                        return;
                    default:
                        Console.WriteLine(" Invalid choice. Please try again.\n");
                        break;
                }
            }
        }

        private static void ShowTopRatedMovies()
        {
            if (_movies.Count == 0) return;

            var topMovies = _movieService.GetTopRatedMovies(_movies);
            Console.WriteLine("Top 5 Highest Rated Movies:");
            Console.WriteLine("================================");
            
            for (int i = 0; i < topMovies.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {topMovies[i]}");
            }
            Console.WriteLine();
        }

        private static void ShowWorstMovies()
        {
            if (_movies.Count == 0) return;

            var worstMovies = _movieService.GetWorstRatedMovies(_movies);
            Console.WriteLine("Worst 5 Movies:");
            Console.WriteLine("==================");
            
            for (int i = 0; i < worstMovies.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {worstMovies[i]}");
            }
            Console.WriteLine();
        }

        private static void ShowBestYears()
        {
            if (_movies.Count == 0) return;

            var yearRatings = _movieService.GetAverageRatingByYear(_movies);
            var sortedYears = yearRatings.OrderByDescending(x => x.Value).Take(5);
            
            Console.WriteLine("Top 5 Years for Movies (by average rating):");
            Console.WriteLine("===============================================");
            
            foreach (var year in sortedYears)
            {
                Console.WriteLine($"{year.Key}: {year.Value}/10.0");
            }
            Console.WriteLine();
        }

        private static void FilterByGenre()
        {
            if (_movies.Count == 0) return;

            var genres = _movies.Select(m => m.Genre).Distinct().OrderBy(g => g).ToList();
            
            Console.WriteLine("Available Genres:");
            for (int i = 0; i < genres.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {genres[i]}");
            }

            Console.Write("\nEnter genre name: ");
            var selectedGenre = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(selectedGenre))
            {
                Console.WriteLine(" No genre entered.\n");
                return;
            }

            var filteredMovies = _movieService.FilterByGenre(_movies, selectedGenre);
            
            if (filteredMovies.Count == 0)
            {
                Console.WriteLine($"No movies found for genre: {selectedGenre}\n");
                return;
            }

            Console.WriteLine($"\nMovies in '{selectedGenre}' genre:");
            Console.WriteLine("=====================================");
            
            foreach (var movie in filteredMovies.OrderByDescending(m => m.Rating))
            {
                Console.WriteLine($"• {movie}");
            }
            Console.WriteLine();
        }
    }
}