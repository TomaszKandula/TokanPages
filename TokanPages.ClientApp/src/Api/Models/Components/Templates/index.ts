export interface FormsProps {
    textSigning: string;
    textSignup: string;
    textPasswordReset: string;
    textUpdatePassword: string;
    textContactForm: string;
    textCheckoutForm: string;
    textNewsletter: string;
    textAccountSettings: string;
}

export interface ApplicationProps {
    unexpectedStatus: string;
    unexpectedError: string;
    validationError: string;
    nullError: string;
}

export interface PasswordProps {
    missingChar: string;
    missingNumber: string;
    missingLargeLetter: string;
    missingSmallLetter: string;
    resetSuccess: string;
    resetWarning: string;
    updateSuccess: string;
    updateWarning: string;
}

export interface ArticlesProps {
    success: string;
    warning: string;
    error: string;
    likesHintAnonym: string;
    likesHintUser: string;
    maxLikesReached: string;
}

export interface NewsletterProps {
    success: string;
    warning: string;
    generalError: string;
    removalError: string;
}

export interface MessageProps {
    success: string;
    warning: string;
    error: string;
}

export interface UserProps {
    deactivation: string;
    removal: string;
    updateSuccess: string;
    updateWarning: string;
    emailVerification: string;
    signingWarning: string;
    signupSuccess: string;
    signupWarning: string;
}

export interface PaymentsProps {
    checkoutWarning: string;
}

export interface TemplatesProps {
    application: ApplicationProps;
    password: PasswordProps;
    articles: ArticlesProps,
    newsletter: NewsletterProps;
    messageOut: MessageProps;
    user: UserProps;
    payments: PaymentsProps;
}

export interface TemplatesContent {
    content: {
        language: string;
        forms: FormsProps;
        templates: TemplatesProps;
    };
}
