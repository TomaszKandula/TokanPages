import * as React from "react";
import { useHistory } from "react-router";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../../../Store/Configuration";
import { IContentAccount } from "../../../../Store/States";
import { OperationStatus } from "../../../../Shared/enums";
import { UserInfoView } from "./View/userInfoView";
import Validate from "validate.js";

import { 
    ApplicationDialogAction, 
    UserUpdateAction, 
    UserDataStoreAction, 
    UserSigninAction, 
    UserReAuthenticateAction 
} from "../../../../Store/Actions";

import { 
    GetTextWarning, 
    SuccessMessage, 
    WarningMessage 
} from "../../../../Shared/Services/Utilities";

import { 
    IValidateAccountForm, 
    ValidateAccountForm, 
} from "../../../../Shared/Services/FormValidation";

import { 
    ACCOUNT_FORM, 
    DEACTIVATE_USER, 
    RECEIVED_ERROR_MESSAGE, 
    UPDATE_USER_SUCCESS, 
    UPDATE_USER_WARNING 
} from "../../../../Shared/constants";

export const UserInfo = (props: IContentAccount): JSX.Element => 
{
    const dispatch = useDispatch();
    const history = useHistory();

    const store = useSelector((state: IApplicationState) => state.userDataStore.userData);
    const update = useSelector((state: IApplicationState) => state.userUpdate);
    const media = useSelector((state: IApplicationState) => state.userMediaUpload);
    const error = useSelector((state: IApplicationState) => state.applicationError);

    const hasNotStarted = update?.status === OperationStatus.notStarted;
    const hasFinished = update?.status === OperationStatus.hasFinished;
    const hasMediaUploadFinished = media?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const accountFormDefault: IValidateAccountForm = 
    {
        ...store,
        userAboutText: store.shortBio ?? ""
    }

    const avatarName = Validate.isEmpty(store.avatarName) ? "N/A" : store.avatarName;
    const [isUserActivated, setIsUserActivated] = React.useState({ checked: true });
    const [accountForm, setAccountForm] = React.useState(accountFormDefault);
    const [progress, setProgress] = React.useState(false);

    const showSuccess = (text: string) => dispatch(ApplicationDialogAction.raise(SuccessMessage(ACCOUNT_FORM, text)));
    const showWarning = (text: string)=> dispatch(ApplicationDialogAction.raise(WarningMessage(ACCOUNT_FORM, text)));

    const clear = React.useCallback(() => 
    {
        if (!progress) return;
        
        dispatch(UserUpdateAction.clear());
        setProgress(false);

        if (!isUserActivated.checked)
        {
            dispatch(UserSigninAction.clear());
            dispatch(UserDataStoreAction.clear());
            history.push("/");
        }
        else
        {
            dispatch(UserReAuthenticateAction.reAuthenticate());
        }
    }, 
    [ progress ]);

    React.useEffect(() => 
    {
        if (hasError)
        {
            clear();
            return;
        }

        if (hasNotStarted && progress) 
        {
            dispatch(UserUpdateAction.update(
            {
                id: store.userId,
                isActivated: isUserActivated.checked,
                firstName: accountForm.firstName,
                lastName: accountForm.lastName,
                emailAddress: accountForm.email,
                userAboutText: accountForm.userAboutText
            }));

            return;
        }

        if (hasFinished)
        {
            clear();
            showSuccess(isUserActivated.checked ? UPDATE_USER_SUCCESS : DEACTIVATE_USER);
        }
    }, 
    [ progress, hasError, hasNotStarted, hasFinished ]);

    React.useEffect(() => 
    {
        if (hasMediaUploadFinished)
        {
            if (media.handle === undefined) return;
            if (media.payload?.blobName === undefined) return;

            const blobName = media.payload?.blobName;
            dispatch(UserDataStoreAction.update({ ...store, avatarName: blobName }));
        }
    }, 
    [ hasMediaUploadFinished ]);

    const accountKeyHandler = (event: React.KeyboardEvent<HTMLInputElement>) => 
    {
        if (event.code === "Enter")
        {
            accountButtonHandler();
        }
    }

    const accountFormHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    {
        setAccountForm({ ...accountForm, [event.currentTarget.name]: event.currentTarget.value }); 
    };

    const accountSwitchHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    {
        setIsUserActivated({ ...isUserActivated, [event.target.name]: event.target.checked });
    };

    const accountButtonHandler = () => 
    {
        let validationResult = ValidateAccountForm(accountForm);
        if (!Validate.isDefined(validationResult))
        {
            setProgress(true);
            return;
        }

        showWarning(GetTextWarning({ object: validationResult, template: UPDATE_USER_WARNING }));
    };

    return(
        <UserInfoView bind=
        {{
            isLoading: props.isLoading,
            userStore: store,
            accountForm: accountForm,
            userImageName: avatarName,
            isUserActivated: isUserActivated.checked,
            accountFormProgress: progress,           
            accountKeyHandler: accountKeyHandler,
            accountFormHandler: accountFormHandler,
            accountSwitchHandler: accountSwitchHandler,
            accountButtonHandler: accountButtonHandler,
            sectionAccessDenied: props.content?.sectionAccessDenied,
            sectionAccountInformation: props.content?.sectionAccountInformation,
        }} />
    );
}
