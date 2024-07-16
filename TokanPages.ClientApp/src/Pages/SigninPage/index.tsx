import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { UserSignin } from "../../Components/Account";
import { BackArrow } from "../../Shared/Components";
import { Colours } from "../../Theme";

import {
    ContentNavigationAction,
    ContentFooterAction,
    ContentUserSigninAction,
    ContentTemplatesAction,
} from "../../Store/Actions";

export const SigninPage = (): JSX.Element => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentUserSigninAction.get());
        dispatch(ContentTemplatesAction.get());
    }, [language?.id]);

    return (
        <>
            <BackArrow />
            <UserSignin 
                pt={10}
                pb={30}
                background={{ backgroundColor: Colours.colours.lightGray3 }} 
            />
        </>
    );
};
