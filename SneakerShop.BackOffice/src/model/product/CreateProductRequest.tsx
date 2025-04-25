export default interface CreateProductRequest {
    name: string;
    price: number;
    description?: string;
    categories?: string[];
    imageUrl?: string[];
}