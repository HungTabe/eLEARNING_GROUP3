using OnlineLearning.Data;
using OnlineLearning.Models.Domains.Miscellaneous;
using OnlineLearning.Repositories.Interfaces;

namespace OnlineLearning.Repositories.Implementations
{
    public class FAQsRepository : BaseRepository<FAQ>, IFAQsRepository
    {
        public FAQsRepository(OnlLearnDBContext context) : base(context) { }
    }
}
