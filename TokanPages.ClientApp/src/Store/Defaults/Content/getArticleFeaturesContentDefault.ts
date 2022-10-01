import { IContentArticleFeatures } from "../../States";

export const GetArticleFeatContentDefault: IContentArticleFeatures = 
{
    isLoading: false,
    content: 
    {
        language: "",
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