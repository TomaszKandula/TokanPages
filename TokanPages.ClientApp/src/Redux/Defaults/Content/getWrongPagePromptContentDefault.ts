import { IGetWrongPagePromptContent } from "../../States/Content/getWrongPagePromptContentState";

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