import { IconDto } from "./iconDto";
import { ImageDto } from "./imageDto";

export interface PresentationDto {
    image: ImageDto;
    title: string;
    subtitle: string;
    icon: IconDto;
    description: string;
    logos: {
        title: string;
        images: ImageDto[];
    };
}
