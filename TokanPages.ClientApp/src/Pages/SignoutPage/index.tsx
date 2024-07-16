import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { UserSignout } from "../../Components/Account";
import { Colours } from "../../Theme";

import {
    ContentNavigationAction,
    ContentFooterAction,
    ContentUserSignoutAction,
    ContentTemplatesAction,
} from "../../Store/Actions";

export const SignoutPage = (): JSX.Element => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentUserSignoutAction.get());
        dispatch(ContentTemplatesAction.get());
    }, [language?.id]);

    return (
        <UserSignout 
            pt={10}
            pb={30}
            background={{ backgroundColor: Colours.colours.lightGray3 }} 
        />
    );
};
