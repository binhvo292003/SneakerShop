import { useState } from 'react';
import Header from './Header';
import { Outlet } from 'react-router-dom';
import Sidebar from './Sidebar';
import { useMediaQuery } from '@/hooks/useMediaQuery';

function App() {
    const [sidebarOpen, setSidebarOpen] = useState(false);
    const isDesktop = useMediaQuery('(min-width: 1024px)');

    return (
        <div className="flex h-screen bg-gray-100 dark:bg-gray-950">
            <Sidebar isOpen={isDesktop || sidebarOpen} onClose={() => setSidebarOpen(false)} />

            <div className="flex flex-col flex-1 overflow-hidden">
                <Header onMenuClick={() => setSidebarOpen(!sidebarOpen)} />

                <main className="flex-1 overflow-y-auto p-4 md:p-6">
                    <Outlet />
                </main>
            </div>
        </div>
    );
}

export default App;
