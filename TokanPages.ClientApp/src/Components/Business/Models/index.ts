import { OfferItemDto } from "../../../Api/Models";
import { ReactChangeEvent } from "../../../Shared/types";

export interface MessageFormProps {
    company: string;
    firstName: string;
    lastName: string;
    email: string;
    phone: string;
    description: string;
    techStack: string[];
    services: string[];
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
    background?: string;
}
