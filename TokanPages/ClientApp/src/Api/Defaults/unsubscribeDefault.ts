import { IUnsubscribeContentDto } from "../../Api/Models"

export const unsubscribeDefault: IUnsubscribeContentDto = 
{
    content:
    {
        contentPre:
        {
            caption: "",
            text1: "",
            text2: "",
            text3: "",
            button: ""
        },
        contentPost:
        {
            caption: "",
            text1: "",
            text2: "",
            text3: "",
            button: ""
        }
    }
}
