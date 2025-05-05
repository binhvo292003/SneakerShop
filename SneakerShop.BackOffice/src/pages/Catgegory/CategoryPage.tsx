import { Button } from '@/components/ui/button';
import { Dialog, DialogContent, DialogHeader } from '@/components/ui/dialog';
import { Input } from '@/components/ui/input';
import {
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from '@/components/ui/table';
import axios from 'axios';
import { Edit, Trash2 } from 'lucide-react';
import { useEffect, useState } from 'react';

interface CategoryItem {
    id: number;
    name: string;
}

export default function CategoryPage() {
    const [categories, setCategories] = useState<CategoryItem[]>([]);
    const [open, setOpen] = useState(false);
    const [editing, setEditing] = useState<CategoryItem | null>(null);
    const [name, setName] = useState('');
    const baseUrl = 'http://localhost:8000/api/category';

    const fetchCategories = async () => {
        const source = axios.CancelToken.source();
        try {
            const response = await axios.get(baseUrl, {
                cancelToken: source.token,
            });
            setCategories(response.data);
        } catch (err) {
            if (axios.isCancel(err)) {
                console.log('Request canceled', err.message);
            }
        }
    };

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

    const handleSubmit = async () => {
        const source = axios.CancelToken.source();

        try {
            if (editing) {
                console.log('Editing category:', { id: editing.id, name });
                await axios.put(
                    `${baseUrl}/${editing.id}`,
                    { id: editing.id, name },
                    {
                        cancelToken: source.token,
                    }
                );
            } else {
                console.log('Adding new category:', { name });
                await axios.post(
                    baseUrl,
                    { name },
                    {
                        cancelToken: source.token,
                    }
                );
            }
            // Refresh the category list
            await fetchCategories();
        } catch (err) {
            if (axios.isCancel(err)) {
                console.log('Request canceled', err.message);
            }
        } finally {
            setOpen(false);
        }
    };

    useEffect(() => {
        fetchCategories();
    }, []);

    return (
        <div className="">
            <div className="flex justify-between items-center mb-4">
                <h2 className="text-xl font-bold">Categories</h2>
                <Button onClick={handleNew}>Add Category</Button>
            </div>{' '}
            <Table className="caret-transparent">
                <TableHeader>
                    <TableRow>
                        <TableHead className="w-[100px]">Id</TableHead>
                        <TableHead>Category Name</TableHead>
                        <TableHead className="w-[100px] text-right">Action</TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>
                    {categories.map((data) => (
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
