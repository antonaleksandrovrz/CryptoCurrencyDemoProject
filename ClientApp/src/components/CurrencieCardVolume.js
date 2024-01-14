// CurrencieCardVolume.js
import React from 'react';

const CurrencieCardVolume = ({ currency }) => {
    const roundedChange = Math.round(currency.volumeUsd24Hr * 1000) / 1000;
    const roundedPrice = Math.round(currency.priceUsd * 1000) / 1000;
    const formattedPrice = roundedPrice.toLocaleString(undefined, {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2,
    });

    const arrowColorClass = currency.volumeusd24hr > 0 ? 'text-success' : 'text-danger';
    const arrowIcon = currency.volumeusd24hr > 0 ? '▲' : '▼';

    return (
        <div className="col-md-4 mb-4">
            <div className="card">
                <div className="card-body">
                    <h5 className="card-title">{currency.name}</h5>
                    <p className="card-text">Symbol: {currency.symbol}</p>
                    <p className="card-text">Price: ${formattedPrice}</p>
                    <p className="card-text">Max supply: {currency.maxSupply}</p>
                    <p className="card-text">
                        volumeusd24hr: <span className={arrowColorClass}>{roundedChange} {arrowIcon}</span> 
                    </p>
                </div>
            </div>
        </div>
    );
};

export default CurrencieCardVolume;
