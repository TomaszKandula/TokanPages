import { IContentUserSignup } from "../../States";

export const GetUserSignupContentDefault: IContentUserSignup = 
{
    isLoading: false,
    content: 
    {
        language: "",
        caption: "",
        button: "",
        link: "",
        consent: "",
        labelFirstName: "",
        labelLastName: "",
        labelEmail: "",
        labelPassword: ""
    }    
}