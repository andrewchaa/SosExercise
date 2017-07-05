namespace SosExercise
{
    public class ArchivedDataService : IArchivedDataService
    {
        public Customer GetCustomer(int customerId)
        {
            return new Customer();
        }
    }

    public interface IArchivedDataService
    {
        Customer GetCustomer(int customerId);
    }
}