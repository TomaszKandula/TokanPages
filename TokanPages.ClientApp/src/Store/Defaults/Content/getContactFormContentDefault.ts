import { IContentContactForm } from "../../States";

export const GetContactFormContentDefault: IContentContactForm = 
{
    isLoading: false,
    content: 
    {
        language: "",
        caption: "",
        text: "",
        button: "",
        consent: "",
        labelFirstName: "",
        labelLastName: "",
        labelEmail: "",
        labelSubject: "",
        labelMessage: ""
    }
}