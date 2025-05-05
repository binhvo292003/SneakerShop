export default interface ProductDetail {
    id: number;
    name: string;
    price: number;
    description?: string;
    categories?: { id: number; name: string; }[];
    variants?: { id: number; size: string; stock: number }[];
}