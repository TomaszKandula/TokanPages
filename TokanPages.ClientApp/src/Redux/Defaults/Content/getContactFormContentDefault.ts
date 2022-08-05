import { IGetContactFormContent } from "../../../Redux/States/Content/getContactFormContentState";

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