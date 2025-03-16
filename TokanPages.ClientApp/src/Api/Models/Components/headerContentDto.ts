import { LinkDto } from "./Common/linkDto";

export interface HeaderPhotoDto {
    w360: string;
    w720: string;
    w1440: string;
    w2880: string;
}

export interface HeaderContentDto {
    language: string;
    photo: HeaderPhotoDto;
    caption: string;
    subtitle: string;
    description: string;
    hint: string;
    primaryButton: LinkDto;
    secondaryButton: LinkDto;
    tertiaryButton: LinkDto;
}
