﻿using System;

namespace SnakeGame
{
    class Program
    {
        static void Main(string[] args)
        {
        begining:
            int[] xAxis = new int[30];
            int[] yAxis = new int[30];

            xAxis[0] = 15;
            yAxis[0] = 10;

            bool isAlive = true;
            Console.CursorVisible = false;

            // Sets the movement speed of the snake
            int speed = 150;

            Random applePosition = new Random();
            int xAxisApple = 0;
            int yAxisApple = 0;

            int collectedApples = 0;
            int points = 0;
            string answer;

            Console.WriteLine("Use the Arrow keys to play the game! Good luck! (press any key to continue)");
            Console.ReadKey();
            Console.Clear();
            createBoundaries();

            // Generates the snake starting position
            snakePosition(xAxis, yAxis, out xAxis, out yAxis, collectedApples, isAlive, out isAlive);

            // Generates a random apple position within the bounds of the field
            apple(xAxisApple, yAxisApple, out xAxisApple, out yAxisApple, speed, out speed, collectedApples, out collectedApples, points, out points, applePosition, out applePosition);

            ConsoleKey userInputKey = Console.ReadKey().Key;
            do
            {
                do
                {
                    System.Threading.Thread.Sleep(speed);

                    if (Console.KeyAvailable)
                    {
                        userInputKey = Console.ReadKey().Key;
                    }

                    // Listens to tuser input
                    switch (userInputKey)
                    {
                        case ConsoleKey.UpArrow:
                            Console.SetCursorPosition(xAxis[0], yAxis[0]);
                            Console.WriteLine(" ");
                            yAxis[0]--;
                            break;
                        case ConsoleKey.DownArrow:
                            Console.SetCursorPosition(xAxis[0], yAxis[0]);
                            Console.WriteLine(" ");
                            yAxis[0]++;
                            break;
                        case ConsoleKey.RightArrow:
                            Console.SetCursorPosition(xAxis[0], yAxis[0]);
                            Console.WriteLine(" ");
                            xAxis[0]++;
                            break;
                        case ConsoleKey.LeftArrow:
                            Console.SetCursorPosition(xAxis[0], yAxis[0]);
                            Console.WriteLine(" ");
                            xAxis[0]--;
                            break;
                    }
                    if (xAxis[0] == xAxisApple && yAxis[0] == yAxisApple)
                    {
                        // Generates a new apple at a random location if collected
                        apple(xAxisApple, yAxisApple, out xAxisApple, out yAxisApple, speed, out speed, collectedApples, out collectedApples, points, out points, applePosition, out applePosition);
                    }

                    // Updates snake position
                    snakePosition(xAxis, yAxis, out xAxis, out yAxis, collectedApples, isAlive, out isAlive);

                    // Checks if the snake hit a wall or not
                    wallHit(isAlive, xAxis[0], yAxis[0], out isAlive, out xAxis[0], out yAxis[0]);
                }
                while (isAlive == true);

                Console.SetCursorPosition(10, 5);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You have lost! Total points collected: {0}", points);
                Console.WriteLine("Do you wish to continue? y/n");
            
                // Checks if user wants to continue the game or not
                answer = Console.ReadLine();
                if (answer == "y" || answer == "Y")
                {
                    Console.Clear();
                    isAlive = true;
                    goto begining;
                }
                else
                    break;
            } while (true);
        }

        private static void snakePosition(int[] xAxis, int[] yAxis, out int[] xAxis2, out int[] yAxis2, int collectedApples, bool isAlive, out bool isAlive2)
        {
            Console.SetCursorPosition(xAxis[0], yAxis[0]);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("O");

            for (int i = 1; i < collectedApples + 1; i++)
            {
                Console.SetCursorPosition(xAxis[i], yAxis[i]);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("o");

                if (xAxis[i] == xAxis[0] && yAxis[i] == yAxis[0])
                    isAlive = false;
            }

            Console.SetCursorPosition(xAxis[collectedApples + 1], yAxis[collectedApples + 1]);
            Console.WriteLine(" ");

            for (int i = collectedApples + 1; i > 0; i--)
            {
                xAxis[i] = xAxis[i - 1];
                yAxis[i] = yAxis[i - 1];
            }

            xAxis2 = xAxis;
            yAxis2 = yAxis;
            isAlive2 = isAlive;
        }

        private static void apple(int xAxisApple, int yAxisApple, out int xAxisApple2, out int yAxisApple2, int speed, out int speed2, int collectedApples, out int collectedApples2, int points, out int points2, Random applePosition, out Random applePosition2)
        {
            xAxisApple = applePosition.Next(3, 19);
            yAxisApple = applePosition.Next(3, 19);

            Console.SetCursorPosition(xAxisApple, yAxisApple);
            Console.WriteLine((Char)243);

            speed -= 10;
            points += 20;
            collectedApples++;

            xAxisApple2 = xAxisApple;
            yAxisApple2 = yAxisApple;
            speed2 = speed;
            points2 = points;
            collectedApples2 = collectedApples;
            applePosition2 = applePosition;
        }

        private static void wallHit(bool isAlive, int xAxis, int yAxis, out bool isAlive2, out int xAxis2, out int yAxis2)
        {
            if (xAxis == 1 || yAxis == 1 || xAxis == 51 || yAxis == 20)
                isAlive = false;

            xAxis2 = xAxis;
            yAxis2 = yAxis;
            isAlive2 = isAlive;
        }

        private static void createBoundaries()
        {
            for (int i = 1; i < 21; i++)
            {
                Console.SetCursorPosition(1, i);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("#");
                Console.SetCursorPosition(51, i);
                Console.WriteLine("#");
            }
            for (int i = 1; i < 51; i++)
            {
                Console.SetCursorPosition(i, 1);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("#");
                Console.SetCursorPosition(i, 20);
                Console.WriteLine("#");
            }
        }
    }
}