import { useEffect, useState } from 'react';
import { useProduct } from '@/hooks/useProduct';

import { Button } from '@/components/ui/button';
import ProductTable from './ProductTable';
import ProductDialog from './ProductDialog';
import Product from '@/model/product/Product';

export default function ProductPage() {
    const { items: products, fetchProducts } = useProduct();

    const [open, setOpen] = useState(false);
    const [editing, setEditing] = useState<Product | null>(null);

    useEffect(() => {
        fetchProducts();
    }, []);

    const handleEdit = (product: Product) => {
        setEditing(product);
        setOpen(true);
    };

    const handleNew = () => {
        setEditing(null);
        setOpen(true);
    };

    return (
        <div className="">
            <div className="flex justify-between items-center mb-4">
                <h2 className="text-xl font-bold">Product</h2>
                <Button onClick={handleNew}>Add Product</Button>
            </div>

            <ProductTable productList={products} onEdit={handleEdit} />

            <ProductDialog
                open={open}
                onClose={() => setOpen(false)}
                editing={editing}
            />
        </div>
    );
}
