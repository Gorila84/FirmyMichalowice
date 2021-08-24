export interface Photo {
    id: number;
    description: string;
    url: string;
    isMain: boolean;
    fileData: Blob;
}
