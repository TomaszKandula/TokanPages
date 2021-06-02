import { IGetHeaderContent } from "../States/getHeaderContentState";

export const GetHeaderContentDefault: IGetHeaderContent = 
{
    isLoading: false,
    content: 
    {
        photo: "",
        caption: "",
        description: "",
        action: ""
   }    
}