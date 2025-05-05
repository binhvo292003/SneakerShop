import { Navigate, createBrowserRouter } from 'react-router-dom';
import App from '../layout/App';
import DashboardPage from '../pages/Dashboard/DashboardPage';
import ProductPage from '@/pages/Product/ProductPage';
import CategoryPage from '@/pages/Catgegory/CategoryPage';
import OrderPage from '@/pages/Order/OrderPage';
import UserPage from '@/pages/User/UserPage';
import ErrorPage from '@/pages/Error/ErrorPage';

export const router = createBrowserRouter([
    {
        path: '/',
        element: <App />,
        children: [
            { path: '', element: <ProductPage /> },
            { path: 'product', element: <ProductPage /> },
            { path: 'category', element: <CategoryPage /> },
            { path: 'order', element: <OrderPage /> },
            { path: 'user', element: <UserPage /> },
        ],
    },
    {
        path: '/not-found',
        element: <ErrorPage />,
    },
    {
        path: '*',
        element: <Navigate replace to="/not-found" />,
    },
]);
