import { IGetWrongPagePromptContent } from "../../States/Content/getWrongPagePromptContentState";

export const GetWrongPagePromptContentDefault: IGetWrongPagePromptContent = 
{
    isLoading: false,
    content: 
    {
        code: "",
        header: "",
        description: "",
        button: ""
   }    
}