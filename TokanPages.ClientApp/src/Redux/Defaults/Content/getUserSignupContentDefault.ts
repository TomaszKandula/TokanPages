import { IGetUserSignupContent } from "../../States/Content/getUserSignupContentState";

export const GetUserSignupContentDefault: IGetUserSignupContent = 
{
    isLoading: false,
    content: 
    {
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