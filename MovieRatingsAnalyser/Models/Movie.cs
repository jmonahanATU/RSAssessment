namespace MovieRatingsAnalyser.Models
{
    /// <summary>
    /// Represents a movie with rating information from various sources
    /// </summary>
    public class Movie
    {
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public double Rating { get; set; }
        public string Source { get; set; } = string.Empty;
        public int Year { get; set; }
        public string ReviewDate { get; set; } = string.Empty;

        /// <summary>
        /// Returns a formatted string representation of the movie
        /// </summary>
        public override string ToString()
        {
            return $"{Title} ({Year}) - {Genre} - Rating: {Rating} ({Source})";
        }
    }
}