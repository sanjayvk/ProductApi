Step 1: Clone the code to your local system
Step 2: Make sure Microsoft SQL Server is available
Step 3: Make sure localhost configuration mentioned in appsettings.json file is available in your microsoft sql or change the configuration according to your Microsoft SQL connection.
Step 4: Make sure all dependencies are installed
Step 5: Click on Run to Run the api in Swagger.

GET
/api/products  -- > Just click on try it out, no parameters required

POST
/api/products -- > Pass values in body as below, no need to pass productid
{  
  "name": "string",
  "description": "string",
  "price": 0,
  "stockAvailable": 20,  
}

GET
/api/products/{id} --> Run the api and pass product id as parameter

PUT
/api/products/{id} --> Run the api and pass product id and below request body

{
  "productId": 100000,
  "name": "string",
  "description": "string",
  "price": 0,
  "stockAvailable": 10  
}

DELETE
/api/products/{id} --> Run the api and pass the product id as parameter to delete

PUT
/api/products/decrement-stock/{id}/{quantity} --> Run the api and pass the product and number of quantity as parameter to decrement stock

PUT
/api/products/add-to-stock/{id}/{quantity} --> Run the api and pass the product and number of quantity as parameter to increment stock
