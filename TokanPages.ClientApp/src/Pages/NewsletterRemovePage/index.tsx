import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import Container from "@material-ui/core/Container";
import { ApplicationState } from "../../Store/Configuration";
import { Navigation, Footer } from "../../Components/Layout";
import { NewsletterRemove } from "../../Components/NewsletterRemove";

import {
    ContentNavigationAction,
    ContentFooterAction,
    ContentNewsletterRemoveAction,
    ContentTemplatesAction,
} from "../../Store/Actions";

const useQuery = () => {
    return new URLSearchParams(useLocation().search);
};

export const NewsletterRemovePage = (): JSX.Element => {
    const queryParam = useQuery();
    const dispatch = useDispatch();
    const id = queryParam.get("id") as string;
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentNewsletterRemoveAction.get());
        dispatch(ContentTemplatesAction.get());
    }, [language?.id]);

    return (
        <>
            <Navigation />
            <Container>
                <NewsletterRemove id={id} />
            </Container>
            <Footer />
        </>
    );
};
