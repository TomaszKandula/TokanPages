import { HeaderContentDto, HeaderPhotoDto } from "../../../../Api/Models";

export interface HeaderViewProps {
    className?: string;
}

export interface RenderPictureProps {
    sources: HeaderPhotoDto | undefined;
}

export interface ButtonProps extends HeaderContentDto {
    isLoading: boolean;
}
