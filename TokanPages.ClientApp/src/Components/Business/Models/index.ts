import { ServiceItemDto, TechItemsDto } from "../../../Api/Models";
import { ReactChangeEvent, ReactMouseEvent } from "../../../Shared/types";

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
    isDisabled: boolean;
    list: TechItemsDto[];
    handler: (event: ReactChangeEvent, isChecked: boolean) => void;
}

export interface ServiceItemCardProps {
    isDisabled: boolean;
    key: React.Key | null | undefined;
    value: ServiceItemDto;
    handler: (event: ReactMouseEvent) => void;
    services: string[];
}

export interface BusinessFormProps {
    hasCaption?: boolean;
    hasIcon?: boolean;
    hasShadow?: boolean;
    className?: string;
    background?: string;
}
