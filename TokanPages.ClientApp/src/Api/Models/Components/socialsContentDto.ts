import { ImagesDto, LinkDto } from "./Common";

export interface SocialsContentDto {
    language: string;
    caption: string;
    social1: {
        images: ImagesDto,
        textTitle: string;
        textSubtitle: string;
        textComment: string;
        action: LinkDto
    },
    social2: {
        images: ImagesDto,
        textTitle: string;
        textSubtitle: string;
        textComment: string;
        action: LinkDto
    },
    social3: {
        images: ImagesDto,
        textTitle: string;
        textSubtitle: string;
        textComment: string;
        action: LinkDto
    }
}