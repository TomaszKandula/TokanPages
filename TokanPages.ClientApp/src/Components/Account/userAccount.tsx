import * as React from "react";
import UserAccountView from "./userAccountView";

const UserAccount = (): JSX.Element => 
{
    // TODO: logic goes here...
    
    return(
        <UserAccountView bind=
        {{
            isLoading: false 
        }} />
    );
}

export default UserAccount;
