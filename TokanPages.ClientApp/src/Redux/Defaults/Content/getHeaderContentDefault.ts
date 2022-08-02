import { IGetHeaderContent } from "../../States/Content/getHeaderContentState";

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