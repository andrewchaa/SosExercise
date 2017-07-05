using System;
using Polly;

namespace SosExercise
{
    public class CustomerService
    {
        private readonly ICustomerDataAccess _customerDataAccess;
        private readonly IArchivedDataService _archivedArchivedDataService;

        public CustomerService(ICustomerDataAccess customerDataAccess, 
            IArchivedDataService archivedArchivedDataService)
        {
            _customerDataAccess = customerDataAccess;
            _archivedArchivedDataService = archivedArchivedDataService;
        }

        public Customer GetCustomer(int customerId, bool isCustomerArchived)
        {
            if (isCustomerArchived)
            {
                return _archivedArchivedDataService.GetCustomer(customerId);
            }

            var policy = Policy.Handle<Exception>().Retry(3);

            Customer customer = null;

            try
            {
                policy.Execute(() =>
                {
                    customer = _customerDataAccess.GetCustomer(customerId);
                });
            }
            catch (Exception)
            {
                throw;
            }
            
            return customer;
        }
    }
}
