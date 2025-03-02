using System;
using System.Collections.Generic;

namespace Genetic_Algorithm
{
    internal class Functions
    {
        // Return number of bits (1) in array
        public static int CalculateFitness(int[] subject)
        {
            int fitnessScore = 0;

            foreach (var gene in subject)
            {
                if (gene == 1)
                {
                    fitnessScore++;
                }
            }

            return fitnessScore;
        }

        // Calculating average fitness will give a better picture or population as a whole improves:
        public static double CalculateAverageFitness(int[][] population)
        {
            double totalFitness = 0;

            foreach (var individual in population)
            {
                totalFitness += CalculateFitness(individual);
            }

            return (totalFitness / population.Length);
        }

        // Selecting better parent
        public static int[] SelectBetterParent(int[] parent1, int[] parent2)
        {
            int parent1Fitness = CalculateFitness(parent1);
            int parent2Fitness = CalculateFitness(parent2);

            if (parent1Fitness >= parent2Fitness)
            {
                return parent1;
            }
            else
            {
                return parent2;
            }
        }

        // Selection of parents
        public static int[] ReturnParent(int[][] population, Random random)
        {
            int[] candidate1 = population[random.Next(population.Length)];
            int[] candidate2 = population[random.Next(population.Length)];

            return SelectBetterParent(candidate1, candidate2);
        }

        // Creating child from mixed parents
        public static int[] CreateChild(int[] parent1, int[] parent2, Random random)
        {
            int length = parent1.Length;
            int crossoverPoint = random.Next(1, length - 1);

            int[] child = new int[length];

            for (int i = 0; i < length; i++)
            {
                if (i < crossoverPoint)
                {
                    child[i] = parent1[i];
                }
                else
                {
                    child[i] = parent2[i];
                }
            }

            return child;
        }

        // Mutation of the child
        public static void Mutate(int[] individual, Random random)
        {
            const double mutationRate = 0.05; // 5%

            for (int i = 0; i < individual.Length; i++)
            {
                if (random.NextDouble() < mutationRate)
                {
                    // Flipping the bit
                    if (individual[i] == 0)
                    {
                        individual[i] = 1;
                    }
                    else
                    {
                        individual[i] = 0;
                    }
                }
            }
        }

        // Function to check how many different individuals are in the population
        public static int CountUniqueIndividuals(int[][] population)
        {
            HashSet<string> uniqueIndividuals = new HashSet<string>();

            foreach (var individual in population)
            {
                string dna = string.Join("", individual);
                uniqueIndividuals.Add(dna);
            }

            return uniqueIndividuals.Count;
        }
    }
}
