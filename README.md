# FlightPricesMVC
Web application to fetch and compare flight prices


The first input box draws from an API that receives every airport and autofills the values for the user.
The prefilled airport values are compared against a dictionary in the background and translates the Airport values with IATA values which are used for the API.

The rest of the form gets concatenated in the background and paired with the api credentials.

Once the data is retrieved from the api, it gets deserialized into JSON against the data model.


#TODO
Validate input data. 
Enable Dictionary feature to deserialize
Stylize results page to return more info
Enable user accounts to book and save to database
