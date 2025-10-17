import { DescriptionItemDto, OfferItemDto, PresentationDto } from "../../../Api/Models";
import { ReactChangeEvent, ReactChangeTextEvent, ReactKeyboardEvent, ViewProperties } from "../../../Shared/Types";

export interface OfferItemProps extends OfferItemDto {
    isChecked: boolean;
}

export interface MessageFormProps {
    company: string;
    firstName: string;
    lastName: string;
    email: string;
    phone: string;
    description: string;
}

export interface BusinessFormProps {
    hasCaption?: boolean;
    hasIcon?: boolean;
    hasShadow?: boolean;
    className?: string;
}

export interface ServiceBaseProps {
    items: OfferItemProps[];
    handler: (event: ReactChangeEvent) => void;
}

export interface ServiceItemsProps extends ServiceBaseProps {
    isLoading: boolean;
    isDisabled: boolean;
    caption: string;
}

export interface ServiceProps extends ServiceBaseProps {
    caption: string;
    disclaimer: string;
}

export interface TechnologyProps {
    canDisplay: boolean;
    caption: string;
    items: OfferItemProps[];
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
    description: DescriptionProps;
    technology: TechnologyProps;
    pricing: ServiceProps;
    presentation: PresentationDto;
}

export interface TechStackListProps extends TechnologyProps {
    isLoading: boolean;
    isDisabled: boolean;
}

export interface BusinessFormViewProps extends ViewProperties, BusinessFormProps, FormProps {
    caption: string;
    progress: boolean;
    buttonText: string;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    buttonHandler: () => void;
}

export interface DescriptionProps extends DescriptionItemDto {
    text: string;
    handler: (event: ReactChangeTextEvent) => void;
}
