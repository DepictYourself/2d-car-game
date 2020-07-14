using Moq;
using NUnit.Framework;
using TrafficRush.Model;
using TrafficRush.Model.game_objects;
using TrafficRush.Repository;

namespace TrafficRush.Logic.Test
{
    /// <summary>
    /// Test class for logic methods.
    /// </summary>
    [TestFixture]
    public class TRLogicTest
    {
        private Mock<IModel> model = new Mock<IModel>();

        private Mock<IRepository> repository = new Mock<IRepository>();

        private TRLogic logic;

        /// <summary>
        /// Setup for tests.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            logic = new TRLogic(model.Object, repository.Object);
        }

        /// <summary>
        /// Tests state saving.
        /// </summary>
        [Test]
        public void ShouldSaveCurrentState()
        {
            // WHEN
            logic.Save();

            // THEN
            repository.Verify(r => r.SaveModelToXml(), Times.Once);
        }

        /// <summary>
        /// Tests state loading.
        /// </summary>
        [Test]
        public void ShouldLoadSavedState()
        {
            // WHEN
            logic.LoadSave();

            // THEN
            repository.Verify(r => r.LoadModelFromXml(), Times.Once);
        }

        /// <summary>
        /// Tests high score loading.
        /// </summary>
        [Test]
        public void ShouldGetHighScore()
        {
            // GIVEN
            double score = 11.0;
            repository.Setup(r => r.GetHighScore()).Returns(score);

            // WHEN
            double result = logic.GetHighScore();

            // THEN
            Assert.AreEqual(score, result);
        }

        /// <summary>
        /// Tests high score saving.
        /// </summary>
        [Test]
        public void ShouldSaveHighScore()
        {
            // GIVEN
            double score = 11.0;
            double modelScore = 50.0;
            repository.Setup(r => r.GetHighScore()).Returns(score);
            model.Setup(m => m.Score).Returns(modelScore);

            // WHEN
            logic.SetHighScore();

            // THEN
            repository.Verify(r => r.SetHighScore(It.IsAny<double>()));
        }

        /// <summary>
        /// Tests lane change from MIDDLE.
        /// </summary>
        /// <param name="direction">Left or right.</param>
        [TestCase(Direction.LEFT)]
        [TestCase(Direction.RIGHT)]
        public void ShouldChangeLaneWhenPositionIsMiddle(Direction direction)
        {
            // GIVEN
            Car car = new Car();
            car.SetX(0);
            car.Position = ActiveLane.MIDDLE;

            // WHEN
            logic.ChangeLane(car, direction);

            // THEN
            if (direction.Equals(Direction.LEFT))
            {
                Assert.AreEqual(ActiveLane.LEFT, car.Position);
            }
            else
            {
                Assert.AreEqual(ActiveLane.RIGHT, car.Position);
            }
        }

        /// <summary>
        /// Tests lane change from LEFT.
        /// </summary>
        [Test]
        public void ShouldNotChangeLaneToLeftWhenPositionIsLeft()
        {
            // GIVEN
            Car car = new Car();
            car.SetX(0);
            car.Position = ActiveLane.LEFT;
            Direction direction = Direction.LEFT;

            // WHEN
            logic.ChangeLane(car, direction);

            // THEN
            Assert.AreEqual(0, car.Area.X);
            Assert.AreEqual(ActiveLane.LEFT, car.Position);
        }

        /// <summary>
        /// Tests lane change from RIGHT.
        /// </summary>
        [Test]
        public void ShouldNotChangeLaneToRightWhenPositionIsRight()
        {
            // GIVEN
            Car car = new Car();
            car.SetX(0);
            car.Position = ActiveLane.RIGHT;
            Direction direction = Direction.RIGHT;

            // WHEN
            logic.ChangeLane(car, direction);

            // THEN
            Assert.AreEqual(0, car.Area.X);
            Assert.AreEqual(ActiveLane.RIGHT, car.Position);
        }
    }
}
