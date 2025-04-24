import { Button } from '@/components/ui/button';
import { Dialog, DialogContent, DialogHeader } from '@/components/ui/dialog';
import {
    Table,
    TableBody,
    TableCaption,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from '@/components/ui/table';
import { useState } from 'react';

const dummyData = [
    {
        id: 1001,
        email: 'alice@example.com',
        total: 129.99,
        status: 'completed',
    },
    {
        id: 1002,
        email: 'bob@example.com',
        total: 45.5,
        status: 'pending',
    },
    {
        id: 1003,
        email: 'charlie@example.com',
        total: 300.0,
        status: 'cancelled',
    },
    {
        id: 1004,
        email: 'dave@example.com',
        total: 75.25,
        status: 'completed',
    },
    {
        id: 1005,
        email: 'eve@example.com',
        total: 99.99,
        status: 'processing',
    },
    {
        id: 1006,
        email: 'frank@example.com',
        total: 249.0,
        status: 'completed',
    },
    {
        id: 1007,
        email: 'grace@example.com',
        total: 130.75,
        status: 'refunded',
    },
    {
        id: 1008,
        email: 'hank@example.com',
        total: 50.0,
        status: 'pending',
    },
    {
        id: 1009,
        email: 'irene@example.com',
        total: 89.0,
        status: 'completed',
    },
    {
        id: 1010,
        email: 'jack@example.com',
        total: 180.45,
        status: 'processing',
    },
    {
        id: 1011,
        email: 'kate@example.com',
        total: 60.99,
        status: 'completed',
    },
    {
        id: 1012,
        email: 'leo@example.com',
        total: 112.3,
        status: 'cancelled',
    },
    {
        id: 1013,
        email: 'mia@example.com',
        total: 210.0,
        status: 'completed',
    },
    {
        id: 1014,
        email: 'nick@example.com',
        total: 33.49,
        status: 'pending',
    },
    {
        id: 1015,
        email: 'olivia@example.com',
        total: 56.0,
        status: 'refunded',
    },
    {
        id: 1016,
        email: 'paul@example.com',
        total: 95.9,
        status: 'completed',
    },
    {
        id: 1017,
        email: 'quinn@example.com',
        total: 120.0,
        status: 'processing',
    },
    {
        id: 1018,
        email: 'rachel@example.com',
        total: 78.8,
        status: 'completed',
    },
    {
        id: 1019,
        email: 'steve@example.com',
        total: 142.6,
        status: 'completed',
    },
    {
        id: 1020,
        email: 'tina@example.com',
        total: 67.4,
        status: 'cancelled',
    },
];

interface OrderItem {
    id: number;
    email: string;
    total: number;
    status: string;
}

export default function OrderPage() {
    const [open, setOpen] = useState(false);
    const [order, setOrder] = useState<OrderItem | null>(null);

    const handleViewDetail = (order: OrderItem) => {
        setOrder(order);
        setOpen(true);
    };

    return (
        <div className="">
            <div className="flex justify-between items-center mb-4">
                <h2 className="text-xl font-bold">Product Categories</h2>
            </div>{' '}
            <Table>
                <TableCaption>A list of your recent invoices.</TableCaption>
                <TableHeader>
                    <TableRow>
                        <TableHead className="w-[80px]">Id</TableHead>
                        <TableHead>Orderer</TableHead>
                        <TableHead>Status</TableHead>
                        <TableHead>Total</TableHead>
                        <TableHead className='text-right'>Detail</TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>
                    {dummyData.map((data) => (
                        <TableRow key={data.id}>
                            <TableCell className="font-medium">{data.id}</TableCell>
                            <TableCell>{data.email}</TableCell>
                            <TableCell>{data.status}</TableCell>
                            <TableCell>{data.total}</TableCell>
                            <TableCell>
                                <div className="flex justify-center">
                                    <Button
                                        variant="ghost"
                                        size="sm"
                                        className="h-8 w-8 p-0"
                                        onClick={() => handleViewDetail(data)}
                                    >
                                        <div className="p-2 rounded-md bg-primary flex items-center justify-center text-primary-foreground">
                                            View detail
                                        </div>
                                    </Button>
                                </div>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
            <Dialog open={open} onOpenChange={setOpen}>
                <DialogContent className="max-w-3xl">
                    <DialogHeader className="text-xl font-bold">Order Details</DialogHeader>
                    <div className="grid gap-4">
                        <div className="grid grid-cols-2 gap-4">
                            <div>
                                <h3 className="font-semibold mb-2">Order Information</h3>
                                <p>
                                    <span className="font-medium">Order ID:</span> {order?.id}
                                </p>
                                <p>
                                    <span className="font-medium">Customer:</span> {}
                                </p>
                                <p>
                                    <span className="font-medium">Status:</span>
                                    {order?.status}
                                </p>
                                <p>
                                    <span className="font-medium">Total:</span> $
                                    {order?.total.toFixed(2)}
                                </p>
                            </div>
                            <div>
                                <h3 className="font-semibold mb-2">Shipping Information</h3>
                                <p>123 Main St.</p>
                                <p>Apt 4B</p>
                                <p>New York, NY 10001</p>
                            </div>
                        </div>
                        <div className="mt-4">
                            <h3 className="font-semibold mb-2">Order Items</h3>
                            <Table>
                                <TableHeader>
                                    <TableRow>
                                        <TableHead>Product</TableHead>
                                        <TableHead>Quantity</TableHead>
                                        <TableHead>Price</TableHead>
                                        <TableHead>Subtotal</TableHead>
                                    </TableRow>
                                </TableHeader>
                                <TableBody>
                                    <TableRow>
                                        <TableCell>Sample Product</TableCell>
                                        <TableCell>1</TableCell>
                                        <TableCell>$99.99</TableCell>
                                        <TableCell>$99.99</TableCell>
                                    </TableRow>
                                </TableBody>
                            </Table>
                        </div>
                    </div>
                    <div className="flex justify-end gap-2 mt-4">
                        <Button variant="outline" onClick={() => setOpen(false)}>
                            Close
                        </Button>
                        <Button>Update Status</Button>
                    </div>
                </DialogContent>
            </Dialog>
        </div>
    );
}
