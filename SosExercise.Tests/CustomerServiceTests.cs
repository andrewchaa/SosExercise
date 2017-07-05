using System;
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
        private IDataServiceFactory _dataServiceFactory;
        private Mock<ICustomerDataAccess> _customerDataAccess;

        [SetUp]
        public void SetUp()
        {
            _customerFromArchive = new Customer { Id = 1 };
            _archivedDataService = new Mock<IArchivedDataService>();
            _archivedDataService.Setup(a => a.GetCustomer(_customerFromArchive.Id)).Returns(_customerFromArchive);

            _customerFromDataStore = new Customer { Id = 2 };
            _customerDataAccess = new Mock<ICustomerDataAccess>();
            _customerDataAccess.Setup(a => a.GetCustomer(_customerFromDataStore.Id)).Returns(_customerFromDataStore);

            _customerService = new CustomerService(_customerDataAccess.Object, _archivedDataService.Object);
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
        public void Should_retrieve_the_customer_from_data_store_if_not_archived()
        {
            //arrange
            _isCustomerArchived = false;

            //act
            var customer = _customerService.GetCustomer(2, _isCustomerArchived);

            //assert
            _customerDataAccess.Verify(a => a.GetCustomer(2));
            Assert.That(customer, Is.EqualTo(_customerFromDataStore));
        }

        [Test]
        public void Should_try_to_retrieve_the_customer_three_times()
        {
            //arrange
            var count = 0;
            _customerDataAccess.Setup(c => c.GetCustomer(2)).Callback(() =>
            {
                count++;
                if (count == 0)
                    throw new Exception();
            }).Returns(_customerFromDataStore);
            _isCustomerArchived = false;

            //act
            var customer = _customerService.GetCustomer(2, _isCustomerArchived);

            //assert
            _customerDataAccess.Verify(a => a.GetCustomer(2));
            Assert.That(customer, Is.EqualTo(_customerFromDataStore));
            Assert.That(count, Is.EqualTo(1));
        }

    }
}
