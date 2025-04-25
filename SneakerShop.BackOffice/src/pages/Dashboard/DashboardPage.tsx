import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import {
    ShoppingBag,
    Package,
    DollarSign,
    Users,
    ArrowUpRight,
    ArrowDownRight,
} from 'lucide-react';

export default function DashboardPage() {

    return (
        <div className="flex flex-col gap-4">
            <div>
                <h1 className="text-2xl font-bold tracking-tight">Sneaker Dashboard</h1>
                <p className="text-muted-foreground">
                    Welcome back! Here's an overview of your sneaker business.
                </p>
            </div>

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
                        <CardTitle className="text-sm font-medium">Inventory Value</CardTitle>
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
        </div>
    );
}
