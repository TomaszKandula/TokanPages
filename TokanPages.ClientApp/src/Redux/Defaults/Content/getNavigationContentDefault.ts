import { IGetNavigationContent } from "../../States/Content/getNavigationContentState";

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