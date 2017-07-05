namespace SosExercise
{
    public interface IDataServiceFactory
    {
        IDataService Create(bool isCustomerArchived);
    }
}