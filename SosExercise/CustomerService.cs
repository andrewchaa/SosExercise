namespace SosExercise
{
    public class CustomerService
    {
        private readonly IDataServiceFactory _dataServiceFactory;

        public CustomerService(IDataServiceFactory dataServiceFactory)
        {
            _dataServiceFactory = dataServiceFactory;
        }

        public Customer GetCustomer(int customerId, bool isCustomerArchived)
        {
            var dataService = _dataServiceFactory.Create(isCustomerArchived);
            var customer = dataService.GetCustomer(customerId);

            return customer;
        }
    }
}
