import { IContentNavigation } from "../../States";

export const GetNavigationContentDefault: IContentNavigation = 
{
    isLoading: false,
    content: 
    {
        language: "",
        logo: "",
        menu: 
        {
            image: "",
            items: []
        }
    }    
}