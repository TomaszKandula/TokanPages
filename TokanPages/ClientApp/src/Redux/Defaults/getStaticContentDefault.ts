import { IGetStaticContent } from "../../Redux/States/getStaticContentState";

export const GetStaticContentDefault: IGetStaticContent = 
{
    story:
    {
        isLoading: false,
        items: []    
    },
    terms:
    {
        isLoading: false,
        items: []
    },
    policy:
    {
        isLoading: false,
        items: []
    }
}
