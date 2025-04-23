import { Badge } from '@/components/ui/badge';
import { Button } from '@/components/ui/button';
import {
    Card,
    CardContent,
    CardDescription,
    CardFooter,
    CardHeader,
    CardTitle,
} from '@/components/ui/card';
import { Progress } from '@/components/ui/progress';
import { Tabs, TabsContent, TabsList, TabsTrigger } from '@/components/ui/tabs';
import {
    ShoppingBag,
    Package,
    DollarSign,
    Users,
    ArrowUpRight,
    ArrowDownRight,
    ChevronRight,
    Eye,
} from 'lucide-react';

export default function DashboardPage() {
    const topSneakers = [
        {
            id: 1,
            name: 'Air Jordan 1 Retro High',
            price: 185,
            stock: 23,
            sold: 156,
            image: '/placeholder.svg?height=70&width=70',
        },
        {
            id: 2,
            name: 'Nike Dunk Low',
            price: 110,
            stock: 42,
            sold: 128,
            image: '/placeholder.svg?height=70&width=70',
        },
        {
            id: 3,
            name: 'Adidas Yeezy Boost 350',
            price: 230,
            stock: 8,
            sold: 97,
            image: '/placeholder.svg?height=70&width=70',
        },
        {
            id: 4,
            name: 'New Balance 550',
            price: 120,
            stock: 31,
            sold: 85,
            image: '/placeholder.svg?height=70&width=70',
        },
    ];

    const recentOrders = [
        {
            id: '#ORD-7245',
            customer: 'Alex Johnson',
            status: 'Shipped',
            date: 'Apr 21, 2025',
            total: '$385.00',
        },
        {
            id: '#ORD-7244',
            customer: 'Sarah Miller',
            status: 'Processing',
            date: 'Apr 20, 2025',
            total: '$230.00',
        },
        {
            id: '#ORD-7243',
            customer: 'Michael Chen',
            status: 'Delivered',
            date: 'Apr 19, 2025',
            total: '$560.00',
        },
        {
            id: '#ORD-7242',
            customer: 'Emma Wilson',
            status: 'Pending',
            date: 'Apr 19, 2025',
            total: '$120.00',
        },
        {
            id: '#ORD-7241',
            customer: 'James Brown',
            status: 'Delivered',
            date: 'Apr 18, 2025',
            total: '$295.00',
        },
    ];

    const getStatusColor = (status: string) => {
        switch (status) {
            case 'Delivered':
                return 'bg-green-100 text-green-800 dark:bg-green-900 dark:text-green-300';
            case 'Shipped':
                return 'bg-blue-100 text-blue-800 dark:bg-blue-900 dark:text-blue-300';
            case 'Processing':
                return 'bg-yellow-100 text-yellow-800 dark:bg-yellow-900 dark:text-yellow-300';
            case 'Pending':
                return 'bg-gray-100 text-gray-800 dark:bg-gray-800 dark:text-gray-300';
            default:
                return 'bg-gray-100 text-gray-800 dark:bg-gray-800 dark:text-gray-300';
        }
    };

    return (
        <div className="flex flex-col gap-4">
            <div>
                <h1 className="text-2xl font-bold tracking-tight">Dashboard</h1>
                <p className="text-muted-foreground">
                    Welcome back! Here's an overview of your sneaker business.
                </p>
            </div>

            <Tabs defaultValue="overview" className="space-y-4">
                <TabsList>
                    <TabsTrigger value="overview">Overview</TabsTrigger>
                    <TabsTrigger value="inventory">Inventory</TabsTrigger>
                    <TabsTrigger value="sales">Sales</TabsTrigger>
                    <TabsTrigger value="customers">Customers</TabsTrigger>
                </TabsList>

                <TabsContent value="overview" className="space-y-4">
                    <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-4">
                        <Card>
                            <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                                <CardTitle className="text-sm font-medium">Total Revenue</CardTitle>
                                <DollarSign className="h-4 w-4 text-muted-foreground" />
                            </CardHeader>
                            <CardContent>
                                <div className="text-2xl font-bold">$89,432.56</div>
                                <div className="flex items-center pt-1 text-xs text-green-600 dark:text-green-400">
                                    <ArrowUpRight className="mr-1 h-3 w-3" />
                                    <span>18.2% from last month</span>
                                </div>
                            </CardContent>
                        </Card>

                        <Card>
                            <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                                <CardTitle className="text-sm font-medium">Sneakers Sold</CardTitle>
                                <ShoppingBag className="h-4 w-4 text-muted-foreground" />
                            </CardHeader>
                            <CardContent>
                                <div className="text-2xl font-bold">1,245</div>
                                <div className="flex items-center pt-1 text-xs text-green-600 dark:text-green-400">
                                    <ArrowUpRight className="mr-1 h-3 w-3" />
                                    <span>12.5% from last month</span>
                                </div>
                            </CardContent>
                        </Card>

                        <Card>
                            <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                                <CardTitle className="text-sm font-medium">
                                    Inventory Value
                                </CardTitle>
                                <Package className="h-4 w-4 text-muted-foreground" />
                            </CardHeader>
                            <CardContent>
                                <div className="text-2xl font-bold">$124,750</div>
                                <div className="flex items-center pt-1 text-xs text-red-600 dark:text-red-400">
                                    <ArrowDownRight className="mr-1 h-3 w-3" />
                                    <span>4.3% from last month</span>
                                </div>
                            </CardContent>
                        </Card>

                        <Card>
                            <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                                <CardTitle className="text-sm font-medium">New Customers</CardTitle>
                                <Users className="h-4 w-4 text-muted-foreground" />
                            </CardHeader>
                            <CardContent>
                                <div className="text-2xl font-bold">+342</div>
                                <div className="flex items-center pt-1 text-xs text-green-600 dark:text-green-400">
                                    <ArrowUpRight className="mr-1 h-3 w-3" />
                                    <span>24.5% from last month</span>
                                </div>
                            </CardContent>
                        </Card>
                    </div>

                    <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-7">
                        <Card className="col-span-4">
                            <CardHeader>
                                <CardTitle>Recent Orders</CardTitle>
                                <CardDescription>Latest customer purchases</CardDescription>
                            </CardHeader>
                            <CardContent>
                                <div className="space-y-4">
                                    {recentOrders.map((order) => (
                                        <div
                                            key={order.id}
                                            className="flex items-center justify-between"
                                        >
                                            <div className="space-y-1">
                                                <p className="text-sm font-medium leading-none">
                                                    {order.id}
                                                </p>
                                                <p className="text-sm text-muted-foreground">
                                                    {order.customer}
                                                </p>
                                            </div>
                                            <div className="flex items-center gap-4">
                                                <Badge
                                                    className={getStatusColor(order.status)}
                                                    variant="outline"
                                                >
                                                    {order.status}
                                                </Badge>
                                                <div className="text-sm font-medium">
                                                    {order.total}
                                                </div>
                                                <Button variant="ghost" size="icon">
                                                    <Eye className="h-4 w-4" />
                                                    <span className="sr-only">
                                                        View order {order.id}
                                                    </span>
                                                </Button>
                                            </div>
                                        </div>
                                    ))}
                                </div>
                            </CardContent>
                            <CardFooter>
                                <Button variant="ghost" className="w-full" size="sm">
                                    View all orders
                                    <ChevronRight className="ml-1 h-4 w-4" />
                                </Button>
                            </CardFooter>
                        </Card>

                        <Card className="col-span-3">
                            <CardHeader>
                                <CardTitle>Inventory Status</CardTitle>
                                <CardDescription>Stock levels for popular models</CardDescription>
                            </CardHeader>
                            <CardContent>
                                <div className="space-y-4">
                                    {topSneakers.map((sneaker) => {
                                        const stockPercentage =
                                            (sneaker.stock / (sneaker.stock + sneaker.sold)) * 100;
                                        const stockStatus =
                                            stockPercentage < 15
                                                ? 'Low stock'
                                                : stockPercentage < 30
                                                ? 'Medium stock'
                                                : 'Good stock';
                                        const stockColor =
                                            stockPercentage < 15
                                                ? 'text-red-500'
                                                : stockPercentage < 30
                                                ? 'text-yellow-500'
                                                : 'text-green-500';

                                        return (
                                            <div key={sneaker.id} className="space-y-1">
                                                <div className="flex items-center justify-between">
                                                    <p className="text-sm font-medium">
                                                        {sneaker.name}
                                                    </p>
                                                    <p
                                                        className={`text-xs font-medium ${stockColor}`}
                                                    >
                                                        {stockStatus}
                                                    </p>
                                                </div>
                                                <Progress value={stockPercentage} className="h-2" />
                                                <p className="text-xs text-muted-foreground">
                                                    {sneaker.stock} units remaining
                                                </p>
                                            </div>
                                        );
                                    })}
                                </div>
                            </CardContent>
                            <CardFooter>
                                <Button variant="ghost" className="w-full" size="sm">
                                    Manage inventory
                                    <ChevronRight className="ml-1 h-4 w-4" />
                                </Button>
                            </CardFooter>
                        </Card>
                    </div>
                </TabsContent>

                <TabsContent
                    value="inventory"
                    className="flex items-center justify-center text-muted-foreground"
                >
                    Detailed inventory management content would go here
                </TabsContent>

                <TabsContent
                    value="sales"
                    className="flex items-center justify-center text-muted-foreground"
                >
                    Detailed sales reports and analytics would go here
                </TabsContent>

                <TabsContent
                    value="customers"
                    className="flex items-center justify-center text-muted-foreground"
                >
                    Customer management and analytics would go here
                </TabsContent>
            </Tabs>
        </div>
    );
}
