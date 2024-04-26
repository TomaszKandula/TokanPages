export interface TemplatesContent {
    content: {
        language: string;
        forms: {
            textSigning: string;
            textSignup: string;
            textPasswordReset: string;
            textUpdatePassword: string;
            textContactForm: string;
            textCheckoutForm: string;
            textNewsletter: string;
            textUpdateSubscriber: string;
            textRemoveSubscriber: string;
            textAccountSettings: string;
        };
        templates: {
            application: {
                unexpectedStatus: string;
                unexpectedError: string;
                validationError: string;
                nullError: string;
            };
            password: {
                missingChar: string;
                missingNumber: string;
                missingLargeLetter: string;
                missingSmallLetter: string;
                resetSuccess: string;
                resetWarning: string;
                updateSuccess: string;
                updateWarning: string;
            };
            newsletter: {
                success: string;
                warning: string;
                generalError: string;
                removalError: string;
            };
            messageOut: {
                success: string;
                warning: string;
                error: string;
            };
            user: {
                deactivation: string;
                removal: string;
                updateSuccess: string;
                updateWarning: string;
                emailVerification: string;
                signingWarning: string;
                signupSuccess: string;
                signupWarning: string;
                bankDataWarning: string;
                bankDataSuccess: string;
            };
            payments: {
                checkoutWarning: string;
            };
        };
    };
}