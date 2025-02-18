export interface ClientImageDto {
    path: string;
    name: string;
    width: string;
    heigh: string;
}

export interface ClientsContentDto {
    language: string;
    caption: string;
    images: ClientImageDto[];
}
