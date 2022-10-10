import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState, ApplicationDefault } from "../../../Store/Configuration";
import { UserDataStoreAction, UserSigninAction } from "../../../Store/Actions";
import { IContentUserSignout } from "../../../Store/States";
import { USER_DATA } from "../../../Shared/constants";
import { UserSignoutView } from "./View/userSignoutView";

export const UserSignout = (props: IContentUserSignout): JSX.Element => 
{
    const dispatch = useDispatch();
    const data = useSelector((state: IApplicationState) => state.userDataStore);
    const [progress, setProgress] = React.useState(true);

    React.useEffect(() => 
    {
        if (!progress) return;
        dispatch(UserSigninAction.clear());
        dispatch(UserDataStoreAction.clear());
    }, 
    [ progress ]);

    React.useEffect(() => 
    {
        const isUserTokenRemoved = (): boolean => localStorage.getItem(USER_DATA) === null; 
        const isUserDataEmpty = (): boolean => data.userData === ApplicationDefault.userDataStore.userData;

        if (isUserTokenRemoved() && isUserDataEmpty() && progress) 
        {
            setProgress(false);
        }
    }, 
    [ progress, data.userData ]);

    return (<UserSignoutView bind=
    {{
        isLoading: props.isLoading,
        caption: props.content.caption,
        status: progress ? props.content.onProcessing : props.content.onFinish,
        buttonText: props.content.buttonText
    }}/>);
}
