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
        private readonly IRoboFlowService _roboflowService;

        public ApplianceService(IApplianceRepo applianceRepo,
            IMoveRequestService moveRequestService, IFileService fileService,
            IMapper mapper, IRoboFlowService roboFlowService) 
        {
            _applianceRepo = applianceRepo;
            _moveRequestService = moveRequestService;
            _fileService = fileService;
            _mapper = mapper;
            _roboflowService = roboFlowService;
        }

        public async Task<ApplianceReadDTO?> CreateAppliance(IFormFile img, int moveId, string uri)
        {
            var move = await _moveRequestService.GetMoveRequest(moveId);
            if (move == null) return null;
            var result = await _fileService.SaveImage(img, $"{moveId}");
            if (result.Item1 == 0) return null;
            var _description = await _roboflowService.GetInferenceResultAsync(uri + '/' + result.Item2);
            var appliance = new Appliance
            {
                moveRequestId = moveId,
                ImgURL = uri + '/' + result.Item2,
                description = _description,
            };
            var final = await _applianceRepo.CreateAppliance(appliance);
            var tags = await GetTagsAsync(appliance);
            appliance.Tags = tags;
            var result2 = await AddTagsToAppliance(appliance.Id, tags);
            if (final && result2)
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

        public async Task<Appliance?> GetAppliance(int id)
        {
            var appliance = await _applianceRepo.GetApplianceById(id);
            return appliance;
        }

        public async Task<List<string>> GetTagsAsync(Appliance appliance)
        {
            return await Task.Run(() =>
            {
                var tags = new List<string>();

                switch (appliance.description)
                {
                    case "Air-conditioner":
                        tags.Add("Electronics");
                        tags.Add("Fragile");
                        break;
                    case "Bed":
                        tags.Add("Heavy");
                        break;
                    case "Bench":
                        tags.Add("Heavy");
                        break;
                    case "Cabinet":
                        tags.Add("Heavy");
                        tags.Add("Sharp");
                        break;
                    case "Chair":
                        tags.Add("Heavy");
                        break;
                    case "Computer":
                        tags.Add("Electronics");
                        tags.Add("Fragile");
                        break;
                    case "Desk":
                        tags.Add("Heavy");
                        break;
                    case "Microwave":
                        tags.Add("Electronics");
                        tags.Add("Fragile");
                        break;
                    case "Office-Chair":
                        tags.Add("Heavy");
                        break;
                    case "Refrigerator":
                        tags.Add("Heavy");
                        tags.Add("Electronics");
                        break;
                    case "Sofa":
                        tags.Add("Heavy");
                        break;
                    case "Sofa-Chair":
                        tags.Add("Heavy");
                        break;
                    case "TV":
                        tags.Add("Electronics");
                        tags.Add("Fragile");
                        break;
                    case "TV-Cabinet":
                        tags.Add("Heavy");
                        break;
                    case "Table":
                        tags.Add("Heavy");
                        break;
                    case "Wardrobe":
                        tags.Add("Heavy");
                        tags.Add("Sharp");
                        break;
                    case "Water-Dispenser":
                        tags.Add("Electronics");
                        break;
                    default:
                        break;
                }

                return tags;
            });
        }
    }
}
