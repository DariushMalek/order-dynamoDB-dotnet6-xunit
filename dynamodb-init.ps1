$Response = aws dynamodb --endpoint-url http://localhost:9000 describe-table --table-name ProductType  | ConvertFrom-Json

If ($Response -AND $Response.Table) {
    Write-Host "The ProductType Table Already Exists"    
}
else
{
    Write-Host "Creating Table ProductType..."

    $Result = aws dynamodb --endpoint-url http://localhost:9000 `
        create-table `
        --table-name ProductType `
        --attribute-definitions `
            AttributeName=ProductTypeId,AttributeType=N `
        --key-schema `
            AttributeName=ProductTypeId,KeyType=HASH `
        --provisioned-throughput `
            ReadCapacityUnits=10,WriteCapacityUnits=5
	
	
	aws dynamodb --endpoint-url http://localhost:9000  `
		batch-write-item  `
		--request-items file://scripts/product-types.json   `
		--return-consumed-capacity INDEXES  `
		--return-item-collection-metrics SIZE
	
	
    Write-Host "The ProductType Table has been created."
}

$Response = aws dynamodb --endpoint-url http://localhost:9000 describe-table --table-name Product  | ConvertFrom-Json

If ($Response -AND $Response.Table) {
    Write-Host "The Product Table Already Exists"    
}
else
{
    Write-Host "Creating Table Product..."

    $Result = aws dynamodb --endpoint-url http://localhost:9000 `
        create-table `
        --table-name Product `
        --attribute-definitions `
            AttributeName=ProductTypeId,AttributeType=N `
			AttributeName=ProductId,AttributeType=N `
        --key-schema `
            AttributeName=ProductTypeId,KeyType=HASH `
			AttributeName=ProductId,KeyType=RANGE `
        --provisioned-throughput `
            ReadCapacityUnits=10,WriteCapacityUnits=5
	
	aws dynamodb --endpoint-url http://localhost:9000  `
		batch-write-item  `
		--request-items file://scripts/products.json   `
		--return-consumed-capacity INDEXES  `
		--return-item-collection-metrics SIZE
	
    Write-Host "The Product Table has been created."
}

$Response = aws dynamodb --endpoint-url http://localhost:9000 describe-table --table-name Order  | ConvertFrom-Json

If ($Response -AND $Response.Table) {
    Write-Host "The Order Table Already Exists"    
}
else
{
    Write-Host "Creating Table Order..."

    $Result = aws dynamodb --endpoint-url http://localhost:9000 `
        create-table `
        --table-name Order `
        --attribute-definitions `
            AttributeName=CustomerId,AttributeType=N `
			AttributeName=OrderId,AttributeType=N `
        --key-schema `
            AttributeName=CustomerId,KeyType=HASH `
			AttributeName=OrderId,KeyType=RANGE `
        --provisioned-throughput `
            ReadCapacityUnits=10,WriteCapacityUnits=5
	
	aws dynamodb --endpoint-url http://localhost:9000  `
		batch-write-item  `
		--request-items file://scripts/orders.json   `
		--return-consumed-capacity INDEXES  `
		--return-item-collection-metrics SIZE
	
    Write-Host "The Order Table has been created."
}