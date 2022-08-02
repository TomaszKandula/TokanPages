import { IGetFooterContent } from "../../States/Content/getFooterContentState";

export const GetFooterContentDefault: IGetFooterContent = 
{
    isLoading: false,
    content: 
    {
        language: "",
        terms: 
        {
            text: "",
            href: ""
        },
        policy:
        {
            text: "",
            href: ""
        },
        copyright: "",
        reserved: "",
        icons: []
   }    
}