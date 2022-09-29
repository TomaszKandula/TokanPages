import { IGetUserSignupContent } from "../../States";

export const GetUserSignupContentDefault: IGetUserSignupContent = 
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