// CryptoList.js

import React, { useState, useEffect } from 'react';
import CurrencieCardVolume from './CurrencieCardVolume';

const CurrenciesVolumeLeaders = () => {
    const [cryptocurrenciesVolume, setCryptocurrenciesVolume] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            try {
                // Fetch data from the API endpoint
                const response = await fetch('/api/Currencies/volumeLeaders');  // Update the URL
                const data = await response.json();

                setCryptocurrenciesVolume(data);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        fetchData();
    }, []);

    return (
        <div className="container">
            <h1 className="my-4">Volume Leaders</h1>
            <div className="row">
                {cryptocurrenciesVolume.map(currency => (
                    // Use CurrencieCard component to render each cryptocurrency
                    <CurrencieCardVolume key={currency.id} currency={currency} />
                ))}
            </div>
        </div>
    );
};

export default CurrenciesVolumeLeaders;
