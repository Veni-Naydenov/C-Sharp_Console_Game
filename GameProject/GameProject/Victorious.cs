using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

struct Object
{
    public int X;
    public int Y;
    public char C;
    public ConsoleColor Color;
}
class Victorious
{
    static int fieldWidth = 39;
    static int fieldHeight = 24;
    static int playerPosition = 10; //start player position
    static int playerWidth = 3;
    static DateTime start;
    static void DrawPlayer()
    {
        //symbols for the player-airplane
        char body = Encoding.GetEncoding(437).GetChars(new byte[] { 207 })[0];
        char leftWing = Encoding.GetEncoding(437).GetChars(new byte[] { 202 })[0];
        char rightWing = Encoding.GetEncoding(437).GetChars(new byte[] { 202 })[0];
        char front = Encoding.GetEncoding(437).GetChars(new byte[] { 186 })[0];
        //draw every part ot the airplane
        PrintOnPosition(playerPosition, fieldHeight, leftWing, ConsoleColor.Yellow);
        PrintOnPosition(playerPosition + 1, fieldHeight - 1, front, ConsoleColor.Yellow);
        PrintOnPosition(playerPosition + 1, fieldHeight, body, ConsoleColor.White);
        PrintOnPosition(playerPosition + 2, fieldHeight, rightWing, ConsoleColor.Yellow);

    }
    static void PrintOnPosition(int X, int Y, char C,
        ConsoleColor Color = ConsoleColor.Gray)
    {
        Console.SetCursorPosition(X, Y);
        Console.ForegroundColor = Color;
        Console.Write(C);
    }
    static void PrintStringOnPosition(int X, int Y, string str,
        ConsoleColor Color = ConsoleColor.Gray)
    {
        Console.SetCursorPosition(X, Y);
        Console.ForegroundColor = Color;
        Console.Write(str);
    }
    static void MovePlayerLeft()
    {
        if (playerPosition > 0)
        {
            playerPosition--;
        }
    }
    static void MovePlayerRight()
    {
        if (playerPosition < fieldWidth - playerWidth + 1)
        {
            playerPosition++;
        }
    }
    static void MovePlayerUp()
    {
        if (fieldHeight > 0)
        {
            fieldHeight--;
        }
    }
    static void MovePlayerDown()
    {
        if (fieldHeight < 24)
        {
            fieldHeight++;
        }
    }
    //set random Color for all falling objects
    static ConsoleColor RandomColor(Object o, int randomColor)
    {
        switch (randomColor)
        {
            case 0: return o.Color = ConsoleColor.Yellow;

            case 1: return o.Color = ConsoleColor.White;

            case 2: return o.Color = ConsoleColor.Red;

            case 3: return o.Color = ConsoleColor.Magenta;

            case 4: return o.Color = ConsoleColor.Green;

            case 5: return o.Color = ConsoleColor.Gray;

            case 6: return o.Color = ConsoleColor.DarkYellow;

            case 7: return o.Color = ConsoleColor.DarkRed;

            case 8: return o.Color = ConsoleColor.DarkMagenta;

            case 9: return o.Color = ConsoleColor.DarkGreen;

            case 10: return o.Color = ConsoleColor.DarkGray;

            case 11: return o.Color = ConsoleColor.DarkCyan;

            case 12: return o.Color = ConsoleColor.DarkBlue;

            case 13: return o.Color = ConsoleColor.Cyan;

            case 14: return o.Color = ConsoleColor.Blue;

            case 15: return o.Color = Console.BackgroundColor = ConsoleColor.Yellow;
            default:
                return o.Color = ConsoleColor.White;
        }
    }

