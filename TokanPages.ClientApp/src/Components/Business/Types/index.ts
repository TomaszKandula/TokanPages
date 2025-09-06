import { ViewProperties } from "../../../Shared/Abstractions";
import { DescriptionItemDto, OfferItemDto, PresentationDto, PricingDto } from "../../../Api/Models";
import { ReactChangeEvent, ReactChangeTextEvent, ReactKeyboardEvent } from "../../../Shared/types";

export interface MessageFormProps {
    company: string;
    firstName: string;
    lastName: string;
    email: string;
    phone: string;
    description: string;
}

export interface TechStackListProps {
    isLoading: boolean;
    isDisabled: boolean;
    hasTechItems: boolean;
    techLabel: string;
    list: OfferItemDto[];
    handler: (event: ReactChangeEvent) => void;
}

export interface ServiceItemsProps {
    isLoading: boolean;
    isDisabled: boolean;
    caption: string;
    list: OfferItemDto[];
    handler: (event: ReactChangeEvent) => void;
}

export interface BusinessFormProps {
    hasCaption?: boolean;
    hasIcon?: boolean;
    hasShadow?: boolean;
    className?: string;
}

export interface BusinessFormViewProps extends ViewProperties, BusinessFormProps, FormProps {
    caption: string;
    progress: boolean;
    buttonText: string;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    buttonHandler: () => void;
}

export interface ServicesProps extends PricingDto {
    serviceHandler: (event: ReactChangeEvent) => void;
}

export interface TechnologyProps {
    canDisplay: boolean;
    caption: string;
    items: OfferItemDto[];
    handler: (event: ReactChangeEvent) => void;
}

export interface FormProps {
    companyText: string;
    companyLabel: string;
    firstNameText: string;
    firstNameLabel: string;
    lastNameText: string;
    lastNameLabel: string;
    emailText: string;
    emailLabel: string;
    phoneText: string;
    phoneLabel: string;
    description: ExtendedDescriptionProps;
    technology: TechnologyProps;
    pricing: ServicesProps;
    presentation: PresentationDto;
}

export interface ExtendedDescriptionProps extends DescriptionItemDto {
    text: string;
    handler: (event: ReactChangeTextEvent) => void;
}
