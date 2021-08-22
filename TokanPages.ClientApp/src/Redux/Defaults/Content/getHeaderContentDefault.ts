import { IGetHeaderContent } from "../../States/Content/getHeaderContentState";

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