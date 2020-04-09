namespace ReservationApi.Models
{
    public interface IReservationDataSettings
    {
        string ReservationCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}