import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import { ApplicationState } from "../../Store/Configuration";
import { ContentPageDataAction } from "../../Store/Actions";
import { CustomBreadcrumb, ProgressOnScroll } from "../../Shared/Components";
import { TryAttachState } from "../../Shared/Services/initializeService";
import { Navigation, Footer } from "../../Components/Layout";
import { ArticleList, ArticleDetail } from "../../Components/Articles";
import { Colours } from "../../Theme";

const useQuery = () => {
    return new URLSearchParams(useLocation().search);
};

export const ArticlesPage = (): React.ReactElement => {
    const queryParam = useQuery();
    const dispatch = useDispatch();
    const title = queryParam.get("title");

    const state = useSelector((state: ApplicationState) => state);
    const language = state.applicationLanguage;
    const articles = state.contentPageData.components.article;

    React.useEffect(() => {
        dispatch(ContentPageDataAction.request(["navigation", "footer", "templates", "article"], "ArticlesPage"));
    }, [language?.id]);

    React.useEffect(() => {
        if (articles.language !== "") {
            TryAttachState(state);
        }
    }, [state]);

    return (
        <>
            <Navigation />
            <CustomBreadcrumb mt={12} mb={2} mr={5} ml={5} mtDivider={4} mbDivider={4} watchparam="title" />
            {title ? <ProgressOnScroll height={3} bgcolor={Colours.application.navigation} duration={0.1} /> : null}
            {title ? <ArticleDetail title={title} /> : <ArticleList />}
            <Footer />
        </>
    );
};
