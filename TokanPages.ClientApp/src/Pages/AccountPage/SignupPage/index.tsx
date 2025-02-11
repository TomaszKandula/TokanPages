import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { ContentPageDataAction } from "../../../Store/Actions";
import { UserSignup } from "../../../Components/Account";
import { Navigation } from "../../../Components/Layout";
import { Cookies } from "../../../Components/Cookies";

export const SignupPage = (): React.ReactElement => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(ContentPageDataAction.request([
            "navigation", 
            "templates", 
            "cookiesPrompt", 
            "accountUserSignup"
        ], "SignupPage"));
    }, [language?.id]);

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <UserSignup className="pt-120 pb-240" background="background-colour-light-grey" />
            <Cookies />
        </>
    );
};
