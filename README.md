This repository contains solution for Travelling salesman problem using genetic algorithm.
[City](TSPGA/City.cs) is represented by X and Y coordiantes and unique id. A [Chromosome](TSPGA/Chromosome.cs) consists of
tour order of the cities, fitness function that represents total distance of the tour and goodness representing the
quality of the tour (smaller distance means better quality of the chromosome).

Steps of the genetic algorithm are implemented in the class [GAUtils](TSPGA/GAUtils.cs).

Process of elitism is implemented to ensure the best chromosomes from generation are transferred to next generation
unchanged. If you don't want to use elitism you should comment/remove this line from [Simulation](TSPGA/Simulation.cs) file
```csharp
newPop.AddRange(pop.OrderBy(x => x.fitnessValue).ToList().Take(20));

```

## This project contains

### Mutation operators

- [Swap Mutation Operator](TSPGA/SwapMutationOperator.cs)
- [Centre Inverse Mutation Operator](TSPGA/CentreInverseMutationOperator.cs)
- [Reverse Sequence Mutation Operator](TSPGA/ReverseSequenceMutationOperator.cs)

### Crossover operators

- [Partially Mapped Crossover Operator](TSPGA/PartiallyMappedCrossoverOperator.cs)
- [Order Crossover Operator](TSPGA/OrderCrossoverOperator.cs)
	
### Creating additional mutation operators
Mutation operators must implement the [IMutationOperator](TSPGA/IMutationOperator.cs) interface.
```csharp
public class MyMutationOperator : IMutationOperator
{
	public Chromosome Mutation(Chromosome parent, int length)
	{
		//your mutation algorithm here
	}
}
```

### Creating additional crossover operators
Mutation operators must implement the [ICrossoverOperator](TSPGA/ICrossoverOperator.cs) interface.
```csharp
public class MyCrossoverOperator : ICrossoverOperator
{
	public Tuple<Chromosome, Chromosome> Crossover(Chromosome parent1, Chromosome parent2, int length)
	{
		//your crossover algorithm here
	}
}
```

### Running a simulation
Simulation consists of testing different mutation and crossover operators with different values for mutation and crossover probability.
Simulation is ran multiple times for fixed parameters in order to find best average and worst solution for a given set of parameters. 
Simulation is ran for a specified number of times and stopping condition is maximum number of generations reached.
Files with coordinates of the cities and with optimal route should also be supplied.
```csharp
List<ICrossoverOperator> crossovers = new List<ICrossoverOperator>() { new PartiallyMappedCrossoverOperator(), new OrderCrossoverOperator() };
List<IMutationOperator> mutations = new List<IMutationOperator> { new CentreInverseMutationOperator(), new SwapMutationOperator(), new ReverseSequenceMutationOperator()};
List<double> cp = new List<double> { 0.7, 0.8, 0.9 };
List<double> mp = new List<double> { 0.1, 0.2, 0.3 };
int gens = 200;
int sims = 10;
int population = 100;
Simulation simulation = new Simulation(sims, "berlin52.txt", "berlin52.opt.txt", mutations, crossovers, mp, cp, gens, population);
simulation.runSimulation();
```

### Example results
Running and example on [berlin52.txt](TSPGA/bin/Debug/berlin52.txt) testing all mutation and crossover operators with generation on 10 simulations with maximum generation size of 200.
Best soluton is provided by OrderCrossoverOperator for crossover and ReverseSequenceMutationOperator with 0.9 crossover probability and 0.3 mutation probability
Best: 8965.85907275254 Average: 9753.54665457777 Worst: 10398.9931042185.
Optimal solution is: 7528.17840838357.
