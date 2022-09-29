import { IGetWrongPagePromptContent } from "../../States";

export const GetWrongPagePromptContentDefault: IGetWrongPagePromptContent = 
{
    isLoading: false,
    content: 
    {
        language: "",
        code: "",
        header: "",
        description: "",
        button: ""
   }    
}