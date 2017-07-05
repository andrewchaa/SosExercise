using Moq;
using NUnit.Framework;

namespace SosExercise.Tests
{
    [TestFixture]
    public class CustomerServiceTests
    {
        private bool _isCustomerArchived;
        private int _customerId = 1;
        private Mock<IArchivedDataService> _archivedDataService;
        private Customer _customerFromArchive;
        private CustomerService _customerService;
        private Customer _customerFromDataStore;

        [SetUp]
        public void SetUp()
        {
            _archivedDataService = new Mock<IArchivedDataService>();
            _customerFromArchive = new Customer { Id = 1 };
            _archivedDataService.Setup(a => a.GetCustomer(_customerId)).Returns(_customerFromArchive);
            _customerService = new CustomerService(_archivedDataService.Object);
            _customerFromDataStore = new Customer { Id = 1 };
        }

        [Test]
        public void When_the_customer_is_archived_get_the_customer_from_the_archived_data()
        {
            //arrange
            _isCustomerArchived = true;

            //act
            var customer = _customerService.GetCustomer(_customerId, _isCustomerArchived);

            //assert
            _archivedDataService.Verify(a => a.GetCustomer(_customerId));
            Assert.That(customer, Is.EqualTo(_customerFromArchive));
        }

        [Test]
        public void When_the_customer_is_not_archived_get_the_customer_from_the_data_store()
        {
            //arrange
            _isCustomerArchived = false;
            _archivedDataService.Setup(a => a.GetCustomer(_customerId)).Returns(_customerFromDataStore);
            var service = new CustomerService(_archivedDataService.Object);

            //act
            var customer = service.GetCustomer(_customerId, _isCustomerArchived);

            //assert
            _archivedDataService.Verify(a => a.GetCustomer(_customerId));
            Assert.That(customer, Is.EqualTo(_customerFromDataStore));
        }
    }
}
