import { LinkDto, NewsItemDto } from "./Common";

export interface AccountUserSigninContentDto {
    language: string;
    caption: string;
    button: string;
    link1: LinkDto;
    link2: LinkDto;
    labelEmail: string;
    labelPassword: string;
    consent: string;
    warningBadgeText: string;
    securityNews: NewsItemDto[];
}
