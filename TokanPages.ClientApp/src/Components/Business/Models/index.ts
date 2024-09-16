export interface MessageFormProps {
    company: string;
    firstName: string;
    lastName: string;
    email: string;
    phone: string;
    description: string;
    techStack?: string[];
    services?: string[];
}

export interface TechStackItem {
    value: string;
    key: number;
}

export interface TechStackListProps {
    list: TechStackItem[];
    handler: (value: TechStackItem, isChecked: boolean) => void;
}

export interface BusinessFormProps {
    pt?: number;
    pb?: number;
    hasCaption?: boolean;
    hasIcon?: boolean;
    hasShadow?: boolean;
    background?: React.CSSProperties;
}
