import { Navigate, createBrowserRouter } from 'react-router-dom';
import App from '../layout/App';
import DashboardPage from '../pages/Dashboard/DashboardPage';
import ProductPage from '@/pages/Product/ProductPage';

export const router = createBrowserRouter([
    {
        path: '/',
        element: <App />,
        children: [
            { path: '', element: <ProductPage /> },
            { path: 'product', element: <DashboardPage /> },
            { path: 'not-define', element: <ProductPage /> },
            { path: '*', element: <Navigate replace to="/not-define" /> },
        ],
    },
]);
