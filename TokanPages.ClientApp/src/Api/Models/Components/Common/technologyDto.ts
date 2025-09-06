import { OfferItemDto } from "./offerItemDto";

export interface TechnologyDto {
    caption: string;
    canDisplay: boolean;
    items: OfferItemDto[];
}
