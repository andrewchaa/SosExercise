namespace SosExercise
{
    public interface IDataServiceFactory
    {
        IArchivedDataService Create(bool isCustomerArchived);
    }
}