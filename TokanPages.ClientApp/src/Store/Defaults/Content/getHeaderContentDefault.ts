import { IGetHeaderContent } from "../../States";

export const GetHeaderContentDefault: IGetHeaderContent = 
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