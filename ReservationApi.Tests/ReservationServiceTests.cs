using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using MongoDB.Driver;
using Moq;
using ReservationApi.Models;
using ReservationApi.Services;
using ReservationApi.Tests.Mongo;
using System;
using System.Threading;
using Xunit;

namespace ReservationApi.Tests
{
    public class ReservationServiceTests
    {
        [Fact]
        public void ReservationService_Add_Reservation()
        {
            //arrange
            var mongoCollMock = new Mock<IFakeMongoCollection>();

            mongoCollMock.Setup(x => x.Find(x => x.Id == It.IsAny<string>(), new FindOptions())).Verifiable();

            //var reservationSvc = new ReservationService(mongoCollMock.Object);

            //act
            //var result = reservationSvc.GetAsync("1");

            //assert
            mongoCollMock.Verify();

        }
    }
}
