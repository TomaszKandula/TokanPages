import { IGetNewsletterContent } from "../../States/Content/getNewsletterContentState";

export const GetNewsletterContentDefault: IGetNewsletterContent = 
{
    isLoading: false,
    content: 
    {
        caption: "",
        text: "",
        button: "",
        labelEmail: ""
    }    
}