import { IGetNewsletterContent } from "../../States";

export const GetNewsletterContentDefault: IGetNewsletterContent = 
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