import { IContentHeader } from "../../States";

export const GetHeaderContentDefault: IContentHeader = 
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