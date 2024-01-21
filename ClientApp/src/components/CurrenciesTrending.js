// CryptoList.js

import React, { useState, useEffect } from 'react';
import CurrencieCard from './CurrencieCard';

const CurrenciesTrending = () => {
    const [cryptocurrenciesTrending, setCryptocurrenciesTrending] = useState([]);
    const [errorMessage, setErrorMessage] = useState(null); // Add error message state

    useEffect(() => {
        const fetchData = async () => {
            try {
                var myHeaders = new Headers();
                var token = localStorage.getItem('token');
                myHeaders.append("Authorization", "Bearer " + token);

                var requestOptions = {
                    method: 'GET',
                    headers: myHeaders,
                    redirect: 'follow'
                };

                const response = await fetch("/api/Currencies/trending", requestOptions);

                if (!response.ok) {
                    if (response.status === 401 || response.status === 403) {
                        // Unauthorized error
                        setErrorMessage('User has no rights.');
                    } else {
                        throw new Error(`HTTP error! Status: ${response.status}`);
                    }
                } else {
                    const data = await response.json();
                    setCryptocurrenciesTrending(data);
                    setErrorMessage(null); // Clear error message if fetching is successful
                }
            } catch (error) {
                console.error('Error fetching data:', error);
                setErrorMessage('Error fetching data. Please try again.'); // General error message
            }
        };

        fetchData();
    }, []);

    return (
        <div className="container">
            <h1 className="my-4">Cryptocurrencies</h1>

            {/* Render error message if there is one */}
            {errorMessage && <label style={{ color: 'red' }}>{errorMessage}</label>}

            <div className="row">
                {cryptocurrenciesTrending.map(currency => (
                    // Use CurrencieCard component to render each cryptocurrency
                    <CurrencieCard key={currency.id} currency={currency} />
                ))}
            </div>
        </div>
    );
};

export default CurrenciesTrending;
