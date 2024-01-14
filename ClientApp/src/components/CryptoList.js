// CryptoList.js

import React, { useState, useEffect } from 'react';
import CurrencieCard from './CurrencieCard';

const CryptoList = () => {
    const [cryptocurrencies, setCryptocurrencies] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            try {
                // Fetch data from the API endpoint
                const response = await fetch('/api/Currencies/externalApi');  // Update the URL
                const data = await response.json();

                setCryptocurrencies(data);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        fetchData();
    }, []);

    return (
        <div className="container">
            <h1 className="my-4">Cryptocurrencies</h1>
            <div className="row">
                {cryptocurrencies.map(currency => (
                    // Use CurrencieCard component to render each cryptocurrency
                    <CurrencieCard key={currency.id} currency={currency} />
                ))}
            </div>
        </div>
    );
};

export default CryptoList;
