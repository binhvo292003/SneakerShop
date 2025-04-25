import { createAsyncThunk } from '@reduxjs/toolkit';
import productApi from './productApi';
import CreateProductRequest from '@/model/product/CreateProductRequest';
import UpdateProductRequest from '@/model/product/UpdateProductRequest';
import Product from '@/model/product/Product';
import ProductDetail from '@/model/product/ProductDetail';

export const fetchProducts = createAsyncThunk<Product[]>('items/fetchAll', async () => {
    return await productApi.getAll();
});

export const fetchProductById = createAsyncThunk<ProductDetail, number>(
    'items/fetchById',
    async (id) => {
        return await productApi.getById(id);
    }
);

export const createProduct = createAsyncThunk<ProductDetail, CreateProductRequest>(
    'items/create',
    async (item) => {
        return await productApi.create(item);
    }
);

export const updateProduct = createAsyncThunk<
    ProductDetail,
    { id: number; data: UpdateProductRequest }
>('items/update', async ({ id, data }) => {
    return await productApi.update(id, data);
});

export const deleteProduct = createAsyncThunk<number, number>('items/delete', async (id) => {
    await productApi.remove(id);
    return id;
});
