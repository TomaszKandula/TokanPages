import { IGetStoryContent } from "../../../Redux/States/Content/getStoryContentState";

export const GetStoryContentDefault: IGetStoryContent = 
{
    isLoading: false,
    content: 
    {
        language: "",
        items: []
    }
}
