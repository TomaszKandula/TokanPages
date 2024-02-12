import { ContentContactFormState } from "../../States";

export const ContentContactForm: ContentContactFormState = {
    isLoading: false,
    content: {
        language: "",
        caption: "",
        text: "",
        button: "",
        consent: "",
        labelFirstName: "",
        labelLastName: "",
        labelEmail: "",
        labelSubject: "",
        labelMessage: "",
    },
};
