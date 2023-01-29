import { ContentHeaderState } from "../../States";

export const ContentHeader: ContentHeaderState = 
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