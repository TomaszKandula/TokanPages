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
            textUpdateSubscriber: "",
            textRemoveSubscriber: "",
            textAccountSettings: "",
            textPromoPageEditing: "",
            textBankData: "",
        },
        templates: {
            application: {
                unexpectedStatus: "",
                unexpectedError: "",
                validationError: "",
                nullError: "",
            },
            password: {
                missingChar: "",
                missingNumber: "",
                missingLargeLetter: "",
                missingSmallLetter: "",
                resetSuccess: "",
                resetWarning: "",
                updateSuccess: "",
                updateWarning: "",
            },
            newsletter: {
                success: "",
                warning: "",
                generalError: "",
                removalError: "",
            },
            promoPage: {
                editError: "",
                statusWarning: "",
                captionWarning: "",
                backButton: "",
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
                signupClientSuccess: "",
                signupCreatorSuccess: "",
                signupWarning: "",
                bankDataWarning: "",
                bankDataSuccess: "",
            },
            payments: {
                checkoutWarning: "",
            },
        },
    },
};
