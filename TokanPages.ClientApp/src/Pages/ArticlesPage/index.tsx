import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import Container from "@material-ui/core/Container";
import { ApplicationState } from "../../Store/Configuration";
import { ContentNavigationAction, ContentFooterAction, ContentTemplatesAction, ContentArticleAction } from "../../Store/Actions";
import { ProgressOnScroll } from "../../Shared/Components";
import { Navigation, Footer } from "../../Components/Layout";
import { ArticleList, ArticleDetail } from "../../Components/Articles";
import { Colours } from "../../Theme";

const useQuery = () => {
    return new URLSearchParams(useLocation().search);
};

export const ArticlesPage = (): JSX.Element => {
    const queryParam = useQuery();
    const dispatch = useDispatch();
    const id = queryParam.get("id");

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
            {id ? <ProgressOnScroll height={10} bgcolor={Colours.application.navigation} duration={1} /> : null}
            <Container>{id ? <ArticleDetail id={id} /> : <ArticleList />}</Container>
            <Footer />
        </>
    );
};
