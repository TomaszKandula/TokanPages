import * as React from "react";
import { useHistory } from "react-router";
import { useDispatch, useSelector } from "react-redux";
import { ActionCreators as RaiseDialog } from "../../Redux/Actions/raiseDialogAction";
import { ActionCreators as UpdateUser } from "../../Redux/Actions/Users/updateUserAction";
import { ActionCreators as DataAction } from "../../Redux/Actions/Users/storeUserDataAction";
import { ActionCreators as UserAction } from "../../Redux/Actions/Users/signinUserAction";
import { ActionCreators as ReAuthenticateUser } from "../../Redux/Actions/Users/reAuthenticateUserAction";
import { IApplicationState } from "../../Redux/applicationState";
import { IGetAccountContent } from "../../Redux/States/Content/getAccountContentState";
import { IValidateAccountForm, ValidateAccountForm } from "../../Shared/Services/FormValidation";
import { ACCOUNT_FORM, DEACTIVATE_USER, RECEIVED_ERROR_MESSAGE, UPDATE_USER_SUCCESS, UPDATE_USER_WARNING } from "../../Shared/constants";
import SuccessMessage from "../../Shared/Components/ApplicationDialogBox/Helpers/successMessage";
import WarningMessage from "../../Shared/Components/ApplicationDialogBox/Helpers/warningMessage";
import { GetTextWarning } from "../../Shared/Services/Utilities";
import { OperationStatus } from "../../Shared/enums";
import { IUpdateUserDto } from "../../Api/Models";
import UserAccountView from "./userAccountView";
import Validate from "validate.js";

const UserAccount = (props: IGetAccountContent): JSX.Element => 
{
    const dispatch = useDispatch();
    const history = useHistory();
    const userDataState = useSelector((state: IApplicationState) => state.storeUserData.userData);
    const updateUserState = useSelector((state: IApplicationState) => state.updateUser);
    const raiseErrorState = useSelector((state: IApplicationState) => state.raiseError);
    const isAnonymous = Validate.isEmpty(userDataState.userId);

    const formDefaultValues: IValidateAccountForm = 
    {
        firstName: userDataState.firstName,
        lastName: userDataState.lastName,
        email: userDataState.email,
        shortBio: userDataState.shortBio ?? ""
    }

    const [form, setForm] = React.useState(formDefaultValues);
    const [isUserActivated, setIsUserActivated] = React.useState({ checked: true });
    const [progressUpdate, setProgressUpdate] = React.useState(false);

    const showSuccess = React.useCallback((text: string) => dispatch(RaiseDialog.raiseDialog(SuccessMessage(ACCOUNT_FORM, text))), [ dispatch ]);
    const showWarning = React.useCallback((text: string)=> dispatch(RaiseDialog.raiseDialog(WarningMessage(ACCOUNT_FORM, text))), [ dispatch ]);
    const postUpdateUser = React.useCallback((payload: IUpdateUserDto) => dispatch(UpdateUser.update(payload)), [ dispatch ]);
    const postUpdateUserClear = React.useCallback(() => dispatch(UpdateUser.clear()), [ dispatch ]);
    const reAuthenticateUser = React.useCallback(() => dispatch(ReAuthenticateUser.reAuthenticate()), [ dispatch ]);
    const clearUser = React.useCallback(() => dispatch(UserAction.clear()), [ dispatch ]);
    const clearData = React.useCallback(() => dispatch(DataAction.clear()), [ dispatch ]);

    const resetForm = React.useCallback(() => 
    {
        if (!progressUpdate) return;
        
        postUpdateUserClear();
        setProgressUpdate(false);

        if (!isUserActivated.checked)
        {
            clearUser();
            clearData();
            history.push("/");
        }
        else
        {
            reAuthenticateUser();
        }
    }, 
    [ progressUpdate, postUpdateUserClear, reAuthenticateUser, clearUser, clearData ]);

    React.useEffect(() => 
    {
        if (raiseErrorState?.defaultErrorMessage === RECEIVED_ERROR_MESSAGE)
        {
            resetForm();
            return;
        }

        switch(updateUserState?.operationStatus)
        {
            case OperationStatus.notStarted:
                if (progressUpdate) postUpdateUser(
                {
                    id: userDataState.userId,
                    isActivated: isUserActivated.checked,
                    firstName: form.firstName,
                    lastName: form.lastName,
                    emailAddress: form.email,
                    shortBio: form.shortBio
                });
            break;

            case OperationStatus.hasFinished:
                resetForm();
                showSuccess(isUserActivated.checked ? UPDATE_USER_SUCCESS : DEACTIVATE_USER);
            break;
        }
    }, 
    [ progressUpdate, raiseErrorState?.defaultErrorMessage, updateUserState?.operationStatus, 
        OperationStatus.notStarted, OperationStatus.hasFinished ]);

    const formHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    {
        setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value }); 
    };

    const switchHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    {
        setIsUserActivated({ ...isUserActivated, [event.target.name]: event.target.checked });
    };

    const updateButtonHandler = async () => 
    {
        let validationResult = ValidateAccountForm( 
        { 
            firstName: form.firstName,
            lastName: form.lastName, 
            email: form.email,
            shortBio: form.shortBio
        });

        if (!Validate.isDefined(validationResult))
        {
            setProgressUpdate(true);
            return;
        }

        showWarning(GetTextWarning({ object: validationResult, template: UPDATE_USER_WARNING }));
    };

    return(
        <UserAccountView bind=
        {{
            isLoading: props.isLoading,
            isAnonymous: isAnonymous,
            userId: userDataState.userId,
            userAlias: userDataState.aliasName,
            firstName: form.firstName,
            lastName: form.lastName,
            email: form.email,
            shortBio: form.shortBio,
            userAvatar: userDataState.avatarName,
            updateProgress: progressUpdate,
            uploadProgress: false,
            isUserActivated: isUserActivated.checked,
            formHandler: formHandler,
            switchHandler: switchHandler,
            updateButtonHandler: updateButtonHandler,
            uploadAvatarButtonHandler: null,
            sectionAccessDenied: props.content?.sectionAccessDenied,
            sectionAccountInformation: props.content?.sectionAccountInformation,
            sectionAccountPassword: props.content?.sectionAccountPassword,
            sectionAccountRemoval: props.content?.sectionAccountRemoval
        }} />
    );
}

export default UserAccount;
