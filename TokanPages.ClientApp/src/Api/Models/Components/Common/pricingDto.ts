import { OfferItemDto } from "./offerItemDto";

export interface PricingDto {
    caption: string;
    disclaimer: string;
    items: OfferItemDto[];
}
