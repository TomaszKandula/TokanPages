export interface AccessDeniedProps {
    className?: string;
}

export interface AccessDeniedViewProps extends AccessDeniedProps {
    isLoading: boolean;
    languageId: string;
    accessDeniedCaption: string;
    accessDeniedPrompt: string[];
    homeButtonText: string;
}
