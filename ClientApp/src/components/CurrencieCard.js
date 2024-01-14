// CurrencieCard.js
import React from 'react';

const CurrencieCard = ({ currency }) => {
    const roundedChange = Math.round(currency.changePercent24Hr * 1000) / 1000;
    const roundedPrice = Math.round(currency.priceUsd * 1000) / 1000;
    const formattedPrice = roundedPrice.toLocaleString(undefined, {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2,
    });

    const arrowColorClass = currency.changePercent24Hr > 0 ? 'text-success' : 'text-danger';
    const arrowIcon = currency.changePercent24Hr > 0 ? '▲' : '▼';

    return (
        <div className="col-md-4 mb-4">
            <div className="card">
                <div className="card-body">
                    <h5 className="card-title">{currency.name}</h5>
                    <p className="card-text">Symbol: {currency.symbol}</p>
                    <p className="card-text">Price: ${formattedPrice}</p>
                    <p className="card-text">
                        changePercent24Hr: <span className={arrowColorClass}>{roundedChange} {arrowIcon}</span> 
                    </p>
                </div>
            </div>
        </div>
    );
};

export default CurrencieCard;
