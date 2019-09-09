# Travel-Europe
    Semestral project for NPRG031
    
# User's documentation
## Abstract
This program is used to find the shortest path between two cities and list information about the trip. The user has the option to choose the cities he / she wants to travel through. First, the user enters the parameters of the car he / she wants to travel. Then the user plans the route. At the end, the user will see a detailed route overview, that includes information about the length of the trip, the length of trips between cities, the total time spent on the trip, and the fuel and cost needed.

## Stages
### I. stage: Entering car parameters
   When the application starts, the first window appears. The first window asks the user for four details of the car he is planning to        travel. The required parameters are the type of fuel, fuel consumption in liters per hundred kilometers, tank capacity and maximum        permitted speed. 
   
   ![parameter window](https://i.imgur.com/J0x7qln.jpg)
   
### II. stage: Simulation
  The user must select their start and destination city. It is also possible to add cities to go through on the path
  from the starting city to the destination city. Once the user has selected both a starting and destination city, then he / she can         proceed to the "Simulation" button. After clicking the button the detailed route overview is shown in the log. If the user wants to       change car parameters, then the user can click on the "Car settings" button. If the user wants to calculate another trip, then the user   can click "Reset" button.
  
  
  ![main window](https://i.imgur.com/c0HX3Wr.jpg)
  
### Controls
#### Left mouse click
    Selects starting or destination city. Black circle appears.
    
#### Right mouse click
    Deselects any selected city.
    
#### Ctrl + right mouse click
    Allows you to add a city to your road, which means you’re going through that city. Red circle appears.
    
## Example
  
The user wants to travel from Hamburg to Bratislava. Also he / she wants to travel through Berlin and Prague.

![example simulation](https://i.imgur.com/M7h8NFe.jpg)

# Programmer's documentation
