'use client';

import type React from 'react';

import { useState } from 'react';
import { cn } from '@/lib/utils';
import { Sheet, SheetContent } from '@/components/ui/sheet';
import {
    LayoutDashboard,
    Users,
    ShoppingCart,
    BarChart3,
    FileText,
} from 'lucide-react';
import { Link } from 'react-router-dom';
import { useMediaQuery } from '@/hooks/useMediaQuery';

interface SidebarProps {
    isOpen: boolean;
    onClose: () => void;
}

interface SidebarItem {
    title: string;
    icon: React.ReactNode;
    href: string;
}

export default function Sidebar({ isOpen, onClose }: SidebarProps) {
    const isDesktop = useMediaQuery('(min-width: 1024px)');
    const [activeItem, setActiveItem] = useState('Dashboard');

    const sidebarItems: SidebarItem[] = [
        {
            title: 'Dashboard',
            icon: <LayoutDashboard className="h-5 w-5" />,
            href: '/',
        },
        {
            title: 'Users',
            icon: <Users className="h-5 w-5" />,
            href: '/user',
        },
        {
            title: 'Products',
            icon: <ShoppingCart className="h-5 w-5" />,
            href: '/product',
        },
        {
            title: 'Categories',
            icon: <BarChart3 className="h-5 w-5" />,
            href: '/category',
        },
        {
            title: 'Orders',
            icon: <FileText className="h-5 w-5" />,
            href: '/order',
        }
    ];

    const sidebarContent = (
        <>
            <div className="flex h-14 items-center px-4 border-b">
                <Link
                    to="/"
                    className="flex items-center gap-2 font-semibold"
                    onClick={() => setActiveItem('Dashboard')}
                >
                    <div className="h-7 w-7 rounded-md bg-primary flex items-center justify-center text-primary-foreground">
                        SS
                    </div>
                    <span>Sneaker Shop</span>
                </Link>
            </div>
            <nav className="grid gap-1">
                {sidebarItems.map((item) => (
                    <Link
                        key={item.title}
                        to={item.href}
                        onClick={() => {
                            setActiveItem(item.title);
                            if (!isDesktop) onClose();
                        }}
                        className={cn(
                            'flex items-center gap-3 rounded-md px-3 py-2 text-sm font-medium hover:bg-accent hover:text-accent-foreground',
                            activeItem === item.title
                                ? 'bg-accent text-accent-foreground'
                                : 'transparent'
                        )}
                    >
                        {item.icon}
                        {item.title}
                    </Link>
                ))}
            </nav>
        </>
    );

    if (!isDesktop) {
        return (
            <Sheet open={isOpen} onOpenChange={onClose}>
                <SheetContent side="left" className="p-0 w-64">
                    {sidebarContent}
                </SheetContent>
            </Sheet>
        );
    }

    return (
        <div
            className={cn(
                'w-64 border-r bg-background transition-all duration-300',
                isOpen ? 'block' : 'hidden lg:block'
            )}
        >
            {sidebarContent}
        </div>
    );
}
