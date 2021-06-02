import { IGetWrongPagePromptContent } from "../States/getWrongPagePromptContentState";

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