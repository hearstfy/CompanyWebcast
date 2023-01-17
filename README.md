# How to run

In root folder run; 

```bash
   docker-compose up -d
```

# How to Use
You can also check http://localhost:5001/swagger/index.html for SwaggerDoc

 - Send HTTP Post request to localhost:5001/api/weatherforecast to add a new forecast. Data model below;
    
  ```json

    {
        "date": "DateTime",
        "weatherForecastsHourly": [
            {
                "startHour": "int",
                "endHour": "int",
                "temperatureC": "double"
            }
        ]
    }
 ```

 - Send HTTP Put request to localhost:5001/api/weatherforecast/:id to add or update hourly forecasts within a forecast.
 Data model below;

 ```json
     {
        [
            {
                "startHour": "int",
                "endHour": "int",
                "temperatureC": "double"
            }
        ]
    }
 ```

 - Send HTTP Get request to localhost:5001/api/weatherforecast/weekly to get weekly forecast.
 No request body is needed.