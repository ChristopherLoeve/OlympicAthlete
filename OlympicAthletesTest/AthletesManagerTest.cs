using System.Collections.Generic;
using System.Linq;
using AthleteAPI.Interfaces;
using AthleteAPI.Managers;
using AthleteLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OlympicAthletesTest
{
    [TestClass]
    public class AthletesManagerTest
    {
        private AthletesManager _mgr;

        [TestInitialize]
        public void Setup()
        {
            _mgr = new AthletesManager();
            _mgr.TESTCleanUp();
        }

        [TestCleanup]
        public void CleanUp()
        {
            _mgr.TESTCleanUp();
        }

        [TestMethod]
        public void GetAllTest()
        {
            int expectedCount = 7;
            int actualCount = _mgr.Get().Count();

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void GetSingle_ValidId()
        {
            Athlete expectedAthlete = new Athlete(3, "Mikkel", "Greece", 183);

            Athlete actualAthlete = _mgr.Get(3);

            Assert.AreEqual(expectedAthlete, actualAthlete);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        [DataRow(0)] 
        public void GetSingle_InvalidId(int id)
        {
            Athlete actualAthlete = _mgr.Get(id);
        }

        [TestMethod]
        [DataRow(0, "Pelle", "Turkey", 150, 8)]
        [DataRow(2, "Pelle", "Turkey", 150, 9)]
        public void CreateTest(int id, string name, string country, double height, int expectedId)
        {
            var originalListLength = _mgr.Get().Count();
            var athleteToCreate = new Athlete(id, name, country, height);

            var result = _mgr.Create(athleteToCreate);
            Assert.IsTrue(result);

            // CHECK LIST SIZE HAS INCREASED
            var newListLength = _mgr.Get().Count();
            Assert.AreEqual(originalListLength + 1, newListLength);
            // CHECK ID WAS ASSIGNED CORRECTLY
            var createdAthlete = _mgr.Get().Last();
            Assert.AreEqual(expectedId, createdAthlete.Id);
        }

        [TestMethod]
        public void DeleteTest_ValidId()
        {
            var originalListLength = _mgr.Get().Count();
            var expectedAthleteDeleted = new Athlete(3, "Mikkel", "Greece", 183);

            var actualAthleteDeleted = _mgr.Delete(3);
            var newListLength = _mgr.Get().Count();

            Assert.AreEqual(expectedAthleteDeleted, actualAthleteDeleted);
            Assert.AreEqual(originalListLength - 1, newListLength);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void DeleteTest_InvalidID()
        {
            _mgr.Delete(0);
        }

        [TestMethod]
        public void FilterByName_NameExists()
        {
            var expectedCount = 1;
            var expectedResult = new Athlete(2, "Steven", "United Kingdom", 185.5);

            var actualResult = _mgr.FilterByName("ven");
            var actualCount = actualResult.Count();

            Assert.AreEqual(expectedCount, actualCount);
            Assert.AreEqual(expectedResult, actualResult.First());
        }

        [TestMethod]
        public void FilterByName_NameDoesntExist()
        {
            var expectedCount = 0;

            var actualResult = _mgr.FilterByName("fuyfyupfuig");
            var actualCount = actualResult.Count();

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void FilterByCountry_CountryExists()
        {
            var expectedCount = 1;
            var expectedResult = new Athlete(1, "Henrik", "Denmark", 155.2);

            var actualResult = _mgr.FilterByCountry("Denmark");
            var actualCount = actualResult.Count();

            Assert.AreEqual(expectedCount, actualCount);
            Assert.AreEqual(expectedResult, actualResult.First());
        }

        [TestMethod]
        public void FilterByCountry_CountryDoesntExist()
        {
            var expectedCount = 0;

            var actualResult = _mgr.FilterByCountry("fuyfyupfuig");
            var actualCount = actualResult.Count();

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        [DataRow(150, 170, 1)]
        [DataRow(170, 185, 4)]
        [DataRow(183, 183, 2)]
        [DataRow(100, 120, 0)]
        public void FilterByHeight(int minHeight, int maxHeight, int expectedCount)
        {
            var result = _mgr.FilterByHeight(minHeight, maxHeight);
            var actualCount = result.Count();

            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
