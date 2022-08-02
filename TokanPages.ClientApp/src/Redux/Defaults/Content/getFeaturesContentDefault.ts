import { IGetFeaturesContent } from "../../States/Content/getFeaturesContentState";

export const GetFeaturesContentDefault: IGetFeaturesContent = 
{
    isLoading: false,
    content: 
    {
        language: "",
        caption: "",
        header: "",
        title1: "",
        text1: "",
        title2: "",
        text2: "",
        title3: "",
        text3: "",
        title4: "",
        text4: ""
    }    
}