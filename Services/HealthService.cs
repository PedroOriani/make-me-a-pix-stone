namespace Pix.Services
{
    public class HealthService
    {
        public HealthService()
        {
            Console.WriteLine("Oie");
        }
        public string GetHealthMessage()
        {
            return "I'm alive!";
        }
    }
}