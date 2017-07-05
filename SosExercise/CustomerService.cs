namespace SosExercise
{
    public class CustomerService
    {
        private readonly IArchivedDataService _archivedDataService;

        public CustomerService(IArchivedDataService archivedDataService)
        {
            _archivedDataService = archivedDataService;
        }

        public Customer GetCustomer(int customerId, bool isCustomerArchived)
        {
            if (isCustomerArchived)
            {
                var customer = _archivedDataService.GetCustomer(customerId);
                return customer;
            }

            return new Customer();
        }
    }
}
