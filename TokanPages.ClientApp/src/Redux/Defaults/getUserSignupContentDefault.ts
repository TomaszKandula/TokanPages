import { IGetUserSignupContent } from "../States/getUserSignupContentState";

export const GetUserSignupContentDefault: IGetUserSignupContent = 
{
    isLoading: false,
    content: 
    {
        caption: "",
        button: "",
        link: "",
        label: ""
    }    
}