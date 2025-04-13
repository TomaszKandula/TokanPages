import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { ContentPageDataAction } from "../../../Store/Actions";
import { UserSignup } from "../../../Components/Account";
import { Navigation } from "../../../Components/Layout";
import { useSnapshot, useUnhead } from "../../../Shared/Hooks";

export const SignupPage = (): React.ReactElement => {
    useUnhead("SignupPage");
    useSnapshot();

    const dispatch = useDispatch();
    const state = useSelector((state: ApplicationState) => state);
    const language = state.applicationLanguage;

    React.useEffect(() => {
        dispatch(
            ContentPageDataAction.request(
                ["layoutNavigation", "templates", "sectionCookiesPrompt", "accountUserSignup"],
                "SignupPage"
            )
        );
    }, [language?.id]);

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <main>
                <UserSignup className="pt-120 pb-240" />
            </main>
        </>
    );
};
