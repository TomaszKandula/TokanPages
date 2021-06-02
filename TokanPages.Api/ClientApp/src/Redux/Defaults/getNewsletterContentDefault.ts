import { IGetNewsletterContent } from "../States/getNewsletterContentState";

export const GetNewsletterContentDefault: IGetNewsletterContent = 
{
    isLoading: false,
    content: 
    {
        caption: "",
        text: "",
        button: ""
    }    
}