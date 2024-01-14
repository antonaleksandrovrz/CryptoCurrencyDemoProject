import React, { useState, useEffect } from 'react';
import CurrencieCard from './CurrencieCard';

const CurrenciesList = () => {
    const [cryptocurrenciesList, setCryptocurrenciesList] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);

    const fetchData = async () => {
        try {
            const response = await fetch(`api/Currencies/${currentPage}`);
            if (response.ok) {
                const { data, totalPages: newTotalPages } = await response.json();
                setCryptocurrenciesList(data);
                setTotalPages(newTotalPages);
            } else {
                console.error('Error fetching data:', response.statusText);
            }
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    };

    const handleUpdateClick = async () => {
        try {
            const response = await fetch('api/Currencies', {
                method: 'PUT',
            });
            if (response.ok) {
                fetchData(); // Refresh the data after the update
            } else {
                console.error('Error updating data:', response.statusText);
            }
        } catch (error) {
            console.error('Error updating data:', error);
        }
    };

    const handlePageChange = (newPage) => {
        if (newPage >= 1 && newPage <= totalPages) {
            setCurrentPage(newPage);
        }
    };

    useEffect(() => {
        fetchData();

        // Update every 10 minutes (600,000 milliseconds)
        const updateIntervalId = setInterval(async () => {
            try {
                const response = await fetch('api/Currencies', {
                    method: 'PUT',
                });

                if (response.ok) {
                    fetchData(); // Refresh the data after the update
                } else {
                    console.error('Error updating data:', response.statusText);
                }
            } catch (error) {
                console.error('Error updating data:', error);
            }
        }, 600000);

        // Clean up the interval when the component is unmounted
        return () => clearInterval(updateIntervalId);
    }, [currentPage]);

    return (
        <div className="container">
            <h1 className="my-4">Cryptocurrencies</h1>
            <div className="row">
                {cryptocurrenciesList.map(currency => (
                    <CurrencieCard key={currency.id} currency={currency} />
                ))}
            </div>
            <div>
                <button onClick={() => handlePageChange(currentPage - 1)} disabled={currentPage === 1}>Previous Page</button>
                <span> Page {currentPage} of {totalPages} </span>
                <button onClick={() => handlePageChange(currentPage + 1)} disabled={currentPage === totalPages}>Next Page</button>
            </div>
        </div>
    );
};

export default CurrenciesList;
