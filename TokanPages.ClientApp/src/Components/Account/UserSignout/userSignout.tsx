import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState, CombinedDefaults } from "../../../Store/Configuration";
import { UserDataAction, UserSigninAction } from "../../../Store/Actions";
import { IGetUserSignoutContent } from "../../../Store/States";
import { USER_DATA } from "../../../Shared/constants";
import { UserSignoutView } from "./View/userSignoutView";

export const UserSignout = (props: IGetUserSignoutContent): JSX.Element => 
{
    const dispatch = useDispatch();
    const data = useSelector((state: IApplicationState) => state.storeUserData);
    const [progress, setProgress] = React.useState(true);

    React.useEffect(() => 
    {
        if (!progress) return;
        dispatch(UserSigninAction.clear());
        dispatch(UserDataAction.clear());
    }, 
    [ progress, dispatch ]);
    
    React.useEffect(() => 
    {
        const isUserTokenRemoved = (): boolean => localStorage.getItem(USER_DATA) === null; 
        const isUserDataEmpty = (): boolean => data.userData === CombinedDefaults.storeUserData.userData;

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
