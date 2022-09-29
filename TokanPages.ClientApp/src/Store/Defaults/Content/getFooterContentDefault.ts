import { IGetFooterContent } from "../../States";

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