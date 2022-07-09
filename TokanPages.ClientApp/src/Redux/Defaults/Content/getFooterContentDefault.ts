import { IGetFooterContent } from "../../States/Content/getFooterContentState";

export const GetFooterContentDefault: IGetFooterContent = 
{
    isLoading: false,
    content: 
    {
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