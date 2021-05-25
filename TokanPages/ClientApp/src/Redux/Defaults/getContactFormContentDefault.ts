import { IGetContactFormContent } from "Redux/States/getContactFormContentState";

export const GetContactFormContentDefault: IGetContactFormContent = 
{
    isLoading: false,
    content: 
    {
        caption: "",
        text: "",
        button: ""
    }
}