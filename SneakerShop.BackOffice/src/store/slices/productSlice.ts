import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import {
    fetchProducts,
    fetchProductById,
    createProduct,
    updateProduct,
    deleteProduct,
} from '@/api/productThunk';
import Product from '@/model/product/Product';
import ProductDetail from '@/model/product/ProductDetail';

interface ProductState {
    products: Product[];
    selectedProduct?: ProductDetail;
    loading: boolean;
    error?: string;
}

const initialState: ProductState = {
    products: [],
    selectedProduct: undefined,
    loading: false,
    error: undefined,
};

const productSlice = createSlice({
    name: 'product',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        // Fetch all products
        builder
            .addCase(fetchProducts.pending, (state) => {
                state.loading = true;
                state.error = undefined;
            })
            .addCase(fetchProducts.fulfilled, (state, action: PayloadAction<Product[]>) => {
                state.loading = false;
                state.products = action.payload;
            })
            .addCase(fetchProducts.rejected, (state, action) => {
                state.loading = false;
                state.error = action.error.message;
            });

        // Fetch product by ID
        builder.addCase(
            fetchProductById.fulfilled,
            (state, action: PayloadAction<ProductDetail>) => {
                state.loading = false;
                state.selectedProduct = action.payload;
            }
        );

        // Create product
        builder.addCase(createProduct.fulfilled, (state, action: PayloadAction<ProductDetail>) => {
            state.loading = false;
            state.products.push(action.payload);
        });

        // Update product
        builder.addCase(updateProduct.fulfilled, (state, action: PayloadAction<ProductDetail>) => {
            state.loading = false;
            const index = state.products.findIndex((p) => p.id === action.payload.id);
            if (index !== -1) {
                state.products[index] = action.payload;
            }
        });

        // Delete product
        builder.addCase(deleteProduct.fulfilled, (state, action: PayloadAction<number>) => {
            state.products = state.products.filter((i) => i.id !== action.payload);
        });
    },
});

export default productSlice.reducer;
