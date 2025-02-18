export interface CookiesPromptContentDto {
    language: string;
    caption: string;
    text: string;
    detail: string;
    loading: string[];
    options: OptionsDto;
    buttons: ButtonsDto;
    days: number;
}

export interface OptionsDto {
    enabled: boolean;
    necessaryLabel: string;
    statisticsLabel: string;
    marketingLabel: string;
    personalizationLabel: string;
}

export interface ButtonsDto {
    acceptButton: ButtonProps;
    manageButton: ButtonProps;
    closeButton: ButtonProps;
}

interface ButtonProps {
    label: string;
    enabled: boolean;
}
