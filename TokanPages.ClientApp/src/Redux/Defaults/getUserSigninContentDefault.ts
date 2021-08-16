import { IGetUserSigninContent } from "../States/getUserSigninContentState";

export const GetUserSigninContentDefault: IGetUserSigninContent = 
{
    isLoading: false,
    content: 
    {
        caption: "",
        button: "",
        link1: "",
        link2: ""
    }    
}