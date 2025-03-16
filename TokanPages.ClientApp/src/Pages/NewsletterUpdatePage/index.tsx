import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import { ApplicationState } from "../../Store/Configuration";
import { ContentPageDataAction } from "../../Store/Actions";
import { NewsletterUpdate } from "../../Components/NewsletterUpdate";
import { Navigation } from "../../Components/Layout";

const useQuery = () => {
    return new URLSearchParams(useLocation().search);
};

export const NewsletterUpdatePage = (): React.ReactElement => {
    const queryParam = useQuery();
    const dispatch = useDispatch();
    const id = queryParam.get("id") as string;
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(
            ContentPageDataAction.request(
                ["layoutNavigation", "templates", "sectionCookiesPrompt", "pageNewsletterUpdate"],
                "NewsletterUpdatePage"
            )
        );
    }, [language?.id]);

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <main>
                <NewsletterUpdate id={id} className="pt-120 pb-240" background="background-colour-light-grey" />
            </main>
        </>
    );
};
