import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import Footer from "../Components/Layout/footer";
import ArticleList from "../Components/Articles/articleList";
import ArticleDetail from "../Components/Articles/articleDetail";
import { IApplicationState } from "../Redux/applicationState";
import { ActionCreators as NavigationContent } from "../Redux/Actions/getNavigationContentAction";
import { ActionCreators as FooterContent } from "../Redux/Actions/getFooterContentAction";
import { combinedDefaults } from "../Redux/combinedDefaults";

const useQuery = () => 
{
    return new URLSearchParams(useLocation().search);
}

export default function ArticlesPage() 
{
    const queryParam = useQuery();
    const id = queryParam.get("id");
    const content = id ? <ArticleDetail id={id} /> : <ArticleList />;

    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);

    const dispatch = useDispatch();
    const fetchNavigationContent = React.useCallback(() => { dispatch(NavigationContent.getNavigationContent()); }, [ dispatch ]);
    const fetchFooterContent = React.useCallback(() => { dispatch(FooterContent.getFooterContent()); }, [ dispatch ]);

    React.useEffect(() => 
    {
        if (navigation.content === combinedDefaults.getNavigationContent.content) fetchNavigationContent();
    }, 
    [ fetchNavigationContent, navigation.content ]);

    React.useEffect(() => 
    {
        if (footer.content === combinedDefaults.getFooterContent.content) fetchFooterContent();
    }, 
    [ fetchFooterContent, footer.content ]);

    return (
        <>
            <Navigation navigation={navigation} isLoading={navigation.isLoading} />
            <Container>
                {content}
            </Container>
            <Footer footer={footer} isLoading={footer.isLoading} />
        </>
    );
}
