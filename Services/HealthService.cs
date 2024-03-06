using Pix.Repositories;

namespace Pix.Services;

    public class HealthService
    {

        private HealthRepository _repository
        public HealthService(HealthRepository healthRepository)
        {
            _healthRepository healthRepository
        }
        public string GetHealthMessage()
        {
            return "I'm alive!";
        }
    }