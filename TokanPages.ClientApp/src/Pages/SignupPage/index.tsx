import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { ContentPageDataAction } from "../../Store/Actions";
import { UserSignup } from "../../Components/Account";
import { Navigation } from "../../Components/Layout";

export const SignupPage = (): React.ReactElement => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(ContentPageDataAction.request(["navigation", "templates", "accountUserSignup" ], "UserSignup"));
    }, [language?.id]);

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <UserSignup pt={120} pb={240} background={{ backgroundColor: "#FCFCFC" }} />
        </>
    );
};
