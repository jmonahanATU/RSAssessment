# Movie Ratings Analyser

A C# console application that analyzes movie rating datasets to help users discover and evaluate movies based on various criteria.

## Features

- **Top Rated Movies**: Display the highest-rated films
- **Worst Movies**: Identify poorly-rated films to avoid
- **Best Years Analysis**: Find which years produced the best movies on average
- **Genre Filtering**: Filter movies by specific genres (Horror, Sci-Fi, Drama, etc.)
- **Error Handling**: Graceful handling of missing files and invalid data

## Architecture

MovieRatingsAnalyser/
- **Models/**
  - `Movie.cs` — Data model for movie information
- **Services/**
  - `MovieService.cs` — Business logic for data processing and analysis
- `Program.cs` — Console interface and user interaction
- **Data/**
  - `movies.csv` — Sample dataset

## Requirements

- .NET 6.0 or later
- CSV dataset with columns: Title, Genre, Rating, Source, Year, ReviewDate

## Usage

1. Ensure your movie data is in `Data/movies.csv`
2. Run the application:
   ```bash
   dotnet run

