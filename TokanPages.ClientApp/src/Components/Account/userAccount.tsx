import * as React from "react";
import { useSelector } from "react-redux";
import { IApplicationState } from "../../Redux/applicationState";
import { IGetAccountContent } from "../../Redux/States/Content/getAccountContentState";
import UserAccountView from "./userAccountView";
import Validate from "validate.js";

const UserAccount = (props: IGetAccountContent): JSX.Element => 
{
    const userData = useSelector((state: IApplicationState) => state.updateUserData.userData);
    const isAnonymous = Validate.isEmpty(userData.userId);

    // TODO: add logic for the form
    
    return(
        <UserAccountView bind=
        {{
            isLoading: false,
            isAnonymous: isAnonymous,
            userId: userData.userId,
            userAlias: userData.aliasName,
            firstName: userData.firstName,
            lastName: userData.lastName,
            shortBio: userData.shortBio,
            userAvatar: userData.avatarName,
            updateProgress: false,
            uploadProgress: false,
            formHandler: null,
            updateButtonHandler: null,
            uploadAvatarButtonHandler: null,
            sectionAccessDenied: props.content?.sectionAccessDenied,
            sectionBasicInformation: props.content?.sectionBasicInformation
        }} />
    );
}

export default UserAccount;
