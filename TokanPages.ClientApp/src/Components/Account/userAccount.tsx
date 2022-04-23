import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ActionCreators as DialogAction } from "../../Redux/Actions/raiseDialogAction";
import { IApplicationState } from "../../Redux/applicationState";
import { IGetAccountContent } from "../../Redux/States/Content/getAccountContentState";
import { ActionCreators as UpdateUser } from "../../Redux/Actions/Users/updateUserAction";
import { IValidateAccountForm, ValidateAccountForm } from "../../Shared/Services/FormValidation";
import { ACCOUNT_FORM, RECEIVED_ERROR_MESSAGE, UPDATE_USER_SUCCESS, UPDATE_USER_WARNING } from "../../Shared/constants";
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
    const userData = useSelector((state: IApplicationState) => state.storeUserData.userData);
    const updateUserState = useSelector((state: IApplicationState) => state.updateUser);
    const raiseErrorState = useSelector((state: IApplicationState) => state.raiseError);
    const isAnonymous = Validate.isEmpty(userData.userId);

    const formDefaultValues: IValidateAccountForm = 
    {
        firstName: userData.firstName,
        lastName: userData.lastName,
        shortBio: userData.shortBio
    }

    const [form, setForm] = React.useState(formDefaultValues);
    const [progressUpdate, setProgressUpdate] = React.useState(false);

    const showSuccess = React.useCallback((text: string) => dispatch(DialogAction.raiseDialog(SuccessMessage(ACCOUNT_FORM, text))), [ dispatch ]);
    const showWarning = React.useCallback((text: string)=> dispatch(DialogAction.raiseDialog(WarningMessage(ACCOUNT_FORM, text))), [ dispatch ]);
    const postUpdateUser = React.useCallback((payload: IUpdateUserDto) => dispatch(UpdateUser.update(payload)), [ dispatch ]);
    const postUpdateUserClear = React.useCallback(() => dispatch(UpdateUser.clear()), [ dispatch ]);

    const clearForm = React.useCallback(() => 
    {
        if (!progressUpdate) return;
        setProgressUpdate(false);
        postUpdateUserClear();
    }, 
    [ progressUpdate, postUpdateUserClear ]);

    React.useEffect(() => 
    {
        if (raiseErrorState?.defaultErrorMessage === RECEIVED_ERROR_MESSAGE)
        {
            clearForm();
            return;
        }

        switch(updateUserState?.operationStatus)
        {
            case OperationStatus.notStarted:
                if (progressUpdate) postUpdateUser(
                {
                    id: userData.userId,
                    isActivated: true,
                    firstName: form.firstName,
                    lastName: form.lastName,
                    shortBio: form.shortBio
                });
            break;

            case OperationStatus.hasFinished:
                clearForm();
                showSuccess(UPDATE_USER_SUCCESS);
            break;
        }
    }, 
    [ progressUpdate, raiseErrorState?.defaultErrorMessage, updateUserState?.operationStatus, 
        OperationStatus.notStarted, OperationStatus.hasFinished ]);

    const formHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    {
        setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value }); 
    };

    const updateButtonHandler = async () => 
    {
        let validationResult = ValidateAccountForm( 
        { 
            firstName: form.firstName,
            lastName: form.lastName, 
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
            userId: userData.userId,
            userAlias: userData.aliasName,
            firstName: form.firstName,
            lastName: form.lastName,
            shortBio: form.shortBio,
            userAvatar: userData.avatarName,
            updateProgress: progressUpdate,
            uploadProgress: false,
            formHandler: formHandler,
            updateButtonHandler: updateButtonHandler,
            uploadAvatarButtonHandler: null,
            sectionAccessDenied: props.content?.sectionAccessDenied,
            sectionBasicInformation: props.content?.sectionBasicInformation
        }} />
    );
}

export default UserAccount;
