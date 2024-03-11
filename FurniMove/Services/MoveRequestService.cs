using FurniMove.Interfaces.IRepositories;
using FurniMove.Interfaces.IServices;
using FurniMove.Models;

namespace FurniMove.Services
{
    public class MoveRequestService : IMoveRequestService
    {
        private readonly IMoveRequestRepo _moveRequestRepo;
        public MoveRequestService(IMoveRequestRepo moveRequestRepo)
        {
            _moveRequestRepo = moveRequestRepo;
        }

        public bool CreateMoveRequest(MoveRequest moveRequest)
        {
            return _moveRequestRepo.CreateMoveRequest(moveRequest);
        }

        public MoveRequest? GetMoveRequestById(int id)
        {
            return _moveRequestRepo.GetMoveRequestById(id);
        }

        public ICollection<MoveRequest> GetMoveRequests()
        {
            return _moveRequestRepo.GetMoveRequests();
        }
    }
}
