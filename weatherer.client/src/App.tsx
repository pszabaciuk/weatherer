import { useEffect, useState } from 'react';
import './App.css';
import Graphs from './Graphs';
import { WeatherDataItem } from './GraphItem';

interface WeatherData {
    temp: WeatherDataItem[],
    wind: WeatherDataItem[]
}

function App() {
    const [weatherData, setForecasts] = useState<WeatherData>();

    useEffect(() => {
        populateWeatherData();
    }, []);

    const contents = weatherData === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <Graphs
            tempData={weatherData.temp}
            windData={weatherData.wind}
        />

    return (
        <div>
            <h1 id="tabelLabel">Weather forecast</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>
    );

    async function populateWeatherData() {
        const response = await fetch('api/weather/getweatherdata');
        const data = await response.json();
        setForecasts(data);
    }
}

export default App;