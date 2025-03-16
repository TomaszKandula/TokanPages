import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { ContentPageDataAction } from "../../Store/Actions";
import { PasswordUpdate } from "../../Components/Account";
import { Navigation } from "../../Components/Layout";

export const PasswordUpdatePage = (): React.ReactElement => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(
            ContentPageDataAction.request(
                ["layoutNavigation", "templates", "sectionCookiesPrompt", "pagePasswordUpdate"],
                "PasswordUpdatePage"
            )
        );
    }, [language?.id]);

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <main>
                <PasswordUpdate className="pt-120 pb-240" />
            </main>
        </>
    );
};
