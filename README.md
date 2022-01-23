<img src="default_albelli.nl.jpg" width="200">
# .NET Software Engineer Technical Assignment

## Run project with docker

Execute **RunApp.bat** and waiting until all process get completed.

## Project structure
Database: DynamoDb (run locally on port 9000)

Projects:
  * Albelli.Shop.Api
  * Albelli.Shop.BusinessLogic
  * Albelli.Shop.Data
  * Albelli.Shop.Model
  * Albelli.Shop.Test : Includes unit and integration test
  
DynamoDb init : **dynamodb-init.ps1** which create all tables and then insert data from **scripts** folder. 
  * orders.json
  * products.json
  * product-types.json

## Startup project
Albelli.Shop.Api 

Url: http://localhost/swagger/index.html

## APIs
api/orders/{orderId} : Get order

api/orders : Post order
