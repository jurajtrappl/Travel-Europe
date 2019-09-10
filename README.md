# Travel-Europe
    Semestral project for NPRG031
    
# User's documentation
## Abstract
This program can be used to find the shortest path between two cities and list information about the trip. The user has the option to choose the cities he/she wants to travel through. First, the user enters the parameters of the car he/she wants to travel with. Then the user plans the route. In the end, the user will see a detailed route overview, that includes information about the length of the trip, the length of trips between cities, the total time spent on the trip, and the fuel and cost needed.

## Stages
### I. stage: Entering car parameters
When the application starts, the first window appears. The first window asks the user for four details of the car he is planning to     travel with. The required parameters are the type of fuel, fuel consumption in liters per hundred kilometers, tank capacity and maximum        permitted speed. 
   
   ![parameter window](https://i.imgur.com/J0x7qln.jpg)
   
### II. stage: Simulation
The user must select their start and destination city. It is also possible to add cities to go through on the path
from the starting city to the destination city. Once the user has selected both a starting and destination city, then he/she can       proceed to the "Simulation" button. After clicking the button the detailed route overview is shown in the log. If the user wants to      change car parameters, then the user can click on the "Car settings" button. If the user wants to calculate another trip, then the user  can click the "Reset" button.
  
   ![main window](https://i.imgur.com/c0HX3Wr.jpg)
  
### Controls
#### Left mouse click
    Selects starting or destination city. The black circle appears.
    
#### Right mouse click
    Deselects any selected city.
    
#### Ctrl + right mouse click
    It allows you to add a city to your road, which means you’re going through that city. The red circle appears.
    
## Example
  
The user wants to travel from Hamburg to Bratislava. Also, he/she wants to travel through Berlin and Prague.

![example simulation](https://i.imgur.com/M7h8NFe.jpg)

# Programmer's documentation

## Specification breakdown

## Architecture
The project can be split into five high-level stages:

#### 1. Input
The data for the map is stored in a text file in a specific format. The format must be strict because the map is parsed by regexes.
The file can be segmented into three main parts, countries, cities and roads. Before every part, there is a number indicating
how many lines are there for each part. This is the reason why this class has the only global variable in this project.
   
#### 2. Map
The data are stored in a graph data structure which represents a map. Data are located in a text file in a specific format, then they      are parsed using regexes. The map of Europe contains 21 European countries and over 400 cities overall.
   
#### 3. Car
The Vehicle with four parameters helps to provide detailed information about user-defined trip. Four parameters are fuel type, tank          capacity, consumption, and maximum allowed speed.
   
#### 4. Shortest path
Dijkstra's algorithm is the main and the only algorithm that is used in this project. It provides a calculation for finding the          shortest path from a starting city to a destination city.
   
#### 5. Visualization
This stage provides a graphic representation of a path, choosing the cities by clicking on the map, map itself.

## Main classes
#### 1. Input
Input has one class **ParseMap.cs**. This class contains a single method - _ParseInput_ which returns Map.
The Input file is parsed line by line using regexes _(System.Text.RegularExpressions)_.

Input regexes:
   ```csharp
   static readonly string Country = @"(?<CountryCode>[A-Z]*) (?<CountryName>[A-Za-z]*) (?<CurrencyCode>[A-Z]*)";
   static readonly string City = @"(?<Name>[A-Za-z]*) (?<CountryCode>[A-Z]*) (?<XCoord>\d+) (?<YCoord>\d+)";
   static readonly string Road = @"(?<StartCity>[A-Za-z]*) (?<DestinationCity>[A-Za-z]*) (?<MaxAllowedSpeed>\d+)";
   ```

Global integer variable serves as an iterator through the input text file. Lines are matched and stored in a MatchCollection.
For each match in the MatchCollection function tries to match by a specific regex and gather the data, pass it to a constructor.

#### 2. Map
Map as a graph data structure is composed of three classes, City (node), Road (edge), Map (graph). 
Both City and Road inherits from the IDrawable interface. This interface has one method _Draw_ returning nothing. It is simply used to implement drawing a city or road on the map. Apart from graph data structure classes, the map also contains **Country.cs**, **Point.cs**.

##### 2.1 City
Simple class storing all needed and important information about a city and some basic actions. The city has a name, a country, a location (**Point**) and each city know its neighbors - a list of roads to/from the city.

##### 2.2 Road
Class representing an edge in the graph. Each road knows both cities at the ends of the road itself, maximum allowed speed, distance.
Distance is calculated in the Road constructor by map scale and distances between coordinates of both ends.

##### 2.3 Map
This class serves as a graph, it stores all countries and cities in dictionaries. Countries or cities are easily accessible by their names.

##### 2.4 Country, Point
The Country is class used to hold important information that helps to build a better structure for a map. Each country has a code, stores all cities that belong to a country in a list, name, currency.

Point is a simple data class that has two integer variables x, y.

#### 3. Car
Car is represented by a single class **Car.cs**. As for design, this class implements a singleton design pattern. It contains methods to calculate total fuel and fuel expenses.

#### 4. Shortest path
This stage can be divided into two parts. The first part includes the algorithm and the second part includes the data structure, heap.
When the user chooses more than one city, for example, he wants to travel from Prague to Berlin through Munchen, then the city between are stored in Queue and the path is calculated partly.
 
##### 4.1 Dijkstra's algorithm
Dijkstra's algorithm is an algorithm for finding the shortest path between nodes in a graph, which in this particular example represent a map. The implementation of this algorithm is in **Dijkstra.cs**.

The function has one dictionary that stores previous cities of cities. It is used to rebuild the path after the algorithm reaches its end. The function returns a list of cities that are in the shortest path. 
Initialize every other city than the starting city with +inf distance and insert it to a heap. The starting city has 0 distance and it is also inserted into the heap. While the heap is not empty, extract min from heap and relax edges of current min. The algorithm checks whether the destination city has been found yet or not. If yes, then we rebuild the path from the dictionary and break the computation. 

##### 4.2 Heap
For this purpose, the heap implementation is adjusted so the heap is min-heap. Heap uses heap nodes that represent cities during Dijkstra's computation. Basic functions as Insert, BubbleUp, BubbleDown, DecreaseKey, ExtractMin are implemented.

#### 5. Visualization
This project has two forms. One is for entering car parameters, the seconde one depicts map, provides interface between map and user.
Cities on the map remember their position in x,y coordinates, this helps in the terms of calculating distances that are then
assigned to the roads between cities. The distance is calculated by using map scale and distance between coordinates. When a user
selects the city then it is highlighted by a circle.

##### 5.1 Selecting/deselecting cities on the map
Each city has own hitbox that is 6px rectangle around. Whenever user click on the map, function loops through all cities and use method Contains (method in **City.cs**) which returns true/false based on if it was clicked in the hitbox or not.

## Data structures
Data structures implemented in this project are C# generics such as dictionaries and lists and own heap. Dictionaries are used for storing graph information and access time then is really fast. Lists are used in computation of finding the shortest path and other similar functionalities. Heap is used as optimization for Dijkstra's algorithm to store the cities sorted by distances from the starting city.
