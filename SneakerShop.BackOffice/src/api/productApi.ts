import Product from '@/model/product/Product';
import axiosClient from './axiosClient';
import CreateProductRequest from '@/model/product/CreateProductRequest';
import UpdateProductRequest from '@/model/product/UpdateProductRequest';

const endpoint = '/products';

const productAPI = {
    getAll: async (): Promise<Product[]> => {
        const res = await axiosClient.get<Product[]>(endpoint);
        return res.data;
    },
    getById: async (id: number): Promise<Product> => {
        const res = await axiosClient.get<Product>(`${endpoint}/${id}`);
        return res.data;
    },
    create: async (item: CreateProductRequest): Promise<Product> => {
        const res = await axiosClient.post<Product>(endpoint, item);
        return res.data;
    },
    update: async (id: number, item: UpdateProductRequest): Promise<Product> => {
        const res = await axiosClient.put<Product>(`${endpoint}/${id}`, item);
        return res.data;
    },
    remove: async (id: number): Promise<void> => {
        await axiosClient.delete(`${endpoint}/${id}`);
    },
};

export default productAPI;
