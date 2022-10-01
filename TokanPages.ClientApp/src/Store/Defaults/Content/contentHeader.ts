import { IContentHeader } from "../../States";

export const ContentHeader: IContentHeader = 
{
    isLoading: false,
    content: 
    {
        language: "",
        photo: "",
        caption: "",
        description: "",
        action: 
        {
            text: "",
            href: ""
        }
   }    
}