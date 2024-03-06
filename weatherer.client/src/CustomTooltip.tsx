import { TooltipProps } from 'recharts';

const CustomTooltip = ({
    active,
    payload
}: TooltipProps<number, string>) => {
    if (active && payload && payload.length) {
        const data = payload[0].payload;
        return (
            <div className="custom-tooltip" style={{ backgroundColor: '#fff', padding: '10px', border: '1px solid #ccc' }}>
                <p>Country: {data.country}</p>
                <p>City: {data.city}</p>
                <p>Temperature: {data.value}°C</p>
                <p>Last Update: {new Date(data.lastUpdate).toLocaleString()}</p>
            </div>
        );
    }

    return null;
};

export default CustomTooltip;