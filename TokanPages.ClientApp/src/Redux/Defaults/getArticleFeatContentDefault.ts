import { IGetArticleFeatContent } from "../../Redux/States/getArticleFeatContentState";

export const GetArticleFeatContentDefault: IGetArticleFeatContent = 
{
    isLoading: false,
    content: 
    {
        title: "",
        desc: "",
        text1: "",
        text2: "",
        button: "",
        image1: "",
        image2: "",
        image3: "",
        image4: ""
    }
}