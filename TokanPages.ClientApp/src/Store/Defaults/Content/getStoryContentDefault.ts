import { IGetStoryContent } from "../../States/Content/getStoryContentState";

export const GetStoryContentDefault: IGetStoryContent = 
{
    isLoading: false,
    content: 
    {
        language: "",
        items: []
    }
}
