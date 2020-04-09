using MongoDB.Driver;
using ReservationApi.Models;

namespace ReservationApi.Tests.Mongo
{
    public interface IFakeMongoCollection : IMongoCollection<Reservation>
    {
        IFindFluent<Reservation, Reservation> Find(FilterDefinition<Reservation> filter, FindOptions options);

        //IFindFluent<Reservation, Reservation> Project(ProjectionDefinition<BsonDocument, BsonDocument> projection);

        //IFindFluent<BsonDocument, BsonDocument> Skip(int skip);

        //IFindFluent<BsonDocument, BsonDocument> Limit(int limit);

        //IFindFluent<BsonDocument, BsonDocument> Sort(SortDefinition<BsonDocument> sort);
    }
}
