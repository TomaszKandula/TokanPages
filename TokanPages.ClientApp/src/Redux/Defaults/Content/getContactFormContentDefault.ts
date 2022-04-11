import { IGetContactFormContent } from "../../../Redux/States/Content/getContactFormContentState";

export const GetContactFormContentDefault: IGetContactFormContent = 
{
    isLoading: false,
    content: 
    {
        caption: "",
        text: "",
        button: "",
        consent: ""
    }
}