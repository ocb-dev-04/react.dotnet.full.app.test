export type PaginatedCollection<T> = {
    data: T[];
    totalItems: number;
    totalPages: number;
};
