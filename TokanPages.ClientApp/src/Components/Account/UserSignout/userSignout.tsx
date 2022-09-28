import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { combinedDefaults } from "../../../Store/combinedDefaults";
import { ActionCreators as DataAction } from "../../../Store/Actions/Users/storeUserDataAction";
import { ActionCreators as UserAction } from "../../../Store/Actions/Users/signinUserAction";
import { IApplicationState } from "../../../Store/applicationState";
import { IGetUserSignoutContent } from "../../../Store/States/Content/getUserSignoutContentState";
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
        dispatch(UserAction.clear());
        dispatch(DataAction.clear());
    }, 
    [ progress, dispatch ]);
    
    React.useEffect(() => 
    {
        const isUserTokenRemoved = (): boolean => localStorage.getItem(USER_DATA) === null; 
        const isUserDataEmpty = (): boolean => data.userData === combinedDefaults.storeUserData.userData;

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
