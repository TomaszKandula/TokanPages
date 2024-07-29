import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import { ApplicationState } from "../../Store/Configuration";
import { NewsletterUpdate } from "../../Components/NewsletterUpdate";
import { Navigation } from "../../Components/Layout";
import { Colours } from "../../Theme";

import {
    ContentNavigationAction,
    ContentFooterAction,
    ContentNewsletterUpdateAction,
    ContentTemplatesAction,
} from "../../Store/Actions";

const useQuery = () => {
    return new URLSearchParams(useLocation().search);
};

export const NewsletterUpdatePage = (): JSX.Element => {
    const queryParam = useQuery();
    const dispatch = useDispatch();
    const id = queryParam.get("id") as string;
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentNewsletterUpdateAction.get());
        dispatch(ContentTemplatesAction.get());
    }, [language?.id]);

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <NewsletterUpdate id={id} pt={15} pb={30} background={{ backgroundColor: Colours.colours.lightGray3 }} />
        </>
    );
};
