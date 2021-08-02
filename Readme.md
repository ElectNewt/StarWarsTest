# Star wars  


The application is built using `Net Core 3.1`.


## Usage

update 1

update 2

update 3


update 4
To run the application:
 - Move into the folder `$SwTest\src\StarWars` 
 - Execute the command `dotnet run`

Master Yoda will appear asking you what is the distance you want to travel.
 - Then you have to indicate a distance in MGLT (long).
 - The Application will fecth https://swapi.dev/ to get the `starships` information.
 - It will present on the screen the information with the name of the `starship` and the number of stops that you will need to do to refill your consumables.

## Test

The tests are located into `$SwTest\test\{folder}`

To run the unit tests
 - Move into the folder `SwTest\test\StarWars.UnitTest`
 - Execute the command `dotnet test`

To run the integration test
 - Move into the folder `SwTest\test\StarWars.IntegrationTest`
 - Execute the command `dotnet test`
 - This test realize a call to https://swapi.dev/ in order to obtain the information.

### Disclaimer
1. When queriying the API I only generated the endpoint that is needed for the test.
  - The same idea applies to the Dto received from the API it is built to contain the necessary information to achieve the objetive. 

 2. The main service is build using "RailWay Oriented programming" 
  - The reason why I used it is because in my opinion the code looks much cleaner and it is easier to follow.
  - The code to implement this pattern is included in `Shared.ROP` and it is an small part of a bigger library on my own GitHub account https://github.com/ElectNewt/EjemploRop
  - The original idea is from Scott Wlaschin and it can be found in this link: https://fsharpforfunandprofit.com/rop

 3. I genuinely think that dependency injection is one of the greatest patterns in terms of  facillity to work, even for console applications set it up is very simple, so I implemented it for this task.
