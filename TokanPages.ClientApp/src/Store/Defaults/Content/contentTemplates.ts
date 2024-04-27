import { ContentTemplatesState } from "../../States";

export const ContentTemplates: ContentTemplatesState = {
    isLoading: false,
    content: {
        language: "",
        forms: {
            textSigning: "",
            textSignup: "",
            textPasswordReset: "",
            textUpdatePassword: "",
            textContactForm: "",
            textCheckoutForm: "",
            textNewsletter: "",
            textAccountSettings: "",
        },
        templates: {
            application: {
                unexpectedStatus: "",
                unexpectedError: "",
                validationError: "",
                nullError: "",
            },
            password: {
                emailInvalid: "",
                nameInvalid: "",
                surnameInvalid: "",
                passwordInvalid: "",
                missingChar: "",
                missingNumber: "",
                missingLargeLetter: "",
                missingSmallLetter: "",
                resetSuccess: "",
                resetWarning: "",
                updateSuccess: "",
                updateWarning: "",
            },
            articles: {
                success: "",
                warning: "",
                error: "",
                likesHintAnonym: "",
                likesHintUser: "",
                maxLikesReached: "",
            },
            newsletter: {
                success: "",
                warning: "",
                generalError: "",
                removalError: "",
            },
            messageOut: {
                success: "",
                warning: "",
                error: "",
            },
            user: {
                deactivation: "",
                removal: "",
                updateSuccess: "",
                updateWarning: "",
                emailVerification: "",
                signingWarning: "",
                signupSuccess: "",
                signupWarning: "",
            },
            payments: {
                checkoutWarning: "",
            },
        },
    },
};
