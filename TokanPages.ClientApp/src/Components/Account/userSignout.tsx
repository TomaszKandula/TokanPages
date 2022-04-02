import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { combinedDefaults } from "../../Redux/combinedDefaults";
import { ActionCreators as DataAction } from "../../Redux/Actions/Users/updateUserDataAction";
import { ActionCreators as UserAction } from "../../Redux/Actions/Users/signinUserAction";
import { IApplicationState } from "../../Redux/applicationState";
import { IGetUserSignoutContent } from "../../Redux/States/Content/getUserSignoutContentState";
import { USER_DATA } from "../../Shared/constants";
import UserSignoutView from "./userSignoutView";

const UserSignout = (props: IGetUserSignoutContent): JSX.Element => 
{
    const dispatch = useDispatch();
    const data = useSelector((state: IApplicationState) => state.updateUserData);
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
        const isUserDataEmpty = (): boolean => data.userData === combinedDefaults.updateUserData.userData;

        if (isUserTokenRemoved() && isUserDataEmpty() && progress) 
        {
            setProgress(false);
        }
    }, 
    [ progress, data.userData ]);

    return (<UserSignoutView bind=
    {{
        caption: props.content.caption,
        status: progress ? props.content.onProcessing : props.content.onFinish
    }}/>);
}

export default UserSignout;
