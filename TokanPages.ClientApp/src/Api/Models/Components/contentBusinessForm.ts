import { DescriptionItemDto } from "./Common/descriptionItemDto";
import { PricingDto } from "./Common/pricingDto";
import { TechItemsDto } from "./Common/techItemsDto";

export interface BusinessFormContentDto {
    language: string;
    caption: string;
    buttonText: string;
    companyLabel: string;
    firstNameLabel: string;
    lastNameLabel: string;
    emailLabel: string;
    phoneLabel: string;
    techLabel: string;
    hasTechItems: boolean;
    techItems: TechItemsDto[];
    description: DescriptionItemDto;
    pricing: PricingDto;
}
