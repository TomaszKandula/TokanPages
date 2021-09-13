import { IGetFooterContent } from "../../States/Content/getFooterContentState";

export const GetFooterContentDefault: IGetFooterContent = 
{
    isLoading: false,
    content: 
    {
        terms: "",
        policy: "",
        copyright: "",
        reserved: "",
        icons: []
   }    
}