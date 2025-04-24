import { Button } from '@/components/ui/button';
import {
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from '@/components/ui/table';
import { Dialog, DialogContent, DialogHeader } from '@/components/ui/dialog';
import { useState } from 'react';

const dummyData = [
    {
        id: 1,
        name: 'Alice',
        email: 'alice@example.com',
    },
    {
        id: 2,
        name: 'Bob',
        email: 'bob@example.com',
    },
    {
        id: 3,
        name: 'Chris',
        email: 'chris@example.com',
    },
    {
        id: 4,
        name: 'Daniel',
        email: 'daniel@example.com',
    },
];

interface UserItem {
    id: number;
    name: string;
    email: string;
}

export default function UserPage() {
    const [open, setOpen] = useState(false);
    const [selectedUser, setSelectedUser] = useState<UserItem | null>(null);

    const handleViewDetail = (user: UserItem) => {
        setSelectedUser(user);
        setOpen(true);
    };

    return (
        <div className="">
            <div className="flex justify-between items-center mb-4">
                <h2 className="text-xl font-bold">Users</h2>
            </div>
            <Table>
                <TableHeader>
                    <TableRow>
                        <TableHead className="w-[100px]">Id</TableHead>
                        <TableHead>Name</TableHead>
                        <TableHead>Email</TableHead>
                        <TableHead className="w-[100px] text-center">Detail</TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>
                    {dummyData.map((user) => (
                        <TableRow key={user.id}>
                            <TableCell className="font-medium">{user.id}</TableCell>
                            <TableCell>{user.name}</TableCell>
                            <TableCell>{user.email}</TableCell>
                            <TableCell>
                                <div className="flex justify-center">
                                    <Button
                                        variant="ghost"
                                        size="sm"
                                        className="h-8 w-8 p-0"
                                        onClick={() => handleViewDetail(user)}
                                    >
                                        <div className="p-2 rounded-md bg-primary flex items-center justify-center text-primary-foreground">
                                            View Detail
                                        </div>
                                    </Button>
                                </div>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
            <Dialog open={open} onOpenChange={setOpen}>
                <DialogContent>
                    <DialogHeader className="text-xl font-bold">User Details</DialogHeader>
                    <div className="grid gap-4">
                        <p>
                            <span className="font-medium">User ID:</span> {selectedUser?.id}
                        </p>
                        <p>
                            <span className="font-medium">Name:</span> {selectedUser?.name}
                        </p>
                        <p>
                            <span className="font-medium">Email:</span> {selectedUser?.email}
                        </p>
                    </div>
                    <div className="flex justify-end gap-2 mt-4">
                        <Button variant="outline" onClick={() => setOpen(false)}>
                            Close
                        </Button>
                    </div>
                </DialogContent>
            </Dialog>
        </div>
    );
}
