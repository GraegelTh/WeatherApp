#  WeatherApp

A simple WPF desktop app that fetches current weather data from the [OpenWeather API](https://openweathermap.org/api).  
Created as a beginner project to practice:
- API calls in C#
- JSON deserialization with Newtonsoft.Json
- WPF UI design with XAML
- Handling secrets via environment variables

---

##  Features
- Search weather by **city name** or **German postal code**
- Shows:
  - City name
  - Current temperature
  - Feels like temperature
  - Min/Max temperatures
  - Humidity
  - Weather description & icon
- Background changes depending on weather (sunny, cloudy, rainy, snowy)

---

## Getting Started

### 1. Clone the repo
```bash
git clone https://github.com/GraegelTh/WeatherApp.git
cd WeatherApp
```

### 2. Set up your API key
Register for a free API key at [openweathermap.org](https://openweathermap.org).  
Then set it as an environment variable:

**Windows PowerShell**
```powershell
setx WeatherApp_ApiKey "YOUR_API_KEY_HERE"
```

Restart Visual Studio after setting the variable.

### 3. Run the app
Open the solution in Visual Studio, build, and run.  
Enter a city (e.g. `Augsburg`) or German postal code (`86150`) and press **Enter** or click **Search**.

---

##  Screenshots



---

##  Tech
- .NET 8 / WPF
- C#
- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/)
- OpenWeather API

---

##  Notes
- Currently supports German postal codes for ZIP search. Could be extended with OpenWeather's Geo API for other countries.
- All images and icons are AI generated for this project.

---

##  Learning goals
This project was created mainly to:
- Learn how to call and parse JSON APIs in C#
- Explore WPF layout and styles
- Work with environment variables instead of hardcoding API keys
