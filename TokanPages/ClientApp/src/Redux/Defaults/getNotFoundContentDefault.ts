import { IGetNotFoundContent } from "../States/getNotFoundContentState";

export const GetNotFoundContentDefault: IGetNotFoundContent = 
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