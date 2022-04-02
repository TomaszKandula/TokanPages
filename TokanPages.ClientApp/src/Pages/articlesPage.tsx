import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import Footer from "../Components/Layout/footer";
import ArticleList from "../Components/Articles/articleList";
import ArticleDetail from "../Components/Articles/articleDetail";
import { IApplicationState } from "../Redux/applicationState";
import { ActionCreators as NavigationContent } from "../Redux/Actions/Content/getNavigationContentAction";
import { ActionCreators as FooterContent } from "../Redux/Actions/Content/getFooterContentAction";
import ProgressOnScroll from "../Shared/Components/Scroll/progressOnScroll";
import { CustomColours } from "../Theme/customColours";

const useQuery = () => 
{
    return new URLSearchParams(useLocation().search);
}

const ArticlesPage = (): JSX.Element => 
{
    const queryParam = useQuery();
    const dispatch = useDispatch();
    const id = queryParam.get("id");

    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);

    React.useEffect(() => 
    {
        dispatch(NavigationContent.getNavigationContent());
        dispatch(FooterContent.getFooterContent());
    }, 
    [ dispatch ]);

    return (
        <>
            <Navigation content={navigation?.content} isLoading={navigation?.isLoading} />
            {id ? <ProgressOnScroll height={10} bgcolor={CustomColours.application.navigationBlue} duration={1} /> : null}
            <Container>
                {id ? <ArticleDetail id={id} /> : <ArticleList />}
            </Container>
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}

export default ArticlesPage;
