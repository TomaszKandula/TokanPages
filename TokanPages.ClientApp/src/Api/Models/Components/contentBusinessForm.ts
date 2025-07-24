import { OfferItemDto } from "./Common";
import { DescriptionItemDto } from "./Common/descriptionItemDto";
import { PricingDto } from "./Common/pricingDto";

export interface BusinessFormContentDto {
    language: string;
    caption: string;
    buttonText: string;
    companyLabel: string;
    firstNameLabel: string;
    lastNameLabel: string;
    emailLabel: string;
    phoneLabel: string;
    description: DescriptionItemDto;
    techLabel: string;
    hasTechItems: boolean;
    techItems: OfferItemDto[];
    pricing: PricingDto;
}
