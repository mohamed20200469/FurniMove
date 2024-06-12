using AutoMapper;
using FurniMove.DTOs;
using FurniMove.Mapper;
using FurniMove.Models;
using FurniMove.Repositories.Abstract;
using FurniMove.Services.Abstract;
using Microsoft.AspNetCore.Identity;

namespace FurniMove.Services.Implementation
{
    public class MoveOfferService : IMoveOfferService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMoveOfferRepo _moveOfferRepo;
        public MoveOfferService(IMoveOfferRepo moveOfferRepo, IMapper mapper, 
            UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _moveOfferRepo = moveOfferRepo;
        }
        public async Task<bool> CreateMoveOffer(MoveOffer moveOffer)
        {
            return await _moveOfferRepo.CreateMoveOffer(moveOffer);
        }

        public async Task<List<MoveOffer>> GetAllMoveOffers()
        {
            return await _moveOfferRepo.GetAllMoveOffers();
        }

        public async Task<List<MoveOfferReadDTO>?> GetAllMoveOffersByRequestId(int id)
        {
            var moveOffers = await _moveOfferRepo.GetAllMoveOffersByRequestID(id);

            var moveOfferDTOs = new List<MoveOfferReadDTO>();

            foreach (var moveOffer in moveOffers)
            {
                var serviceProvider = await _userManager.FindByIdAsync(moveOffer.serviceProviderId);
                var serviceProviderDTO = _mapper.Map<UserDTO>(serviceProvider);

                var moveOfferDTO = moveOffer.ToMoveOfferDTO(serviceProviderDTO);

                moveOfferDTOs.Add(moveOfferDTO);
            }
            return moveOfferDTOs;
        }

        public async Task<MoveOffer?> GetMoveOfferById(int id)
        {
            return await _moveOfferRepo.GetMoveOfferById(id);
        }
    }
}
