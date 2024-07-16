import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import { ApplicationState } from "../../Store/Configuration";
import { NewsletterRemove } from "../../Components/NewsletterRemove";
import { BackArrow } from "../../Shared/Components";
import { Colours } from "../../Theme";

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
            <BackArrow />
            <NewsletterRemove 
                id={id} 
                pt={10} 
                pb={30} 
                background={{ backgroundColor: Colours.colours.lightGray3 }} 
            />
        </>
    );
};
