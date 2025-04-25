import { useDispatch, useSelector } from 'react-redux';
import { createProduct, deleteProduct, fetchProducts, updateProduct } from '@/api/productThunk';
import { AppDispatch, RootState } from '@/store';
import CreateProductRequest from '@/model/product/CreateProductRequest';
import UpdateProductRequest from '@/model/product/UpdateProductRequest';

export const useProduct = () => {
    const dispatch = useDispatch<AppDispatch>();
    const items = useSelector((state: RootState) => state.product.products);
    const loading = useSelector((state: RootState) => state.product.loading);
    const error = useSelector((state: RootState) => state.product.error);

    return {
        items,
        loading,
        error,
        fetchProducts: () => dispatch(fetchProducts()),
        createProduct: (data: CreateProductRequest) => dispatch(createProduct(data)),
        updateProduct: (id: number, data: UpdateProductRequest) =>
            dispatch(updateProduct({ id, data })),
        deleteProduct: (id: number) => dispatch(deleteProduct(id)),
    };
};
