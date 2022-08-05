import { IGetUpdatePasswordContent } from "../../States/Content/getUpdatePasswordContentState";

export const GetUpdatePasswordContentDefault: IGetUpdatePasswordContent = 
{
    isLoading: false,
    content: 
    {
        language: "",
        caption: "",
        button: "",
        labelNewPassword: "",
        labelVerifyPassword: ""
    }    
}
