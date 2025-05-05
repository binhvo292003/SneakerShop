import { Button } from '@/components/ui/button';
import {
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from '@/components/ui/table';
import Product from '@/model/product/Product';
import { Edit, Trash2 } from 'lucide-react';

interface ProductTableProps {
    productList: Product[];
    onEdit: (product: Product) => void;
}

export default function ProductTable({ productList, onEdit }: ProductTableProps) {
    return (
        <Table>
            <TableHeader>
                <TableRow>
                    <TableHead className="w-[100px]">Id</TableHead>
                    <TableHead>Name</TableHead>
                    <TableHead>Price</TableHead>
                    <TableHead>Details</TableHead>
                </TableRow>
            </TableHeader>
            <TableBody>
                {productList.map((data) => (
                    <TableRow key={data.id}>
                        <TableCell className="font-medium">{data.id}</TableCell>
                        <TableCell>{data.name}</TableCell>
                        <TableCell>{data.price}</TableCell>
                        <TableCell className="text-right">
                            <div className="flex justify-end gap-2">
                                <Button
                                    variant="ghost"
                                    size="sm"
                                    className="h-8 w-8 p-0"
                                    onClick={() => onEdit(data)}
                                >
                                    <Edit className="h-4 w-4" />
                                    <span className="sr-only">Edit Product</span>
                                </Button>
                                <Button
                                    variant="ghost"
                                    size="sm"
                                    className="h-8 w-8 p-0 text-red-500 hover:text-red-600 hover:bg-red-50"
                                    onClick={() => console.log('Delete category:', data.id)}
                                >
                                    <Trash2 className="h-4 w-4" />
                                    <span className="sr-only">Delete Product</span>
                                </Button>
                            </div>
                        </TableCell>
                    </TableRow>
                ))}
            </TableBody>
        </Table>
    );
}
