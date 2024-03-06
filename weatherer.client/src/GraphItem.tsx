import React from 'react';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer } from 'recharts';
import CustomTooltip from './CustomTooltip';

export interface WeatherDataItem {
    country: string;
    city: string;
    value: number;
    lastUpdate: string;
}

interface GraphItemProps {
    title: string;
    data: WeatherDataItem[];
    dataKey: string;
    xAxisKey: string;
    color: string;
    onClick: (city: string) => void;
}

const GraphItem: React.FC<GraphItemProps> = ({ title, data, dataKey, xAxisKey, color, onClick }) => {
    const handleClick = (data: any) => {
        if (data && data.activePayload[0].payload.city) {
            onClick(data.activePayload[0].payload.city);
        }
    };

    return (
        <div style={{ cursor: 'pointer', width: '100%', height: 450, padding: '64px 0' }}>
            <h2>{title}</h2>
            <ResponsiveContainer width="100%" height="100%">
                <LineChart
                    data={data}
                    margin={{ top: 5, right: 30, left: 20, bottom: 5 }}
                    onClick={handleClick}
                >
                    <CartesianGrid strokeDasharray="3 3" />
                    <XAxis dataKey={xAxisKey} />
                    <YAxis />
                    <Tooltip content={<CustomTooltip />} />
                    <Line type="monotone" dataKey={dataKey} stroke={color} activeDot={{ r: 8 }} />
                </LineChart>
            </ResponsiveContainer>
        </div>
    );
};

export default GraphItem;
