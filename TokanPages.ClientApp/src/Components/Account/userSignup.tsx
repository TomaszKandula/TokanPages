import * as React from "react";
import { IGetUserSignupContent } from "../../Redux/States/getUserSignupContentState";
import UserSignupView from "./userSignupView";

const UserSignup = (props: IGetUserSignupContent) => 
{
    console.log(props);
    //...

    return (<UserSignupView bind=
    {{
        isLoading: false,
        caption: "",
        label: "",
        button: "",
        link: "",
        buttonHandler: null,
        formHandler: null,
        progress: false,
        firstName: "",
        lastName: "",
        email: "",
        password: ""
    }}/>);
}

export default UserSignup;
