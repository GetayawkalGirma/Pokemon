using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pokemon.Data;
using Pokemon.Models;
using PokemonReviewer.Interfaces;

namespace PokemonReviewer.Repository
{
    public class ReviewRepository:IReviewRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ReviewRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        Review IReviewRepository.GetReview(int reviewId)
        {
            return _context.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
        }

        ICollection<Review> IReviewRepository.GetReviews()
        {
            return _context.Reviews.ToList();
        }

        ICollection<Review> IReviewRepository.GetReviewsOfAPokemon(int pokeId)
        {
            return _context.Reviews.Where(r => r.Pokemon.Id == pokeId).ToList();
        }

        bool IReviewRepository.ReviewExists(int reviewId)
        {
            return _context.Reviews.Any(r => r.Id == reviewId);
        }
    }
}
