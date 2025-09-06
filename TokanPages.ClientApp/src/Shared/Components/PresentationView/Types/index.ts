import { ImageDto } from "../../../../Api/Models";

export interface PresentationViewProps {
    isLoading: boolean;
    title: string;
    subtitle: string;
    description: string;
    image: ImageDto;
    icon: {
        href: string;
        name: string;
    };
    logos: {
        title: string;
        images: ImageDto[];
    };
}
