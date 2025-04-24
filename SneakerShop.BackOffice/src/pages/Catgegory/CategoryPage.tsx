import { Button } from '@/components/ui/button';
import { Dialog, DialogContent, DialogHeader } from '@/components/ui/dialog';
import { Input } from '@/components/ui/input';
import {
    Table,
    TableBody,
    TableCaption,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from '@/components/ui/table';
import { Edit, Trash2 } from 'lucide-react';
import { useState } from 'react';

const dummyData = [
    {
        id: 1,
        name: 'Basketball',
    },
    {
        id: 2,
        name: 'Running',
    },
    {
        id: 3,
        name: 'Lifestyle',
    },
    {
        id: 4,
        name: 'Skateboarding',
    },
    {
        id: 5,
        name: 'Tennis',
    },
    {
        id: 6,
        name: 'Training',
    },
    {
        id: 7,
        name: 'Limited Edition',
    },
];

interface CategoryItem {
    id: number;
    name: string;
}

export default function CategoryPage() {
    const [open, setOpen] = useState(false);
    const [editing, setEditing] = useState<CategoryItem | null>(null);
    const [name, setName] = useState('');

    const handleEdit = (category: { id: number; name: string }) => {
        setEditing(category);
        setName(category.name);
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
                <h2 className="text-xl font-bold">Product Categories</h2>
                <Button onClick={handleNew}>Add Category</Button>
            </div>{' '}
            <Table>
                <TableCaption>A list of your recent invoices.</TableCaption>
                <TableHeader>
                    <TableRow>
                        <TableHead className="w-[100px]">Id</TableHead>
                        <TableHead>Category Name</TableHead>
                        <TableHead className="w-[100px] text-right">Action</TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>
                    {dummyData.map((data) => (
                        <TableRow key={data.id}>
                            <TableCell className="font-medium">{data.id}</TableCell>
                            <TableCell>{data.name}</TableCell>
                            <TableCell>
                                <div className="flex justify-end gap-2">
                                    <Button
                                        variant="ghost"
                                        size="sm"
                                        className="h-8 w-8 p-0"
                                        onClick={() => handleEdit(data)}
                                    >
                                        <Edit className="h-4 w-4" />
                                        <span className="sr-only">Edit category</span>
                                    </Button>
                                    <Button
                                        variant="ghost"
                                        size="sm"
                                        className="h-8 w-8 p-0 text-red-500 hover:text-red-600 hover:bg-red-50"
                                        onClick={() => console.log('Delete category:', data.id)}
                                    >
                                        <Trash2 className="h-4 w-4" />
                                        <span className="sr-only">Delete category</span>
                                    </Button>
                                </div>
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
                        placeholder="Category Name"
                        className="my-2"
                    />
                    <Button onClick={handleSubmit} className="mt-4">
                        {editing ? 'Save Changes' : 'Add Category'}
                    </Button>
                </DialogContent>
            </Dialog>
        </div>
    );
}
