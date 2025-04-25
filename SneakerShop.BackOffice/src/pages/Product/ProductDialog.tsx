import { Button } from '@/components/ui/button';
import { Dialog, DialogContent, DialogHeader } from '@/components/ui/dialog';
import { Input } from '@/components/ui/input';
import CreateProductRequest from '@/model/product/CreateProductRequest';
import ProductItem from '@/model/product/Product';
import { useEffect, useState } from 'react';

interface ProductDialogProps {
    open: boolean;
    onClose: () => void;
    onSubmit: (data: CreateProductRequest) => void;
    editing?: ProductItem | null;
}

export default function ProductDialog({ open, onClose, onSubmit, editing }: ProductDialogProps) {
    const [name, setName] = useState('');
    const [price, setPrice] = useState<number>(0);

    useEffect(() => {

    }, [editing]);

    const handleSubmit = () => {
        const data: CreateProductRequest = {
            name,
            price
        };
        onSubmit(data);
    };

    return (
        <Dialog open={open} onOpenChange={onClose}>
            <DialogContent>
                <DialogHeader>{editing ? 'Edit Product' : 'Add Product'}</DialogHeader>
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
                <Button onClick={handleSubmit}>{editing ? 'Save Changes' : 'Add Product'}</Button>
            </DialogContent>
        </Dialog>
    );
}
