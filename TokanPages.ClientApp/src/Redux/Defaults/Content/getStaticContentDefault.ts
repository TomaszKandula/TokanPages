import { IGetStaticContent } from "../../../Redux/States/Content/getStaticContentState";

export const GetStaticContentDefault: IGetStaticContent = 
{
    language: "",
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
