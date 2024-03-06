namespace Pix.Repositories;

    public class HealthRepository
    {
        public HealthRepository()
        {
            Console.WriteLine("Oie");
        }
        public string GetHealthMessage()
        {
            return "I'm alive!";
        }
    }