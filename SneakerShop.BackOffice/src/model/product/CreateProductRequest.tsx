export default interface CreateProductRequest {
    name: string;
    price: number;
    description?: string;
    categories?: number[];
    images?: File[];
}