using Moq;
using NUnit.Framework;
using Proxx.Entity;
using Proxx.Manager.RandomManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProxxTest.GameRepositiryTests
{
    public class GetBlackHolesTests : GameRepositoryBase
    {
        [Test]
        public void CheckDublicationTest()
        {
            var gameRepo = GetGameRepository();
            var rendomManMock = new Mock<IRandomMan>();

            rendomManMock.SetupSequence(x => x.Next(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(2)
                .Returns(2)
                //
                .Returns(2)
                .Returns(2)
                //
                .Returns(1)
                .Returns(2)
                //
                .Returns(2)
                .Returns(2)
                //
                .Returns(0)
                .Returns(1);

            gameRepo.RandomMan = rendomManMock.Object;
            var expectedBlackHolesCount = 3;
            var expectedResult = new HashSet<IdObject> { new IdObject(2, 2), new IdObject(1, 2), new IdObject(0, 1) };
            var maxRows = 3;
            var maxCols = 3;
            var actualResult = gameRepo.GetBlackHoles(maxRows, maxCols, expectedBlackHolesCount);

            Assert.AreEqual(actualResult.Count, expectedBlackHolesCount);
            Assert.AreEqual(actualResult.Where(x => expectedResult.Contains(x)).Count(), expectedBlackHolesCount);
        }

        [Test]
        public void CheckDefaultWork()
        {
            var gameRepo = GetGameRepository();
            var expectedBlackHolesCount = 6;
            var maxRows = 3;
            var maxCols = 3;

            var actualResult = gameRepo.GetBlackHoles(maxRows, maxCols, expectedBlackHolesCount);

            Assert.AreEqual(actualResult.Count, expectedBlackHolesCount);
            Assert.IsEmpty(actualResult.Where(x => x.Row > maxRows - 1 || x.Row < 0 || x.Column > maxCols - 1 || x.Column < 0));
        }

        [Test]
        public void CheckMaxBlackHoles()
        {
            var gameRepo = GetGameRepository();
            var expectedBlackHolesCount = 9;
            var maxRows = 3;
            var maxCols = 3;

            var actualResult = gameRepo.GetBlackHoles(maxRows, maxCols, expectedBlackHolesCount);

            Assert.AreEqual(actualResult.Count, expectedBlackHolesCount-1);

        }
    }
}
