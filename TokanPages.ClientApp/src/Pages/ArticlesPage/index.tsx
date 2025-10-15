import * as React from "react";
import { useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { Breadcrumb, ProgressOnScroll } from "../../Shared/Components";
import { useArticleQuery, usePageContent, useSnapshot, useUnhead } from "../../Shared/Hooks";
import { Navigation, Footer } from "../../Components/Layout";
import { ArticleList, ArticleDetail } from "../../Components/Articles";

export const ArticlesPage = (): React.ReactElement => {
    const heading = useUnhead("ArticlesPage");
    const query = useArticleQuery();

    useSnapshot();
    usePageContent(
        ["layoutNavigation", "layoutFooter", "templates", "sectionCookiesPrompt", "pageArticle", "pageArticles"],
        "ArticlesPage"
    );

    const state = useSelector((state: ApplicationState) => state);
    const data = state.contentPageData;
    const isLoading = data?.isLoading ?? false;

    return (
        <>
            <Navigation />
            <main>
                <h1 className="seo-only">{heading}</h1>
                <Breadcrumb watchparam="title" isLoading={isLoading} />
                {query.title ? <ProgressOnScroll height={3} bgcolor="#6367EF" duration={0.1} /> : null}
                {query.title ? <ArticleDetail title={query.title} /> : <ArticleList page={query.page} />}
            </main>
            <Footer />
        </>
    );
};
