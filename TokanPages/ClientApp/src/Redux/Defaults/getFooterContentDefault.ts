import { IGetFooterContent } from "../States/getFooterContentState";

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