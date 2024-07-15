import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import { ApplicationState } from "../../Store/Configuration";
import {
    ContentNavigationAction,
    ContentFooterAction,
    ContentTemplatesAction,
    ContentArticleAction,
} from "../../Store/Actions";
import { CustomBreadcrumb, ProgressOnScroll } from "../../Shared/Components";
import { Navigation, Footer } from "../../Components/Layout";
import { ArticleList, ArticleDetail } from "../../Components/Articles";
import { Colours } from "../../Theme";

const useQuery = () => {
    return new URLSearchParams(useLocation().search);
};

export const ArticlesPage = (): JSX.Element => {
    const queryParam = useQuery();
    const dispatch = useDispatch();
    const title = queryParam.get("title");

    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentArticleAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentTemplatesAction.get());
    }, [language?.id]);

    return (
        <>
            <Navigation />
            <CustomBreadcrumb mt={12} mb={2} mr={5} ml={5} mtDivider={4} mbDivider={4} />
            {title ? <ProgressOnScroll height={3} bgcolor={Colours.application.navigation} duration={0.1} /> : null}
            {title ? <ArticleDetail title={title} /> : <ArticleList />}
            <Footer />
        </>
    );
};
