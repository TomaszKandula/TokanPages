import { IGetFeaturedContent } from "../../States/Content/getFeaturedContentState";

export const GetFeaturedContentDefault: IGetFeaturedContent = 
{
    isLoading: false,
    content: 
    {
        language: "",
        caption: "",
        text: "",
        title1: "",
        subtitle1: "",
        link1: "",
        image1: "",
        title2: "",
        subtitle2: "",
        link2: "",
        image2: "",
        title3: "",
        subtitle3: "",
        link3: "",
        image3: ""    
    }    
}