using FurniMove.Data;
using FurniMove.Interfaces.IRepositories;
using FurniMove.Models;

namespace FurniMove.Repositories
{
    public class MoveRequestRepo : IMoveRequestRepo
    {
        private readonly AppDbContext _db;
        public MoveRequestRepo(AppDbContext db)
        {
            _db = db;
        }

        public bool CreateMoveRequest(MoveRequest moveRequest)
        {
            _db.MoveRequests.Add(moveRequest);
            return Save();
        }

        public bool DeleteMoveRequestById(int id)
        {
            var moveRequest = _db.MoveRequests.FirstOrDefault(x => x.Id == id);
            if (moveRequest != null)
            {
                _db.MoveRequests.Remove(moveRequest);
            }
            return Save();
        }

        public MoveRequest? GetMoveRequestById(int id)
        {
            var moveRequest = _db.MoveRequests.FirstOrDefault(x => x.Id == id);
            return moveRequest;
        }

        public ICollection<MoveRequest> GetMoveRequests()
        {
            return _db.MoveRequests.ToList();
        }

        public bool Save()
        {
            var saved = _db.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateMoveRequest(MoveRequest moveRequest)
        {
            _db.MoveRequests.Update(moveRequest);
            return Save();
        }
    }
}
