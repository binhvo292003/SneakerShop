import { Button } from '@/components/ui/button';
import {
    Table,
    TableBody,
    TableCaption,
    TableCell,
    TableFooter,
    TableHead,
    TableHeader,
    TableRow,
} from '@/components/ui/table';
import { Dialog, DialogContent, DialogHeader } from '@/components/ui/dialog';

import { useState } from 'react';
import { Input } from '@/components/ui/input';

const invoices = [
    {
        invoice: 'INV001',
        paymentStatus: 'Paid',
        totalAmount: '$250.00',
        paymentMethod: 'Credit Card',
    },
    {
        invoice: 'INV002',
        paymentStatus: 'Pending',
        totalAmount: '$150.00',
        paymentMethod: 'PayPal',
    },
    {
        invoice: 'INV003',
        paymentStatus: 'Unpaid',
        totalAmount: '$350.00',
        paymentMethod: 'Bank Transfer',
    },
    {
        invoice: 'INV004',
        paymentStatus: 'Paid',
        totalAmount: '$450.00',
        paymentMethod: 'Credit Card',
    },
    {
        invoice: 'INV005',
        paymentStatus: 'Paid',
        totalAmount: '$550.00',
        paymentMethod: 'PayPal',
    },
    {
        invoice: 'INV006',
        paymentStatus: 'Pending',
        totalAmount: '$200.00',
        paymentMethod: 'Bank Transfer',
    },
    {
        invoice: 'INV007',
        paymentStatus: 'Unpaid',
        totalAmount: '$300.00',
        paymentMethod: 'Credit Card',
    },
    {
        invoice: 'INV007',
        paymentStatus: 'Unpaid',
        totalAmount: '$300.00',
        paymentMethod: 'Credit Card',
    },
    {
        invoice: 'INV007',
        paymentStatus: 'Unpaid',
        totalAmount: '$300.00',
        paymentMethod: 'Credit Card',
    },
    {
        invoice: 'INV007',
        paymentStatus: 'Unpaid',
        totalAmount: '$300.00',
        paymentMethod: 'Credit Card',
    },
    {
        invoice: 'INV007',
        paymentStatus: 'Unpaid',
        totalAmount: '$300.00',
        paymentMethod: 'Credit Card',
    },
    {
        invoice: 'INV007',
        paymentStatus: 'Unpaid',
        totalAmount: '$300.00',
        paymentMethod: 'Credit Card',
    },
    {
        invoice: 'INV007',
        paymentStatus: 'Unpaid',
        totalAmount: '$300.00',
        paymentMethod: 'Credit Card',
    },
    {
        invoice: 'INV007',
        paymentStatus: 'Unpaid',
        totalAmount: '$300.00',
        paymentMethod: 'Credit Card',
    },
];

export default function ProductPage() {
    const [categories, setCategories] = useState([
        { id: 1, name: 'Electronics' },
        { id: 2, name: 'Books' },
    ]);

    const [open, setOpen] = useState(false);
    const [editing, setEditing] = useState(null);
    const [name, setName] = useState('');

    const resetForm = () => {
        setName('');
        setEditing(null);
        setOpen(false);
    };

    const columns = [
        { key: 'id', header: 'ID' },
        { key: 'name', header: 'Name' },
    ];

    return (
        <div className="">
            <div className="flex justify-between items-center mb-4">
                <h2 className="text-xl font-bold">Product Categories</h2>
                <Button onClick={() => setOpen(true)}>Add Category</Button>
            </div>{' '}
            <Table>
                <TableCaption>A list of your recent invoices.</TableCaption>
                <TableHeader>
                    <TableRow>
                        <TableHead className="w-[100px]">Invoice</TableHead>
                        <TableHead>Status</TableHead>
                        <TableHead>Method</TableHead>
                        <TableHead className="text-right">Amount</TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>
                    {invoices.map((invoice) => (
                        <TableRow key={invoice.invoice}>
                            <TableCell className="font-medium">{invoice.invoice}</TableCell>
                            <TableCell>{invoice.paymentStatus}</TableCell>
                            <TableCell>{invoice.paymentMethod}</TableCell>
                            <TableCell className="text-right">{invoice.totalAmount}</TableCell>
                        </TableRow>
                    ))}
                </TableBody>
                <TableFooter>
                    <TableRow>
                        <TableCell colSpan={3}>Total</TableCell>
                        <TableCell className="text-right">$2,500.00</TableCell>
                    </TableRow>
                </TableFooter>
            </Table>
            <Dialog open={open} onOpenChange={setOpen}>
                <DialogContent>
                    <DialogHeader>{editing ? 'Edit Category' : 'Add Category'}</DialogHeader>
                    <Input
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                        placeholder="Name"
                        className="my-2"
                    />
                    <Input
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                        placeholder="Description"
                        className="my-2"
                    />
                    <Input
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                        placeholder="Price"
                        className="my-2"
                    />
                    // List categoriese
                </DialogContent>
            </Dialog>
        </div>
    );
}
