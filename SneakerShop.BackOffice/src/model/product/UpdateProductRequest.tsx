export default interface UpdateProductRequest {
    id: number;
    name: string;
    price: number;
    description?: string;
    categories?: string[];
    imageUrl?: string[];
}