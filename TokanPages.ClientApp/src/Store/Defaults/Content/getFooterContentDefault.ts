import { IContentFooter } from "../../States";

export const GetFooterContentDefault: IContentFooter = 
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