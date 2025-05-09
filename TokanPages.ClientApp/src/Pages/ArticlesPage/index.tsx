import * as React from "react";
import { useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import { ApplicationState } from "../../Store/Configuration";
import { CustomBreadcrumb, ProgressOnScroll } from "../../Shared/Components";
import { usePageContent, useSnapshot, useUnhead } from "../../Shared/Hooks";
import { Navigation, Footer } from "../../Components/Layout";
import { ArticleList, ArticleDetail } from "../../Components/Articles";

const useQuery = () => {
    return new URLSearchParams(useLocation().search);
};

export const ArticlesPage = (): React.ReactElement => {
    useUnhead("ArticlesPage");
    useSnapshot();
    usePageContent(
        ["layoutNavigation", "layoutFooter", "templates", "sectionCookiesPrompt", "pageArticle"],
        "ArticlesPage"
    );

    const queryParam = useQuery();
    const title = queryParam.get("title");

    const state = useSelector((state: ApplicationState) => state);
    const data = state.contentPageData;
    const isLoading = data?.isLoading ?? false;

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
