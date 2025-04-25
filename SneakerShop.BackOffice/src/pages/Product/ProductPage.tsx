import { useEffect, useState } from 'react';
import { useProduct } from '@/hooks/useProduct';

import { Button } from '@/components/ui/button';
import ProductTable from './ProductTable';
import ProductDialog from './ProductDialog';
import Product from '@/model/product/Product';
import CreateProductRequest from '@/model/product/CreateProductRequest';

export default function ProductPage() {
    const { items: products, createProduct, updateProduct, fetchProducts } = useProduct();

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

    const handleSubmit = (data: CreateProductRequest) => {
        if (editing) {
            createProduct( data);
        } else {
            createProduct(data);
        }
        setOpen(false);
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
                onSubmit={handleSubmit}
                editing={editing}
            />
        </div>
    );
}
