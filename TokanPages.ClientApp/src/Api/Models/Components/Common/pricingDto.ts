import { ServiceItemDto } from "./serviceItemDto";

export interface PricingDto {
    caption: string;
    disclaimer: string;
    services: ServiceItemDto[];
}
