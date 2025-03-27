using OnlineLearning.Data;
using OnlineLearning.Models.Domains.Miscellaneous;
using OnlineLearning.Models.Domains.UserModels;
using OnlineLearning.Repositories.Interfaces;

namespace OnlineLearning.Repositories.Implementations
{ 
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(OnlLearnDBContext context) : base(context) { }
    }
}
