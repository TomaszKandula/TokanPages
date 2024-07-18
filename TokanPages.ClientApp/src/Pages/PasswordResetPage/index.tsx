import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { ResetPassword } from "../../Components/Account";
import { BackArrow } from "../../Shared/Components";
import { Colours } from "../../Theme";

import {
    ContentNavigationAction,
    ContentFooterAction,
    ContentResetPasswordAction,
    ContentTemplatesAction,
} from "../../Store/Actions";

export const PasswordResetPage = (): JSX.Element => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentResetPasswordAction.get());
        dispatch(ContentTemplatesAction.get());
    }, [language?.id]);

    return (
        <>
            <BackArrow />
            <ResetPassword pt={10} pb={30} background={{ backgroundColor: Colours.colours.lightGray3 }} />
        </>
    );
};
