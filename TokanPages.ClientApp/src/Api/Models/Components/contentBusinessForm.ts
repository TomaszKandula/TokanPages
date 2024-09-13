export interface BusinessFormContentDto {
    content: {
        language: string;
        caption: string;
        buttonText: string;
        companyLabel: string;
        firstNameLabel: string;
        lastNameLabel: string;
        emailLabel: string;
        phoneLabel: string;
        techLabel: string;
        techItems: TechItemsProps[];
        description: DescriptionProps;
        pricing: PricingProps;
    };
}

interface TechItemsProps {
    value: string;
    key: number;
}

interface DescriptionProps {
    label: string;
    multiline: boolean;
    rows: number;
    required: boolean;
}

interface PricingProps {
    caption: string;
    programing: string;
    programmingPrice: string;
    hosting: string;
    hostingPrice: string;
    support: string;
    supportPrice: string;
    info: string;
}
