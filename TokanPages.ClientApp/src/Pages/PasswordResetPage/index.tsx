import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { ContentPageDataAction } from "../../Store/Actions";
import { ResetPassword } from "../../Components/Account";
import { Navigation } from "../../Components/Layout";

export const PasswordResetPage = (): React.ReactElement => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(ContentPageDataAction.request(["navigation", "templates", "resetPassword"], "ResetPassword"));
    }, [language?.id]);

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <ResetPassword pt={120} pb={240} background={{ backgroundColor: "#FCFCFC" }} />
        </>
    );
};
