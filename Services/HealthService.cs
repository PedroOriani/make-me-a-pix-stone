using Pix.Repositories;

namespace Pix.Services;
{
    public class HealthService
    {
        private readonly HealthRepository _repository
            
        public HealthService(HealthRepository healthRepository)
        {
            _repository healthRepository
        }
        public string GetHealthMessage()
        {
            return _repository.GetHealthMessage();
        }
    }
}
