import { IGetUpdatePasswordContent } from "../../States";

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
