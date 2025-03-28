import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import { ApplicationState } from "../../Store/Configuration";
import { ContentPageDataAction } from "../../Store/Actions";
import { CustomBreadcrumb, ProgressOnScroll } from "../../Shared/Components";
import { TryPostStateSnapshot } from "../../Shared/Services/SpaCaching";
import { Navigation, Footer } from "../../Components/Layout";
import { ArticleList, ArticleDetail } from "../../Components/Articles";

const useQuery = () => {
    return new URLSearchParams(useLocation().search);
};

export const ArticlesPage = (): React.ReactElement => {
    const queryParam = useQuery();
    const dispatch = useDispatch();
    const title = queryParam.get("title");

    const state = useSelector((state: ApplicationState) => state);
    const language = state.applicationLanguage;
    const data = state.contentPageData;
    const articles = state?.contentPageData?.components?.pageArticle;
    const isLoading = data?.isLoading ?? false;

    React.useEffect(() => {
        dispatch(
            ContentPageDataAction.request(
                ["layoutNavigation", "layoutFooter", "templates", "sectionCookiesPrompt", "pageArticle"],
                "ArticlesPage"
            )
        );
    }, [language?.id]);

    React.useEffect(() => {
        if (articles?.language !== "") {
            TryPostStateSnapshot(state);
        }
    }, [state]);

    return (
        <>
            <Navigation />
            <main>
                <CustomBreadcrumb watchparam="title" isLoading={isLoading} />
                {title ? <ProgressOnScroll height={3} bgcolor="#6367EF" duration={0.1} /> : null}
                {title ? <ArticleDetail title={title} /> : <ArticleList />}
            </main>
            <Footer />
        </>
    );
};
