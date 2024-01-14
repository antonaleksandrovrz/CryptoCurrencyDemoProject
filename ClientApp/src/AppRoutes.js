
import CryptoList from "./components/CryptoList";
import CurrenciesList from "./components/CurrenciesList";
import CurrenciesTrending from "./components/CurrenciesTrending";
import CurrenciesVolumeLeaders from "./components/CurrenciesVolumeLeaders";
import { Home } from "./components/Home";

const AppRoutes = [
    {
        index: true,
        element: <Home />
    },
    {
        path: '/cryptocurrencies',
        element: <CryptoList />
    }
    ,
    {
        path: '/currencies',
        element: <CurrenciesList />
    }
    ,
    {
        path: '/currencies/trending',
        element: <CurrenciesTrending />
    }
    ,
    {
        path: '/currencies/volumeLeaders',
        element: <CurrenciesVolumeLeaders />
    }
];

export default AppRoutes;
