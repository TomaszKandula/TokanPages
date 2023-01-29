import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ContentUserSignoutState } from "../../../Store/States";
import { USER_DATA } from "../../../Shared/constants";
import { UserSignoutView } from "./View/userSignoutView";

import { 
    ApplicationState, 
    ApplicationDefault 
} from "../../../Store/Configuration";

import { 
    UserDataStoreAction, 
    UserSigninAction 
} from "../../../Store/Actions";

export const UserSignout = (props: ContentUserSignoutState): JSX.Element => 
{
    const dispatch = useDispatch();
    const store = useSelector((state: ApplicationState) => state.userDataStore);
    const [hasProgress, setHasProgress] = React.useState(true);

    React.useEffect(() => 
    {
        if (!hasProgress) return;

        dispatch(UserSigninAction.clear());
        dispatch(UserDataStoreAction.clear());
    }, 
    [ hasProgress ]);

    React.useEffect(() => 
    {
        const isUserTokenRemoved = (): boolean => localStorage.getItem(USER_DATA) === null; 
        const isUserDataEmpty = (): boolean => store.userData === ApplicationDefault.userDataStore.userData;

        if (isUserTokenRemoved() && isUserDataEmpty() && hasProgress) 
        {
            setHasProgress(false);
        }
    }, 
    [ hasProgress, store.userData ]);

    return (<UserSignoutView
        isLoading={props.isLoading}
        caption={props.content.caption}
        status={hasProgress ? props.content.onProcessing : props.content.onFinish}
        buttonText={props.content.buttonText}
    />);
}
