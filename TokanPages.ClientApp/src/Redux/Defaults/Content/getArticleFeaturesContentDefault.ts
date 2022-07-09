import { IGetArticleFeaturesContent } from "../../States/Content/getArticleFeaturesContentState";

export const GetArticleFeatContentDefault: IGetArticleFeaturesContent = 
{
    isLoading: false,
    content: 
    {
        title: "",
        description: "",
        text1: "",
        text2: "",
        action: 
        {
            text: "",
            href: ""
        },
        image1: "",
        image2: "",
        image3: "",
        image4: ""
    }
}