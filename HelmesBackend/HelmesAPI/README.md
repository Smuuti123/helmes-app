# Backend HelmesApp

This project was generated with [http://localhost:5058/swagger/index.html]

## Description

The backend service allows users to: create and manage shipments, add bags (containing either parcels or letters) to shipments ,add parcels to bags ,finalize shipments, preventing further modifications, view details of shipments, bags, and parcels

## API Endpoints


Shipments
Create Shipment: POST /api/Shipments - Creates a new shipment.
Get Shipment: GET /api/Shipments/{id} - Retrieves details of a specific shipment.
Get All Shipments: GET /api/Shipments - Retrieves a list of all shipments.
Finalize Shipment: POST /api/Shipments/{id}/Finalize - Finalizes a shipment, preventing further modifications.

Bags with Letters
Create Bag with Letters: POST /api/BagWithLettersController/Shipment/{shipmentId} - Creates a new bag containing letters within a specified shipment.
Get Bag with Letters: GET /api/BagWithLettersController/{id} - Retrieves details of a specific bag containing letters.

Bags with Parcels
Create Bag: POST /api/BagWithParcelsController/Shipment/{shipmentId} - Creates a new bag  within a specified shipment.
Add Parcel to Bag: POST /api/BagWithParcelsController/{bagId}/AddParcel - Creates and adds a parcel to a specified bag.
Get Bag with Parcels: GET /api/BagWithParcelsController/{id} - Retrieves details of a specific bag containing parcels.


## Error Handling

The API provides appropriate error messages and HTTP status codes for validation errors, not found errors, and database errors.

## Running the application

Open terminal -> type: cd HelmesBackend -> type: cd HelmesAPI -> type: dotnet build -> type: dotnet run

The API will be available at http://localhost:5058/swagger/index.html.