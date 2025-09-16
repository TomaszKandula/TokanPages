import { TechnologyDto } from "./Common";
import { DescriptionItemDto } from "./Common/descriptionItemDto";
import { PresentationDto } from "./Common/presentationDto";
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
    technology: TechnologyDto;
    pricing: PricingDto;
    presentation: PresentationDto;
}
