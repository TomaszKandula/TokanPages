import { IGetResetPasswordContent } from "../../States/Content/getResetPasswordContentState";

export const GetResetPasswordContentDefault: IGetResetPasswordContent = 
{
    isLoading: false,
    content: 
    {
        language: "",
        caption: "",
        button: "",
        labelEmail: ""
    }    
}