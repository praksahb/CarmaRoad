# CarmaRoad
## vertical infinite scroller - 

Prototype - [Webgl Build](https://devlovex.itch.io/carmaggedon)



## Main Classes
### Animal 
-  primarily three types
-  Animal Road crossing behaviour handled by state machines

-  Animal Spawning handled by separate state machine.
- MVC used for creating animal from base Animal class which is stored in a array in animal scriptable object (list)
 
### Player
#### Player Vehicle
- Currently is driving ambulance.
- MVC for creating vehicle from baseVehicle class similarly accessed from vehicle SO

### Design Patterns implemented
- Singletons for specific use cases involving global instances mostly required for action publisher purposes
- MVC for creating animals, vehicle
- State Machines for handling animal states while crossing road, and for spawning and waiting