    static void Main()
    {
        int destroyed = 0;
        start = DateTime.Now;

        char[] rocks = new char[] { '^', '@', '*', '&', '+', '%', '$', '#', '!', '.', ';', '-', '\u0000' };
        //f is the symbol for the bullet -ASCII code 176
        char f = Encoding.GetEncoding(437).GetChars(new byte[] { 176 })[0];
        char[] bullets = new char[] { f };

        int speed = 180;
        int livesCount = 7;
        int startFallingPosition = 3;
        //symbol for the sky
        char s = Encoding.GetEncoding(437).GetChars(new byte[] { 177 })[0];
        Object sky = new Object();
        sky.Y = 2;
        sky.Color = ConsoleColor.Blue;
        sky.C = s;
        //set the console dimentions
        Console.BufferHeight = Console.WindowHeight = 25;
        Console.BufferWidth = Console.WindowWidth = 40;

        Random randomGenerator = new Random();
        List<Object> objects = new List<Object>();
        List<Object> bulletsList = new List<Object>();
        Object newObject = new Object();

        while (true)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;

            for (int i = 0; i <= fieldWidth; i++)
            {
                PrintOnPosition(sky.X = i, sky.Y, sky.C, sky.Color);
            }
            bool hitted = false;
            {
                int checkPosition = randomGenerator.Next(0, 4);
                int randomRock = randomGenerator.Next(0, 11);
                int randomColor = randomGenerator.Next(0, 16);
                newObject.Color = RandomColor(newObject, randomColor);

                if (checkPosition == 0)
                {
                    int randomPosition = randomGenerator.Next(0, 20);
                    newObject.X = randomPosition;
                }
                else if (checkPosition == 2)
                {
                    int randomPosition = randomGenerator.Next(28, 29);
                    newObject.X = randomPosition;
                    newObject.C = rocks[12];
                }
                else if (checkPosition == 3)
                {
                    int randomPosition = randomGenerator.Next(1, 7);
                    newObject.X = randomPosition;
                }
                else
                {
                    int randomPosition = randomGenerator.Next(4, 20);
                    newObject.X = randomPosition;
                }
                if (checkPosition != 2)
                {
                    newObject.C = rocks[randomRock];
                }

                newObject.Y = startFallingPosition;
                objects.Add(newObject);
                checkPosition = Math.Abs(checkPosition - 1);
                randomRock = randomGenerator.Next(0, 12);
                Object newObject2 = new Object();

                if (checkPosition == 0)
                {
                    int randomPosition = randomGenerator.Next(25, 38);
                    newObject2.X = randomPosition;
                }
                else if (checkPosition == 2)
                {
                    int randomPosition = randomGenerator.Next(0, 1);
                    newObject2.X = randomPosition;
                    newObject2.C = rocks[12];
                }
                else if (checkPosition == 3)
                {
                    int randomPosition = randomGenerator.Next(0, 3);
                    newObject2.X = randomPosition;
                }
                else
                {
                    int randomPosition = randomGenerator.Next(10, 39);
                    newObject2.X = randomPosition;
                }
                if (checkPosition != 2)
                {
                    newObject2.C = rocks[randomRock];
                }

                newObject2.Color = RandomColor(newObject2, randomColor);
                newObject2.Y = startFallingPosition;
                objects.Add(newObject2);
            }

            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    MovePlayerLeft();
                }
                if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    MovePlayerRight();
                }
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    MovePlayerUp();
                }
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    MovePlayerDown();
                }
                if (keyInfo.Key == ConsoleKey.Spacebar)
                {
                    Object objectBullet = new Object();
                    objectBullet.C = bullets[0];
                    objectBullet.X = playerPosition + 1;
                    objectBullet.Y = fieldHeight - 2;
                    objectBullet.Color = ConsoleColor.White;
                    bulletsList.Add(objectBullet);
                }
            }
            //list for the falling objects
            List<Object> newList = new List<Object>();
            //list with all active bullets
            List<Object> newListFire = new List<Object>();

            for (int i = 0; i < objects.Count; i++)
            {
                Object oldRock = objects[i];//get every object-rocks

                newObject = new Object();//new object with new Y-coordinates,the rocks are falling
                newObject.X = oldRock.X;
                newObject.Y = oldRock.Y + 1;
                newObject.C = oldRock.C;
                newObject.Color = oldRock.Color;

                if ((((newObject.Y == fieldHeight
                    && (newObject.X == playerPosition || newObject.X == playerPosition + 1 || newObject.X == playerPosition + 2))
                    || ((newObject.Y == fieldHeight - 1) && (newObject.X == playerPosition + 1)))
                    && newObject.C != rocks[12]))
                {
                    livesCount--;
                    hitted = true;

                    if (livesCount <= 0) //Game Over
                    {
                        TimeSpan timeDiff = DateTime.Now - start;
                        TimeSpan result = timeDiff;

                        PrintStringOnPosition(0, 1, "GAME OVER!", ConsoleColor.Red);
                        PrintStringOnPosition(12, 1, "Destructed aim:" + destroyed, ConsoleColor.Green);
                        PrintStringOnPosition(0, 0, "Endurance: " + result);

                        string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                        string resultFile = System.IO.Path.Combine(desktop, "GameResult.txt");
                        //Game over, write the results in file
                        try
                        {
                            StreamWriter writer = new StreamWriter(resultFile);
                            using (writer)
                            {
                                string one = "Destructed aim:" + destroyed;
                                string endurance = "Endurance: " + result;
                                writer.WriteLine(one);
                                writer.WriteLine(endurance);
                            }
                        }
                        catch (UnauthorizedAccessException)
                        {
                            Console.WriteLine("Access Denied,security or I/O error!");
                        }
                        catch (ArgumentNullException)
                        {
                            Console.WriteLine("Invalid value or null !");
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("Invalid value or argument!");
                        }
                        catch (DirectoryNotFoundException)
                        {
                            Console.WriteLine("Can't find the path to the file or directory !");
                        }
                        catch (PathTooLongException)
                        {
                            Console.WriteLine("Too long path or file name!");
                        }
                        catch (IOException)
                        {
                            Console.WriteLine("Error writing file!");
                        }
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                if (newObject.Y < Console.WindowHeight)
                {
                    newList.Add(newObject);
                }
            }

            for (int j = 0; j < bulletsList.Count; j++)
            {
                Object oldBullet = bulletsList[j];
                Object newObjectBullet = new Object();

                newObjectBullet.X = oldBullet.X;
                newObjectBullet.Y = oldBullet.Y - 1;
                newObjectBullet.C = oldBullet.C;
                newObjectBullet.Color = oldBullet.Color;

                if (newObjectBullet.Y > 2) // 2 is the sky level, Y=2. It is still flying.
                {
                    newListFire.Add(newObjectBullet);
                }
            }
            //check for collision(bullet-rock) and remove the rock if has hit.
            for (int k = 0; k < bulletsList.Count; k++)
            {
                for (int r = 0; r < newList.Count; r++)
                {
                    if ((bulletsList[k].Y == newList[r].Y && bulletsList[k].X == newList[r].X))
                    {
                        destroyed++;
                        speed += 5;
                        PrintOnPosition(newList[r].X, newList[r].Y, rocks[12], ConsoleColor.Black);//make the rock Null
                        newList.Remove(newList[r]);//remove the rock
                        PrintOnPosition(newListFire[k].X, newListFire[k].Y, '\u0000', ConsoleColor.Black);
                        newListFire.Remove(newListFire[k]);//remove the bullet
                    }
                }
            }

            objects = newList;

            if (hitted)
            {
                objects.Clear();
                speed = 180;
                Console.BackgroundColor = ConsoleColor.Red;
                PrintOnPosition(playerPosition + 1, fieldHeight, 'X', ConsoleColor.Red);
            }
            else
            {
                DrawPlayer();
            }

            foreach (Object rock in objects)
            {
                PrintOnPosition(rock.X, rock.Y, rock.C, rock.Color);
            }

            bulletsList.Clear();
            //add the active bullets in the list with bullets
            foreach (Object bomb in newListFire)
            {
                bulletsList.Add(bomb);
                PrintOnPosition(bomb.X, bomb.Y, bomb.C, bomb.Color);
            }

            newListFire.Clear();

            PrintStringOnPosition(1, 0, "Lives: " + livesCount, ConsoleColor.Green);
            PrintStringOnPosition(14, 0, "|Play|", ConsoleColor.White);
            PrintStringOnPosition(21, 0, "=^=", ConsoleColor.White);
            PrintStringOnPosition(26, 0, "SPACE to FIRE", ConsoleColor.Cyan);
            PrintStringOnPosition(1, 1, "Hits: " + destroyed, ConsoleColor.Red);
            PrintStringOnPosition(12, 1, "-VICTORIOUS-", ConsoleColor.White);

            int realSpeed = 180 - speed;

            if (realSpeed < 0)
            {
                PrintStringOnPosition(26, 1, "Slow mode !", ConsoleColor.Green);
            }
            else
            {
                PrintStringOnPosition(26, 1, "Speed:" + realSpeed, ConsoleColor.Yellow);
            }

            if (speed > 0)
            {
                speed -= 1;
            }

            Thread.Sleep(speed);
        }
    }
}

