# Core.Eshop

# Core.Eshop

REST API service that provides all available products of an e-shop and enables products description update.

Prerequisites to run the project 
Visual Studio with running .net5.0. it is also possible to JetBrains Rider. Any other IDE software were not tested.
MSSQL to run local database. To set up the database use the eshop.sql and update connection strings in appsetting.json to reflect your local setting.

Unit tests
unit test should reflect all endpoints
InMemory is used as the mock method if you wish to use local database please comment out insides of CustomWebApplicationFactory.ConfigureWebHost() mehod.

Versioning
- used Uri versioning, supported Versions V1 && V2
