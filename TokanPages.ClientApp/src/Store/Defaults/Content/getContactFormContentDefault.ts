import { IGetContactFormContent } from "../../States";

export const GetContactFormContentDefault: IGetContactFormContent = 
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