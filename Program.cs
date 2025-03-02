using System;
using System.Linq;

namespace Genetic_Algorithm
{
    internal class Program
    {
        static void PrintPopulation(int[][] population)
        {
            for (int i = 0; i < population.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(string.Join("", population[i]));
                Console.ResetColor();

                Console.Write(" | Fitness --> ");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(Functions.CalculateFitness(population[i]));
                Console.ResetColor();
            }
        }

        static void Main(string[] args)
        {
            Random random = new Random();

            const int numberOfPositions = 10; // The number of genes in the individual
            const int numberOfSubjects = 8; // Number of individuals in the population
            const int iterationCounter = 6; // Number of full algorithm iterations

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("-------------------------------");
            Console.WriteLine($"Running Genetic Algorithm with:");
            Console.WriteLine($"Population size: {numberOfSubjects}");
            Console.WriteLine($"Gene length: {numberOfPositions}");
            Console.WriteLine($"Iterations: {iterationCounter}");
            Console.WriteLine("-------------------------------");
            Console.WriteLine();
            Console.ResetColor();

            // Population initialization - each individual is a table with values: {0 or 1}
            int[][] population = new int[numberOfSubjects][];
            for (int i = 0; i < population.Length; i++)
            {
                population[i] = new int[numberOfPositions];
                for (int j = 0; j < numberOfPositions; j++)
                {
                    population[i][j] = random.Next(2);
                }
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("First population:");
            Console.ResetColor();
            PrintPopulation(population);

            /* 
                Main algorithm loop
            */
            for (int i = 0; i < iterationCounter; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nIteration number: {i + 1}, new population: ");
                Console.ResetColor();

                int[][] newPopulation = new int[numberOfSubjects][];

                double averageFitness = Functions.CalculateAverageFitness(population);
                int uniqueIndividuals = Functions.CountUniqueIndividuals(population);

                Console.Write("Average fitness: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{averageFitness:F2}");
                Console.ResetColor();

                Console.Write("Unique individuals: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(uniqueIndividuals);
                Console.ResetColor();

                // Optional elitism - maintaining the best individual
                int[] bestIndividual = population.OrderByDescending(Functions.CalculateFitness).First();
                newPopulation[0] = (int[])bestIndividual.Clone(); // keeping the best one

                for (int j = 1; j < numberOfSubjects; j += 2)
                {
                    // Choosing 2 parents
                    int[] parent1 = Functions.ReturnParent(population, random);
                    int[] parent2 = Functions.ReturnParent(population, random);

                    // Crossing (creating two children)
                    int[] child1 = Functions.CreateChild(parent1, parent2, random);
                    int[] child2 = Functions.CreateChild(parent1, parent2, random);

                    // Mutation of children
                    Functions.Mutate(child1, random);
                    Functions.Mutate(child2, random);

                    // Inserting children in a new population
                    newPopulation[j] = child1;
                    if (j + 1 < numberOfSubjects)
                    {
                        newPopulation[j + 1] = child2;
                    }
                }

                // Replacing the old population with new one
                population = newPopulation;

                PrintPopulation(population);
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\nEnd of algorithm, press any key.");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}

/*
    TO DO:
    - add inputs for main three values with validation
*/
