import { ServiceItemDto, TechItemsDto } from "../../../Api/Models";
import { ReactMouseEvent } from "../../../Shared/types";

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
    handler: (value: TechItemsDto, isChecked: boolean) => void;
}

export interface ServiceItemCardProps {
    isDisabled: boolean;
    key: number;
    value: ServiceItemDto;
    handler: (event: ReactMouseEvent, id: string) => void;
    services: string[];
}

export interface BusinessFormProps {
    pt?: number;
    pb?: number;
    hasCaption?: boolean;
    hasIcon?: boolean;
    hasShadow?: boolean;
    background?: React.CSSProperties;
}
