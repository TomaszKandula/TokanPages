import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import { ApplicationState } from "../../Store/Configuration";
import { ContentPageDataAction } from "../../Store/Actions";
import { NewsletterRemove } from "../../Components/NewsletterRemove";
import { Navigation } from "../../Components/Layout";

const useQuery = () => {
    return new URLSearchParams(useLocation().search);
};

export const NewsletterRemovePage = (): React.ReactElement => {
    const queryParam = useQuery();
    const dispatch = useDispatch();
    const id = queryParam.get("id") as string;
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(ContentPageDataAction.request(["navigation", "templates", "newsletterRemove"], "NewsletterRemovePage"));
    }, [language?.id]);

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <NewsletterRemove id={id} pt={120} pb={240} background={{ backgroundColor: "#FCFCFC" }} />
        </>
    );
};
