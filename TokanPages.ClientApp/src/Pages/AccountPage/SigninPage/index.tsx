import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { ContentPageDataAction } from "../../../Store/Actions";
import { UserSignin } from "../../../Components/Account";
import { Navigation } from "../../../Components/Layout";
import { useSnapshot, useUnhead } from "../../../Shared/Hooks";

export const SigninPage = (): React.ReactElement => {
    useUnhead("SigninPage");
    useSnapshot();

    const dispatch = useDispatch();
    const state = useSelector((state: ApplicationState) => state);
    const language = state.applicationLanguage;

    React.useEffect(() => {
        dispatch(
            ContentPageDataAction.request(
                ["layoutNavigation", "templates", "sectionCookiesPrompt", "accountUserSignin"],
                "SigninPage"
            )
        );
    }, [language?.id]);

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <main>
                <UserSignin className="pt-120 pb-240" />
            </main>
        </>
    );
};
