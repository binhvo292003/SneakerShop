import { Button } from '@/components/ui/button';
import {
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from '@/components/ui/table';
import { Dialog, DialogContent, DialogHeader } from '@/components/ui/dialog';

import { useState } from 'react';
import { Input } from '@/components/ui/input';
import { Edit, Trash2 } from 'lucide-react';
import { Label } from '@/components/ui/label';
import { Checkbox } from '@/components/ui/checkbox';

const dummyData = [
    {
        id: 1,
        name: 'Sneaker Nike Air',
        price: 100.0,
    },
    {
        id: 2,
        name: 'Sneaker Adidas',
        price: 120.0,
    },
    {
        id: 3,
        name: 'Sneaker Puma',
        price: 80.0,
    },
    {
        id: 4,
        name: 'Sneaker Reebok',
        price: 90.0,
    },
    {
        id: 5,
        name: 'Sneaker New Balance',
        price: 110.0,
    },
];

const categories = [
    { id: 1, name: 'Basketball', products: 42, active: true },
    { id: 2, name: 'Running', products: 38, active: true },
    { id: 3, name: 'Lifestyle', products: 65, active: true },
    { id: 4, name: 'Skateboarding', products: 24, active: true },
    { id: 5, name: 'Tennis', products: 18, active: true },
    { id: 6, name: 'Training', products: 31, active: true },
    { id: 7, name: 'Limited Edition', products: 12, active: false },
];

const productVariants = [
    { id: 1, size: 'S', stock: 10 },
    { id: 1, size: 'M', stock: 10 },
    { id: 1, size: 'L', stock: 10 },
];

interface ProductItem {
    id: number;
    name: string;
    price: number;
}

export default function ProductPage() {
    const [open, setOpen] = useState(false);
    const [editing, setEditing] = useState<ProductItem | null>(null);
    const [name, setName] = useState('');

    const handleEdit = (product: ProductItem) => {
        setEditing(product);
        setName(product.name);
        setOpen(true);
    };

    const handleNew = () => {
        setEditing(null);
        setName('');
        setOpen(true);
    };

    const handleSubmit = () => {
        if (editing) {
            console.log('Editing category:', { id: editing.id, name });
        } else {
            console.log('Adding new category:', { name });
        }
        setOpen(false);
    };
    return (
        <div className="">
            <div className="flex justify-between items-center mb-4">
                <h2 className="text-xl font-bold">Product</h2>
                <Button onClick={handleNew}>Add Product</Button>
            </div>
            <Table>
                <TableHeader>
                    <TableRow>
                        <TableHead className="w-[100px]">Id</TableHead>
                        <TableHead>Name</TableHead>
                        <TableHead>Price</TableHead>
                        <TableHead>Details</TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>
                    {dummyData.map((data) => (
                        <TableRow key={data.id}>
                            <TableCell className="font-medium">{data.id}</TableCell>
                            <TableCell>{data.name}</TableCell>
                            <TableCell>{data.price}</TableCell>
                            <TableCell className="text-right">
                                <TableCell>
                                    <div className="flex justify-end gap-2">
                                        <Button
                                            variant="ghost"
                                            size="sm"
                                            className="h-8 w-8 p-0"
                                            onClick={() => handleEdit(data)}
                                        >
                                            <Edit className="h-4 w-4" />
                                            <span className="sr-only">Edit Product</span>
                                        </Button>
                                        <Button
                                            variant="ghost"
                                            size="sm"
                                            className="h-8 w-8 p-0 text-red-500 hover:text-red-600 hover:bg-red-50"
                                            onClick={() => console.log('Delete category:', data.id)}
                                        >
                                            <Trash2 className="h-4 w-4" />
                                            <span className="sr-only">Delete Product</span>
                                        </Button>
                                    </div>
                                </TableCell>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
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
                    <div className="grid gap-2">
                        <Label className="required">Categories (Select at least one)</Label>
                        <div className="grid grid-cols-2 gap-2">
                            {categories
                                .filter((category) => category.active)
                                .map((category) => (
                                    <div key={category.id} className="flex items-center space-x-2">
                                        <Checkbox id={`edit-category-${category.id}`} />
                                        <Label
                                            htmlFor={`edit-category-${category.id}`}
                                            className="text-sm font-normal"
                                        >
                                            {category.name}
                                        </Label>
                                    </div>
                                ))}
                        </div>
                    </div>
                    <div className="grid gap-2 mt-4">
                        <Label className="required">Variant</Label>
                        <div className="grid grid-cols-2 gap-2">
                            {productVariants.map((variant) => (
                                <div key={variant.id} className="flex items-center space-x-2">
                                    <Checkbox id={`edit-variant-${variant.id}`} />
                                    <Label
                                        htmlFor={`edit-variant-${variant.id}`}
                                        className="text-sm font-normal"
                                    >
                                        {`Size: ${variant.size}, Stock: ${variant.stock}`}
                                    </Label>
                                </div>
                            ))}
                        </div>
                    </div>

                    <Button size="sm" onClick={() => {}}>
                            Add Variant
                    </Button>

                    <Button onClick={handleSubmit}>
                        {editing ? 'Save Changes' : 'Add Category'}
                    </Button>
                </DialogContent>
            </Dialog>
        </div>
    );
}
