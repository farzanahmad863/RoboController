using RoboController;
using System.Text.RegularExpressions;

bool validInput = false;

Coordinate roomBoundary = new(0, 0);
Coordinate position = new(0, 0);
Direction direction = Direction.N;
char[] navigationCommands = [];

while (!validInput)
{
    Console.WriteLine("Please enter the room size, width and depth");
    string roomBoundaryInput = Console.ReadLine();
    Console.WriteLine("Please give robot's starting postion and direction");
    string positionInput = Console.ReadLine();
    Console.WriteLine("Please enter navigation commands");
    string commandsInput = Console.ReadLine();

    string[] roomBoundaryInputArr = roomBoundaryInput.Split(" ");
    string[] positionInputArr = positionInput.Split(" ");

    int width;
    int depth;
    Direction robotDirection;

    if (roomBoundaryInputArr.Length == 2
        && int.TryParse(roomBoundaryInputArr[0], out width)
        && int.TryParse(roomBoundaryInputArr[1], out depth))
    {
        roomBoundary = new(width, depth);
        validInput = true;
    }
    else
    {
        validInput = false;
    }


    if (positionInputArr.Length == 3
        && int.TryParse(positionInputArr[0], out width)
        && int.TryParse(positionInputArr[1], out depth)
        && Enum.TryParse(positionInputArr[2], true, out robotDirection))
    {
        position = new(width, depth);
        direction = robotDirection;
        validInput = true;
    }
    else
    {
        validInput = false;
    }

    Regex r = new Regex(@"[^lrf]+", RegexOptions.IgnoreCase);

    if (validInput
        && commandsInput?.Trim() != ""
        && commandsInput?.Length > 0
        && !r.IsMatch(commandsInput))
    {
        navigationCommands = commandsInput.ToCharArray();
        validInput = true;
    }
    else
    {
        validInput = false;
    }

    if (!validInput)
    {
        Console.Clear();
        Console.WriteLine("Invalid input format");
        Console.WriteLine("please write room size as width and depth separated with space e.g 3 3");
        Console.WriteLine("please give robot's starting position as width, depth and direction(N=North,E=East,W=West,S=South) separated with space e.g 1 3 N");
        Console.WriteLine("please write navigation commands L=left, R=right, F=walkforward e.g LRFFLF");
    }
    else
    {
        try
        {
            Robot robot = new(roomBoundary, position, direction);
            foreach (char command in navigationCommands)
            {
                robot.Navigate(command);
            };
            Console.WriteLine("Report: " + robot.position.X.ToString() + " " + robot.position.Y.ToString() + " " + robot.direction);
            Environment.Exit(0);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            Environment.Exit(1);
        }
    }
}