namespace SosExercise
{
    public class ArchivedDataService : IDataService
    {
        public Customer GetCustomer(int customerId)
        {
            return new Customer();
        }
    }
}