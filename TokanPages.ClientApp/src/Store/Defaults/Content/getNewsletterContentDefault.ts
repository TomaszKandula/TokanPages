import { IContentNewsletter } from "../../States";

export const GetNewsletterContentDefault: IContentNewsletter = 
{
    isLoading: false,
    content: 
    {
        language: "",
        caption: "",
        text: "",
        button: "",
        labelEmail: ""
    }    
}