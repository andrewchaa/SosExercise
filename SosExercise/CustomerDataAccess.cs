namespace SosExercise
{
    public class CustomerDataAccess : IDataService
    {
        public Customer GetCustomer(int customerId)
        {
            return new Customer();
        }
    }
}