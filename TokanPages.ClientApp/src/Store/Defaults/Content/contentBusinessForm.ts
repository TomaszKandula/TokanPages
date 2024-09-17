import { ContentBusinessFormState } from "../../States";

export const ContentBusinessForm: ContentBusinessFormState = {
    isLoading: false,
    content: {
        language: "",
        caption: "",
        buttonText: "",
        companyLabel: "",
        firstNameLabel: "",
        lastNameLabel: "",
        emailLabel: "",
        phoneLabel: "",
        techLabel: "",
        techItems: [],
        description: {
            label: "",
            multiline: false,
            rows: 0,
            required: false
        },
        pricing: {
            caption: "",
            disclaimer: "",
            services: [],
        }
    },
};
