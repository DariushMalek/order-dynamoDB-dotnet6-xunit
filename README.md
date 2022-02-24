# TechLand Simple Shop Project
By Dariush Malek : https://www.linkedin.com/in/noorollah-malek/

## Run project with docker

Execute **RunApp.bat** and waiting until all process get completed.

## Project structure
Database: DynamoDb (run locally on port 9000)

Projects:
  * TechLand.Shop.Api
  * TechLand.Shop.BusinessLogic
  * TechLand.Shop.Data
  * TechLand.Shop.Model
  * TechLand.Shop.Test : Includes unit and integration test
  
DynamoDb init : **dynamodb-init.ps1** which create all tables and then insert data from **scripts** folder. 
  * orders.json
  * products.json
  * product-types.json

## Startup project
TechLand.Shop.Api 

Url: http://localhost/swagger/index.html

## APIs
api/orders/{orderId} : Get order

api/orders : Post order
