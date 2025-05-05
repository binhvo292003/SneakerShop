import { Button } from '@/components/ui/button';
import { Dialog, DialogContent, DialogHeader, DialogTitle } from '@/components/ui/dialog';
import { Input } from '@/components/ui/input';
import ProductItem from '@/model/product/Product';
import axios from 'axios';
import { useEffect, useState } from 'react';

interface Category {
    id: number;
    name: string;
}

interface ProductDialogProps {
    open: boolean;
    onClose: () => void;
    editing?: ProductItem | null;
}

export default function ProductDialog({ open, onClose, editing }: ProductDialogProps) {
    const [name, setName] = useState('');
    const [price, setPrice] = useState<number>(0);
    const [description, setDescription] = useState('');
    const [categories, setCategories] = useState<number[]>([]);
    const [images, setImages] = useState<File[]>([]);
    const [availableCategories, setAvailableCategories] = useState<Category[]>([]);
    const categoryUrl = 'http://localhost:8000/api/category';

    const fetchCategories = async () => {
        const source = axios.CancelToken.source();

        try {
            const response = await axios.get(categoryUrl, {
                cancelToken: source.token,
            });
            setAvailableCategories(response.data);
        } catch (err) {
            if (axios.isCancel(err)) {
                console.log('Request canceled', err.message);
            }
        }
    };

    useEffect(() => {
        fetchCategories();
    }, []);

    useEffect(() => {
        if (editing) {
            setName(editing.name || '');
            setPrice(editing.price || 0);
            setImages([]);
        }
    }, [editing]);

    const handleImageChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (e.target.files) {
            setImages(Array.from(e.target.files));
        }
    };

    const handleCategoryChange = (categoryId: number) => {
        setCategories((prev) =>
            prev.includes(categoryId)
                ? prev.filter((id) => id !== categoryId)
                : [...prev, categoryId]
        );
    };

    const handleSubmit = async () => {
        const formData = new FormData();

        formData.append('name', name);
        formData.append('price', price.toString());
        formData.append('description', description);

        categories.forEach((catId, index) => {
            formData.append(`categories[${index}]`, catId.toString());
        });

        images.forEach((image) => {
            formData.append('images', image);
        });

        console.log('FormData keys:', Array.from(formData.keys()));

        try {
            const response = await axios.post('http://localhost:8000/api/products', formData, {
                headers: { 'Content-Type': 'multipart/form-data' },
            });

            console.log('Response:', response.data);

            // Reset form
            setName('');
            setPrice(0);
            setDescription('');
            setCategories([]);
            setImages([]);
            onClose();
        } catch (error) {
            if (axios.isAxiosError(error)) {
                console.error('Validation errors:', error.response?.data);
            } else {
                console.error('Error submitting product:', error);
            }
        }
    };

    return (
        <Dialog open={open} onOpenChange={onClose}>
            <DialogContent>
                <DialogHeader>
                    <DialogTitle>{editing ? 'Edit Product' : 'Add Product'}</DialogTitle>
                </DialogHeader>{' '}
                <Input
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                    placeholder="Name"
                    className="my-2"
                />
                <Input
                    type="number"
                    value={price}
                    onChange={(e) => setPrice(Number(e.target.value))}
                    placeholder="Price"
                    className="my-2"
                />
                <Input
                    type="text"
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                    placeholder="Description"
                    className="my-2"
                />
                <Input
                    type="file"
                    onChange={handleImageChange}
                    accept="image/*"
                    multiple
                    className="my-2"
                />
                <div className="my-2">
                    {images.map((file, index) => (
                        <p key={index}>{file.name}</p>
                    ))}
                </div>
                <div className="my-2">
                    <label>Categories:</label>
                    <div>
                        {availableCategories.map((category) => (
                            <div key={category.id}>
                                <input
                                    type="checkbox"
                                    checked={categories.includes(category.id)}
                                    onChange={() => handleCategoryChange(category.id)}
                                />
                                <label>{category.name}</label>
                            </div>
                        ))}
                    </div>
                </div>
                <Button onClick={handleSubmit}>{editing ? 'Save Changes' : 'Add Product'}</Button>
            </DialogContent>
        </Dialog>
    );
}
