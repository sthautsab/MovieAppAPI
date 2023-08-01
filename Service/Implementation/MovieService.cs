using DAL.Repository.Interface;
using Domain.Models;
using DTO.MovieDTO;
using Microsoft.AspNetCore.Http;
using Service.Interfaces;

namespace Service.Implementation
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public MovieService(IMovieRepository movieRepository, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _movieRepository = movieRepository;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task AddMovie(MovieVM movieVM)
        {
            string uniqueFileName = null;
            if (movieVM.Photo != null)
            {
                uniqueFileName = await UploadFile(movieVM.Photo);
            }
            Movie movie = new Movie();
            movie.Name = movieVM.Name;
            movie.Director = movieVM.Director;
            movie.Description = movieVM.Description;
            movie.PhotoPath = uniqueFileName;
            movie.Genre = movieVM.Genre;

            await _movieRepository.AddMovie(movie);
        }


        public async Task<List<MovieVM>> GetMovies()
        {
            var movies = await _movieRepository.GetMovies();
            List<MovieVM> movieVMs = movies.Select(movie => new MovieVM()
            {
                Id = movie.Id,
                Name = movie.Name,
                Director = movie.Director,
                Description = movie.Description,
                PhotoPath = movie.PhotoPath,
                Genre = movie.Genre,
                AverageRating = movie.AverageRating
            }).ToList();

            return movieVMs;
        }

        public async Task<MovieVM> GetMovieById(Guid id)
        {
            MovieVM movieVM = (await GetMovies()).FirstOrDefault(x => x.Id == id);
            return movieVM;
        }

        public async Task<PagedMovieVM> GetPagedMovies(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 3;

            List<MovieVM> moviesVM = new List<MovieVM>();
            moviesVM = (await _movieRepository.GetMovies()).Select(x => new MovieVM()
            {
                Id = x.Id,
                Name = x.Name,
                Director = x.Director,
                Description = x.Description,
                Genre = x.Genre,
                PhotoPath = x.PhotoPath,
                AverageRating = x.AverageRating
            }).ToList();

            int totalMovies = moviesVM.Count;
            int totalPages = (int)Math.Ceiling(totalMovies / (double)pageSize);

            //starting index of each page
            int startIndex = (pageNumber - 1) * pageSize;

            //skip skips first specified number of data and take takes the specified number of data
            List<MovieVM> pagedMovies = moviesVM.Skip(startIndex).Take(pageSize).ToList();

            PagedMovieVM pagedMovieVM = new PagedMovieVM
            {
                Movies = pagedMovies,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalMovies = totalMovies,
                TotalPages = totalPages
            };
            return pagedMovieVM;
        }



        public async Task<MovieDetailsVM> GetMovieDetails(Guid id)
        {
            MovieDetailsVM movieDetailsVM = new MovieDetailsVM();

            Movie movie = new Movie();
            movie = await _movieRepository.GetMovieById(id);

            MovieVM movieVM = new MovieVM()
            {
                Id = movie.Id,
                Name = movie.Name,
                Description = movie.Description,
                Director = movie.Director,
                Genre = movie.Genre,
                PhotoPath = movie.PhotoPath,
                AverageRating = movie.AverageRating
            };

            movieDetailsVM.MovieVM = movieVM;
            return movieDetailsVM;
        }

        public async Task<MovieUpdateVM> GetMovieDataForEdit(Guid id)
        {
            MovieVM movieVM = await GetMovieById(id);
            MovieUpdateVM movieUpdateVM = new MovieUpdateVM()
            {
                Id = movieVM.Id,
                Name = movieVM.Name,
                Description = movieVM.Description,
                Director = movieVM.Director,
                Genre = movieVM.Genre,
                PhotoPath = movieVM.PhotoPath
            };
            return movieUpdateVM;
        }

        public async Task EditMovie(MovieUpdateVM updatedMovie)
        {
            string uniqueFileName = null;
            //string filePath = updatedMovie.PhotoPath;
            if (updatedMovie.Photo != null)
            {
                uniqueFileName = await UploadFile(updatedMovie.Photo);
            }
            Movie movie = new Movie();
            movie.Id = updatedMovie.Id;
            movie.Name = updatedMovie.Name;
            movie.Director = updatedMovie.Director;
            movie.Description = updatedMovie.Description;
            movie.Genre = updatedMovie.Genre;

            if (uniqueFileName != null)
            {
                movie.PhotoPath = uniqueFileName;
            }
            else
            {
                movie.PhotoPath = updatedMovie.PhotoPath;
            }

            await _movieRepository.UpdateMovie(movie);
        }


        public async Task DeleteMovie(Guid id)
        {
            _movieRepository.DeleteMovie(id);
        }

        public async Task<string> UploadFile(IFormFile file)
        {
            string uniqueFileName = null;
            string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");

            uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

            string filePath = Path.Combine(uploadFolder, uniqueFileName);

            await file.CopyToAsync(new FileStream(filePath, FileMode.Create));
            return uniqueFileName;
        }
    }
}
