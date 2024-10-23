namespace RoboController.Tests
{
    public class UnitTests
    {
        Coordinate roomBoundary = new(5, 5);
        Coordinate startPosition = new(3, 3);

        [Fact]
        public void Navigate_Forward_With_Command_F()
        {
            //arrange
            Robot robot = new(roomBoundary, startPosition, Direction.S);

            Coordinate expected = new(3, 4);

            //act 
            robot.Navigate('F');

            //assert
            Assert.Equal(expected, robot.position);
        }

        [Fact]
        public void Navigate_Left_With_Command_L()
        {
            //arrange
            Robot robot = new(roomBoundary, startPosition, Direction.S);

            Direction expected = Direction.E;

            //act 
            robot.Navigate('L');

            //assert
            Assert.Equal(expected, robot.direction);
        }

        [Fact]
        public void Navigate_Right_With_Command_R()
        {
            //arrange
            Robot robot = new(roomBoundary, startPosition, Direction.S);

            Direction expected = Direction.W;

            //act 
            robot.Navigate('R');

            //assert
            Assert.Equal(expected, robot.direction);
        }

        [Fact]
        public void Navigate_Invalid_Forward_Command_Throws()
        {
            //arrange
            Robot robot = new(roomBoundary, new Coordinate(0, 0), Direction.N);

            //act and assert
            Assert.Throws<Exception>(() => robot.Navigate('F'));
        }

        [Fact]
        public void Navigagte_Invalid_Room_Boundry_Throws()
        {
            //act and assert
            Assert.Throws<ArgumentException>(() => new Robot(new Coordinate(0, 0), startPosition, Direction.N));
        }

        [Fact]
        public void Initialization_Invalid_Initial_Position_Throws()
        {
            //act and assert
            Assert.Throws<ArgumentException>(() => new Robot(new Coordinate(5, 5), new Coordinate(6, 7), Direction.N));
        }
    }
}