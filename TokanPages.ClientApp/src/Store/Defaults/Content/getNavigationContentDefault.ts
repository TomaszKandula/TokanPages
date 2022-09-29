import { IGetNavigationContent } from "../../States";

export const GetNavigationContentDefault: IGetNavigationContent = 
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