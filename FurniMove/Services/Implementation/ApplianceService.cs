using AutoMapper;
using FurniMove.DTOs;
using FurniMove.Models;
using FurniMove.Repositories.Abstract;
using FurniMove.Services.Abstract;

namespace FurniMove.Services.Implementation
{
    public class ApplianceService : IApplianceService
    {
        private readonly IApplianceRepo _applianceRepo;
        private readonly IMoveRequestService _moveRequestService;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public ApplianceService(IApplianceRepo applianceRepo,
            IMoveRequestService moveRequestService, IFileService fileService,
            IMapper mapper) 
        {
            _applianceRepo = applianceRepo;
            _moveRequestService = moveRequestService;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<ApplianceReadDTO?> CreateAppliance(ApplianceWriteDTO dto, string uri)
        {
            var move = await _moveRequestService.GetMoveRequest((int)dto.moveRequestId!);
            if (move == null) return null;
            var result = _fileService.SaveImage(dto.Img, $"{dto.moveRequestId}");
            if (result.Item1 == 0) return null;
            var appliance = new Appliance
            {
                moveRequestId = dto.moveRequestId,
                ImgURL = uri + '/' + result.Item2,
                description = dto.description,
            };
            var final = await _applianceRepo.CreateAppliance(appliance);
            if (final)
            {
                var readDto = _mapper.Map<ApplianceReadDTO>(appliance);
                return readDto;
            }
            return null;
        }

        public async Task<bool> AddTagsToAppliance(int applianceId, List<string> tags)
        {
            var result = await _applianceRepo.AddTagsToAppliance(applianceId, tags);

            return result;
        }

        public async Task<List<ApplianceReadDTO>> GetAllAppliancesByMove(int moveId)
        {
            var appliances = await _applianceRepo.GetAppliancesByMove(moveId);
            var appliancesDTO = _mapper.Map<List<ApplianceReadDTO>>(appliances);

            return appliancesDTO;
        }
    }
}
