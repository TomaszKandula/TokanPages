import { IGetNewsletterContent } from "../../States/Content/getNewsletterContentState";

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