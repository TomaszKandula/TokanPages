import { LinkDto } from "./Common/linkDto";

export interface FeatureShowcaseContentDto {
    language: string;
    caption: string;
    heading: string;
    text: string;
    image: string;
    action: LinkDto;
}