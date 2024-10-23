namespace RoboController
{
    public struct Coordinate
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }
    }
    public enum Direction
    {
        // Represents directions North, East, South and West 
        N = 'N',
        E = 'E',
        S = 'S',
        W = 'W'
    }
    public class Robot
    {
        public Coordinate roomBoundary;
        public Coordinate position;
        public Direction direction;

        public Robot(Coordinate roomBoundry, Coordinate position, Direction direction)
        {
            ValidateParameters(roomBoundry, position);

            this.roomBoundary = roomBoundry;
            this.position = position;
            this.direction = direction;
        }

        private static void ValidateParameters(Coordinate roomBoundry, Coordinate position)
        {
            if (roomBoundry.X == 0 && roomBoundry.Y == 0)
            {
                throw new ArgumentException("Invalid room boundry");
            }
            else if (position.X < 0 || position.Y < 0 || position.X > roomBoundry.X || position.Y > roomBoundry.Y)
            {
                throw new ArgumentException("Invalid starting position");
            }
        }

        private bool CheckRoomBoundary()
        {
            switch (direction)
            {
                case Direction.N:
                    return position.Y - 1 >= 0;
                case Direction.E:
                    return position.X + 1 <= roomBoundary.X;
                case Direction.S:
                    return position.Y + 1 <= roomBoundary.Y;
                case Direction.W:
                    return position.X - 1 >= 0;
                default:
                    return false;
            }
        }

        private void Turn(char turn)
        {
            if (turn == 'R')
            {
                switch (this.direction)
                {
                    case Direction.N:
                        direction = Direction.E;
                        break;
                    case Direction.E:
                        direction = Direction.S;
                        break;
                    case Direction.S:
                        direction = Direction.W;
                        break;
                    case Direction.W:
                        direction = Direction.N;
                        break;
                    default:
                        break;
                }
            }
            else if (turn == 'L')
            {
                switch (this.direction)
                {
                    case Direction.N:
                        direction = Direction.W;
                        break;
                    case Direction.E:
                        direction = Direction.N;
                        break;
                    case Direction.S:
                        direction = Direction.E;
                        break;
                    case Direction.W:
                        direction = Direction.S;
                        break;
                    default:
                        break;
                }
            }
        }
        private void Walk()
        {
            if (!CheckRoomBoundary())
            {
                throw new Exception("Robot walked outside of the room bounds");
            }
            switch (direction)
            {
                case Direction.N:
                    position.Y -= 1;
                    break;
                case Direction.E:
                    position.X += 1;
                    break;
                case Direction.S:
                    position.Y += 1;
                    break;
                case Direction.W:
                    position.X -= 1;
                    break;
                default:
                    break;
            }
        }

        public void Navigate(char command)
        {
            if (command == 'F')
            {
                Walk();
            }
            else if (command == 'L' || command == 'R')
            {
                Turn(command);
            }
        }
    }
}