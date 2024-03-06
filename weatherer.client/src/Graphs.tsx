import React, { useState } from 'react';
import GraphItem, { WeatherDataItem } from './GraphItem';

interface GraphsProps {
    tempData: WeatherDataItem[];
    windData: WeatherDataItem[];
}

const Graphs: React.FC<GraphsProps> = ({ tempData, windData }) => {
    const [tempGraphData, setTempGraphData] = useState(tempData);
    const [windGraphData, setWindGraphData] = useState(windData);
    const [xAxisKey, setXAxisKey] = useState('city');

    const handleTempClick = async (city: string) => {
        const response = await fetch(`api/weather/gettemptrend/${city}`);
        const data = await response.json();
        setTempGraphData(data);
        setXAxisKey('lastUpdate');
    };

    const handleWindClick = async (city: string) => {
        const response = await fetch(`api/weather/getwindtrend/${city}`);
        const data = await response.json();
        setWindGraphData(data);
        setXAxisKey('lastUpdate');
    };

    return (
        <div>
            <GraphItem
                title="Minimum temperature"
                data={tempGraphData}
                dataKey="value"
                xAxisKey={xAxisKey}
                color="#8884d8"
                onClick={handleTempClick}
            />
            <GraphItem
                title="Highest wind speed"
                data={windGraphData}
                dataKey="value"
                xAxisKey={xAxisKey}
                color="#82ca9d"
                onClick={handleWindClick}
            />
        </div>
    );
};

export default Graphs;