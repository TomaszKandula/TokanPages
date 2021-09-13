import { IGetNavigationContent } from "../../States/Content/getNavigationContentState";

export const GetNavigationContentDefault: IGetNavigationContent = 
{
    isLoading: false,
    content: 
    {
        logo: "",
        menu: 
        {
            image: "",
            items: []
        }
    }    
}