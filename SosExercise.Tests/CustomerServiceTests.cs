using Moq;
using NUnit.Framework;

namespace AsosExercise.Tests
{
    public class CustomerServiceTests
    {
        [Test]
        public void When_the_customer_is_archived_get_the_customer_from_the_archived_data()
        {
            //arrange
            const bool isCustomerArchived = true;
            var customerId = 1;
            var archivedDataService = new Mock<IArchivedDataService>();
            var customerToReturn = new Customer { Id = 1 };
            archivedDataService.Setup(a => a.GetCustomer(customerId)).Returns(customerToReturn);
            var service = new CustomerService(archivedDataService.Object);

            //act
            var customer = service.GetCustomer(customerId, isCustomerArchived);

            //assert
            archivedDataService.Verify(a => a.GetCustomer(customerId));
            Assert.That(customer, Is.EqualTo(customerToReturn));
        }
    }
}
